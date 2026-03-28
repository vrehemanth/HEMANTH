using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using EGI_Backend.Application.Interfaces;

namespace EGI_Backend.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IDataService _dataService;
        private readonly IAIService _aiService;
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, System.Collections.Generic.List<(string role, string content)>> _guestHistory = new();

        public ChatService(IDataService dataService, IAIService aiService)
        {
            _dataService = dataService;
            _aiService = aiService;
        }

        public async Task<string> ProcessMessage(string userId, string role, string message)
        {
            var rawMessage = message.ToLower();
            var options = new JsonSerializerOptions 
            { 
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                MaxDepth = 32
            };

            // ===== GUEST / UNREGISTERED USER — Policy Suggestion Engine =====
            if (role == "Guest" || role == "guest")
            {
                return await HandleGuestQuery(userId, rawMessage, message, options);
            }

            // ===== AUTHENTICATED USER — Role-Based Data Access =====
            // STEP 1: Always get dashboard summary (compact, has all key counts)
            var summary = await _dataService.GetSummary(userId, role);
            var summaryJson = JsonSerializer.Serialize(summary, options);
            // Cap summary JSON to prevent token overflow
            if (summaryJson.Length > 1500) summaryJson = summaryJson.Substring(0, 1500) + "...}";

            // STEP 2: Keyword-route to fetch relevant detailed data
            string detailedSection = "";

            if (rawMessage.Contains("claim"))
            {
                var data = await _dataService.GetUserClaims(userId, role);
                // If user asks about specific status, filter to show those claims with reasons
                string statusFilter = null;
                if (rawMessage.Contains("reject")) statusFilter = "Rejected";
                else if (rawMessage.Contains("approv") || rawMessage.Contains("accept")) statusFilter = "Approved";
                else if (rawMessage.Contains("pending")) statusFilter = "Pending";
                
                detailedSection = BuildDetailedJson("Claims", data, options, statusFilter, 10);
            }
            else if (rawMessage.Contains("invoice") || rawMessage.Contains("bill") || rawMessage.Contains("payment"))
            {
                var data = await _dataService.GetInvoices(userId, role);
                detailedSection = BuildDetailedJson("Invoices", data, options);
            }
            else if (rawMessage.Contains("endorsement") || rawMessage.Contains("revision") || rawMessage.Contains("modification"))
            {
                var data = await _dataService.GetEndorsements(userId, role);
                detailedSection = BuildDetailedJson("Endorsements / Policy Revisions", data, options);
            }
            else if (rawMessage.Contains("plan"))
            {
                var data = await _dataService.GetAvailablePlans();
                if (data is System.Collections.Generic.IEnumerable<EGI_Backend.Application.DTOs.InsurancePlanDto> plansData)
                {
                    var slimPlans = plansData.Select(p => new {
                        p.PlanCode, p.PlanName, PerMemberBasePremium = (int)p.BasePremium, p.Description, p.Status, p.HasHealthCheckup,
                        Coverages = string.Join(", ", p.Coverages.Select(c => $"{c.Type}: ₹{FormatIndian(c.CoverageAmount)} ({c.CoveredGroup})"))
                    }).ToList();
                    detailedSection = "[Available Insurance Plans]: Total = " + slimPlans.Count + "\n" + JsonSerializer.Serialize(slimPlans, options);
                }
                else
                {
                    detailedSection = BuildSlimList("Available Insurance Plans", data, new[] { "PlanCode", "PlanName", "BasePremium", "Description", "Status" }, 30);
                }
            }
            else if (rawMessage.Contains("policy") || rawMessage.Contains("policies"))
            {
                var data = await _dataService.GetPolicies(userId, role);
                detailedSection = BuildDetailedJson("Policies", data, options);
            }
            else if (rawMessage.Contains("member") || rawMessage.Contains("employee") || rawMessage.Contains("dependent"))
            {
                var data = await _dataService.GetMembers(userId, role);
                detailedSection = BuildDetailedJson("Members & Dependents", data, options);
            }
            else if (rawMessage.Contains("hospital") || rawMessage.Contains("dispatch") || rawMessage.Contains("network"))
            {
                var data = await _dataService.GetHospitals();
                detailedSection = BuildDetailedJson("Hospitals", data, options, null, 20);
            }
            else if (rawMessage.Contains("health") || rawMessage.Contains("checkup") || rawMessage.Contains("profile"))
            {
                var data = await _dataService.GetProfile(userId, role);
                detailedSection = "[User Profile]: " + JsonSerializer.Serialize(data, options);
            }
            else if (rawMessage.Contains("commission") && (role == "Agent"))
            {
                var data = await _dataService.GetCommissionLogs(userId, role);
                detailedSection = BuildDetailedJson("Commission Logs", data, options);
            }
            else if (rawMessage.Contains("staff") || rawMessage.Contains("agent") || rawMessage.Contains("officer"))
            {
                if (role == "Admin")
                {
                    var data = await _dataService.GetStaff(userId, role);
                    detailedSection = BuildDetailedJson("Staff", data, options);
                }
            }
            else if (rawMessage.Contains("audit") || rawMessage.Contains("log") || rawMessage.Contains("activity"))
            {
                if (role == "Admin")
                {
                    var data = await _dataService.GetAuditLogs(userId, role);
                    detailedSection = BuildDetailedJson("Audit Logs", data, options);
                }
            }
            else if (rawMessage.Contains("pending") && rawMessage.Contains("client"))
            {
                if (role == "Admin")
                {
                    var data = await _dataService.GetPendingClients(userId, role);
                    detailedSection = BuildDetailedJson("Pending Clients", data, options);
                }
            }
            else if (rawMessage.Contains("customer") || rawMessage.Contains("client"))
            {
                var data = await _dataService.GetMembers(userId, role);
                detailedSection = BuildDetailedJson("Customers / Clients", data, options);
            }

            // STEP 3: Build context string
            var contextString = $"Dashboard Summary:\n{summaryJson}";
            if (!string.IsNullOrEmpty(detailedSection))
            {
                contextString += "\n\nDetailed Data:\n" + detailedSection;
            }

            // Safety cap for Groq free tier token limit
            if (contextString.Length > 4000)
            {
                contextString = contextString.Substring(0, 4000) + "\n[TRUNCATED]";
            }

            // STEP 4: Role-specific prompt
            string roleDescription = role switch
            {
                "Admin" => "You are helping a System Admin who can see all system data: agents, officers, clients, claims, policies, invoices, endorsements, audit logs, revenue, payouts, and staff.",
                "Agent" => "You are helping an Insurance Agent who can see: their assigned customers, policies they sold, commission earned, and claims from their customers.",
                "ClaimsOfficer" => "You are helping a Claims Officer who can see: all pending claims, claims they reviewed, approval/rejection stats, and health checkup statuses.",
                "Customer" => "You are helping a Corporate Customer who can see: their policies, members, dependents, claims, invoices, endorsements, and health checkup info.",
                _ => "You are helping a system user."
            };

            var prompt = $@"
You are an insurance chatbot for an Employee Group Insurance system.
{roleDescription}

RULES:
- Answer using ONLY the provided context data.
- 'Revisions' or 'Modifications' = Endorsements. 'Accepted' = Approved. 'Employees' = Members.
- For counts, use Dashboard Summary numbers. For details/lists, use the Detailed Data section.
- If a status (like Rejected) is not in 'By Status', it means 0.
- For health checkup: LastHealthCheckupDate shows last date, next is 365 days later.
- When listing claims, format each with: ClaimNumber, ClaimType, Amount, Status, and RejectionReason (if rejected).
- When listing invoices, format each with: key details like Amount, Status, DueDate.
- When listing endorsements, format each with: Type, Status, and description.
- ALWAYS format money values using the Indian Rupee symbol (₹) and the Indian numbering system (Lakhs and Crores, e.g., ₹1,50,000 or ₹1,50,00,000).
- DOCUMENTATION RULES: 
  - For Corporate Registration (KYB): Require GSTIN, PAN, CIN (Certificate of Incorporation), and Address Proof.
  - For Claims: Require Medical Bill, Hospital Discharge Report, and Doctor Prescription.
  - For Life Claims: Require Death Certificate and Post-Mortem Report.
  - For Accident Claims: Require FIR and Accident Report.
- If user greets, respond politely.
- If data is not available, say 'No data found in system'.

User Role: {role}

Context:
{contextString}

User Question:
{message}

Give a clear, well-formatted answer. Use numbered lists when showing multiple records.
";

            Console.WriteLine("========== CHATBOT DEBUG ==========");
            Console.WriteLine($"Role: {role} | Context: {contextString.Length} chars");
            Console.WriteLine(contextString.Substring(0, Math.Min(500, contextString.Length)));
            Console.WriteLine("====================================");

            return await _aiService.GenerateResponse(prompt);
        }

        // ===== GUEST HANDLER: Policy Suggestion Engine =====
        private async Task<string> HandleGuestQuery(string userId, string rawMessage, string originalMessage, JsonSerializerOptions options)
        {
            var plans = await _dataService.GetAvailablePlans();
            string plansJson = "[]";
            if (plans is System.Collections.Generic.IEnumerable<EGI_Backend.Application.DTOs.InsurancePlanDto> plansData)
            {
                var plansDataList = plansData.ToList();
                var slimPlans = plansDataList
                    .OrderByDescending(p => p.BasePremium)
                    .Select(p => new {
                        p.PlanCode, p.PlanName, PerMemberBasePremium = (int)p.BasePremium,
                        Coverages = string.Join(", ", p.Coverages.Select(c => $"{c.Type}: ₹{FormatIndian(c.CoverageAmount)} ({c.CoveredGroup})"))
                    }).ToList();
                plansJson = $"[TotalAvailablePlans={plansDataList.Count}]: " + JsonSerializer.Serialize(slimPlans, options);
            }
            if (plansJson.Length > 8000) plansJson = plansJson.Substring(0, 8000) + "...]";

            var prompt = $@"
You are a friendly insurance advisor for an Employee Group Insurance platform.
You are chatting with an UNREGISTERED visitor who is exploring insurance options.

AVAILABLE INSURANCE PLANS IN OUR SYSTEM:
{plansJson}

YOUR CAPABILITIES:
- Recommend plans based on user's needs.
- Help estimate costs (PerMemberBasePremium is the YEARLY base cost per member).
- Compare plans.
- Encourage them to register on their platform to get a detailed quote.

SUGGESTION / ONBOARDING RULES:
If a user asks for a plan suggestion or quote, DO NOT guess or assume their details. First check if they provided ALL 5 factors:
1) Industry type
2) Employee count
3) Dependents (Yes/No or count)
4) Average age
5) Budget per person per year
If any of these are missing, politely ask them to provide the missing details FIRST before generating a plan recommendation or quote.
CRITICAL: NEVER question or second-guess the user's employee count or dependent count. Accept ALL numbers as-is. 1000 employees with 3000 dependents is perfectly normal (avg 3 per employee). Do NOT say 'that is not feasible' or try to correct their numbers.

BUDGET RULES (STRICT LOGIC - MANDATORY!):
When the user provides a budget per person per year (e.g. ₹25,000):
1. **Integer Comparison**: Compare the 'PerMemberBasePremium' number (e.g. 2500) directly against the user's budget (e.g. 25000).
2. **Boolean Check**: Is (PerMemberBasePremium <= UserBudget)? If YES, the plan is WITHIN budget. 
3. **Logic Check**: 2,500 < 25,000. 16,000 < 25,000. 14,000 < 25,000. 6,000 < 25,000. Check your digit counts!
4. **FILTER**: You must ONLY recommend plans where it is WITHIN budget. Completely hide all others.
5. **NO SKIPPING**: You MUST list EVERY SINGLE plan from the JSON that is within budget. If 14 plans fit, you MUST list 14. Do NOT abbreviate.
6. **NO HALLUCINATION**: If the plan is 16,000 and the budget is 25,000, it is a perfect match. Do not say it is above budget.

ESTIMATION RULES (STRICT ARITHMETIC - UNIT-BASED MATH):
1. **Members**: Count total employees + total dependents. (Example: 1000 + 3000 = 4000).
2. **Total Yearly Base (TYB)**: Multiply premium by members. 
   - 🚨 **LOGIC: (25,000 per member) * (4,000 members) = 10 Crores (₹10,00,00,000).** 
   - 🚨 **It is NOT 100 Crores. Do not add extra zeros.**
3. **Multiplier**: Add 10%. (10 Crores + 1 Crore = 11 Crores).
4. **Discounted Yearly**: Subtract 5%. (11 Crores - 55 Lakhs = 10.45 Crores).
   - 🚨 **DISCOUNT RULE: You MUST always mention the 5% yearly discount. Never say it's not available.**
5. **Monthly**: Divide Yearly by 12. (11 Crores / 12 = 91.6 Lakhs).
6. **COMMAS**: Use Indian style only: ₹1,00,00,000 (1 Crore), ₹10,45,00,000 (10.45 Crores).
7. **VERIFY**: Count your digits! 1 Crore has 8 digits. 10 Crores have 9 digits. 100 Crores have 10 digits.
7. **INDIAN FORMATTING ONLY**: 
   - 1 Lakh = 1,00,000 (7 digits). 
   - 1 Crore = 1,00,00,000 (8 digits). 
   - 10 Crores = 10,00,00,000 (9 digits).
   - Use the Indian digit grouping (X,XX,XX,XXX). Example: ₹12,32,00,000 is 12.32 Crores.
   - 🚨 NEVER use US formatting (120,000,000). Always use Indian commas (12,00,00,000).

RULES:
- Give ONLY the Top 5 policy recommendations that fit within the budget. Skip all others to keep the chat clean.
- SORT these 5 plans in DESCENDING order (highest premium first, closest to the budget).
- 🚨 STRICTLY HIDE any plan that is EVEN ₹1 ABOVE the user's budget. Never mention a surplus plan.- Keep each plan entry concise: PlanCode, PlanName, Coverages, and PerMemberBasePremium.
- ALWAYS append '(per member)' or '(per person)' explicitly next to the premium string so users know family members are charged individually based on risk factors!
- DO NOT show risk factors, math calculations, or estimations when simply listing plans. Only mention them when estimating a quote.
- NEVER alter or perform math on the 'PerMemberBasePremium' value when listing plans. State the EXACT number provided in the JSON data.
- Be helpful, friendly, and professional. Use formatting (bullet points, bold text).
- Only recommend plans from the provided data.
- If the user asks for a clean list, numbered list, or to exclude special characters like '*' or '#', strictly use a simple 1. 2. 3. numbering format and avoid all asterisks/hashtags.
- If asked about claims or account-specific things, tell them to register/login first.
- DOCUMENTATION RULES: 
  - For Corporate Registration (KYB): Require GSTIN, PAN, CIN (Certificate of Incorporation), and Address Proof.
  - For Claims: Require Medical Bill, Hospital Discharge Report, and Doctor Prescription.
  - For Life Claims: Require Death Certificate and Post-Mortem Report.
  - For Accident Claims: Require FIR and Accident Report.
- ALWAYS use the Indian Numeral System for commas (Lakhs and Crores). Examples: ₹1,50,000 (1.5 Lakh), ₹1,00,00,000 (1 Crore), ₹5,60,00,000 (5.6 Crore).
- DO NOT use the US format (₹150,000). Always put commas in the Indian style (X,XX,XXX).
- Remember: 2,500 < 25,000, 16,000 < 25,000, 14,000 < 25,000. Check your zero counts carefully!
- If the result is over 1 Crore, write it as '₹X.XX Crores'. If it is over 1 Lakh, write '₹X.XX Lakhs'.
- Count your zeros before placing commas: 12,00,00,000 has 7 zeros + leading digits.

User Question:
{originalMessage}

Give a helpful and friendly answer.
";

            // Manage Conversation History for Guest Sessions
            var history = _guestHistory.GetOrAdd(userId, _ => new List<(string role, string content)>());
            
            // Limit history to 10 turns to keep prompt efficient
            if (history.Count >= 10) history.RemoveRange(0, 2); 

            // Construct Contextual Messages
            var messages = new List<(string role, string content)> { ("system", prompt) };
            foreach (var turn in history) messages.Add(turn);
            messages.Add(("user", originalMessage));

            // Generate contextual response
            var response = await _aiService.GenerateChatResponse(messages);

            // Record turn in history for next call
            history.Add(("user", originalMessage));
            history.Add(("assistant", response));

            return response;
        }

        // ===== Helpers =====
        private string BuildDetailedJson(string label, object data, JsonSerializerOptions options, string statusFilter = null, int maxItems = 5)
        {
            if (data is string strData) return $"[{label}]: {strData}";
            if (data is System.Collections.IEnumerable enumerable)
            {
                int total = 0;
                var statDict = new System.Collections.Generic.Dictionary<string, int>();
                var allItems = new System.Collections.Generic.List<object>();
                foreach (var item in enumerable)
                {
                    total++;
                    var prop = item.GetType().GetProperty("Status");
                    if (prop != null)
                    {
                        var val = prop.GetValue(item)?.ToString() ?? "Unknown";
                        if (!statDict.ContainsKey(val)) statDict[val] = 0;
                        statDict[val]++;
                    }
                    allItems.Add(item);
                }
                
                var breakdown = "";
                if (statDict.Count > 0)
                {
                    var parts = new System.Collections.Generic.List<string>();
                    foreach (var kv in statDict) parts.Add($"{kv.Value} {kv.Key}");
                    breakdown = " | By Status: " + string.Join(", ", parts);
                }

                // Filter by status if specified
                System.Collections.Generic.List<object> displayItems;
                string filterNote = "";
                if (!string.IsNullOrEmpty(statusFilter))
                {
                    displayItems = new System.Collections.Generic.List<object>();
                    foreach (var item in allItems)
                    {
                        var prop = item.GetType().GetProperty("Status");
                        if (prop != null)
                        {
                            var val = prop.GetValue(item)?.ToString() ?? "";
                            if (val.Equals(statusFilter, StringComparison.OrdinalIgnoreCase))
                            {
                                if (displayItems.Count < maxItems)
                                {
                                    // Extract only essential fields to save tokens
                                    var slim = new System.Collections.Generic.Dictionary<string, object>();
                                    foreach (var p in new[] { "ClaimNumber", "ClaimType", "ClaimAmount", "ClaimReason", "Status", "RejectionReason", "ClaimDate", "Type", "EndorsementType", "Description", "Amount", "DueDate", "BillingPeriodFrom" })
                                    {
                                        var pi = item.GetType().GetProperty(p);
                                        if (pi != null)
                                        {
                                            var v = pi.GetValue(item);
                                            if (v != null) slim[p] = v;
                                        }
                                    }
                                    displayItems.Add(slim);
                                }
                            }
                        }
                    }
                    filterNote = $" (Filtered to {statusFilter} only, showing {displayItems.Count})";
                }
                else
                {
                    displayItems = allItems.Count > maxItems ? allItems.GetRange(0, maxItems) : allItems;
                }

                var json = JsonSerializer.Serialize(displayItems, options);
                if (json.Length > 3500) json = json.Substring(0, 3500) + "...]";
                return $"[{label}]: Total = {total}{breakdown}{filterNote}\nRecords:\n{json}";
            }
            return $"[{label}]: {JsonSerializer.Serialize(data, options)}";
        }

        /// <summary>Builds a compact text list with only specified fields - ideal for large lists like plans/hospitals</summary>
        private string BuildSlimList(string label, object data, string[] fields, int maxItems = 20)
        {
            if (data is string strData) return $"[{label}]: {strData}";
            if (data is System.Collections.IEnumerable enumerable)
            {
                var sb = new System.Text.StringBuilder();
                int count = 0;
                foreach (var item in enumerable)
                {
                    if (count >= maxItems) break;
                    count++;
                    var parts = new System.Collections.Generic.List<string>();
                    foreach (var f in fields)
                    {
                        var prop = item.GetType().GetProperty(f);
                        if (prop != null)
                        {
                            var val = prop.GetValue(item);
                            if (val != null) parts.Add($"{f}: {val}");
                        }
                    }
                    sb.AppendLine($"{count}. {string.Join(" | ", parts)}");
                }
                return $"[{label}]: Total = {count}\n{sb}";
            }
            return $"[{label}]: No data";
        }

        /// <summary>Formats a decimal number using Indian Numeral System (Lakhs/Crores commas)</summary>
        private static string FormatIndian(decimal amount)
        {
            var indian = new System.Globalization.CultureInfo("en-IN");
            return ((long)amount).ToString("N0", indian);
        }
    }
}

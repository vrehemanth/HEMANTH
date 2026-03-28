using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using System.Linq;

namespace EGI_Backend.Infrastructure.Services
{
    public class AIAdjudicationService : IAIAdjudicationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string? _apiKey;
        private readonly string _model;

        public AIAdjudicationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiKey = configuration["AIAdjudicationSettings:GroqApiKey"];
            _model = configuration["AIAdjudicationSettings:Model"] ?? "llama-3.1-8b-instant";
            
            if (string.IsNullOrEmpty(_apiKey)) Console.WriteLine("[AI SERVICE]: Warning - GroqApiKey is missing from configuration.");
        }

        public async Task<AIAdjudicationResult> AdjudicateClaimAsync(Claim claim, string dischargeSummaryText)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                return new AIAdjudicationResult
                {
                    IsApprovedRecommendation = false,
                    ConfidenceScore = 0,
                    Reasoning = "AI Adjudication Service is not configured (Missing API Key).",
                    AnalysisDetails = "Please configure 'AIAdjudicationSettings:GroqApiKey' in appsettings.json."
                };
            }

            try
            {
                var prompt = BuildPrompt(claim, dischargeSummaryText);
                
                var requestBody = new
                {
                    model = _model,
                    messages = new[]
                    {
                        new { role = "system", content = "You are a professional medical insurance claims adjudicator. Your task is to analyze the claim against policy coverage rules and provide a structured JSON response. You are precise, critical, and objective." },
                        new { role = "user", content = prompt }
                    },
                    response_format = new { type = "json_object" },
                    temperature = 0.1
                };

                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.groq.com/openai/v1/chat/completions");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    return new AIAdjudicationResult
                    {
                        IsApprovedRecommendation = false,
                        ConfidenceScore = 0,
                        Reasoning = $"AI Service Error: {response.StatusCode}",
                        AnalysisDetails = errorBody
                    };
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("\n--- GROQ AI RESPONSE ---\n" + responseContent + "\n---------------------\n");
                var jsonResponse = JsonDocument.Parse(responseContent);
                var aiContent = jsonResponse.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

                if (string.IsNullOrEmpty(aiContent))
                {
                    throw new Exception("Empty response from AI service.");
                }

                // Robust parsing of the AI's internal JSON content
                using var aiJsonDoc = JsonDocument.Parse(aiContent);
                var root = aiJsonDoc.RootElement;

                string decision = "";
                if (root.TryGetProperty("decision", out var dProp)) decision = dProp.GetString() ?? "";

                int confidence = 0;
                if (root.TryGetProperty("confidenceScore", out var cProp))
                {
                    if (cProp.ValueKind == JsonValueKind.Number) confidence = cProp.GetInt32();
                    else if (cProp.ValueKind == JsonValueKind.String && int.TryParse(cProp.GetString(), out var cValue)) confidence = cValue;
                }

                string reasoning = "";
                if (root.TryGetProperty("reasoning", out var rProp)) reasoning = rProp.GetString() ?? "";

                string technicalAnalysis = "";
                if (root.TryGetProperty("technicalAnalysis", out var tProp))
                {
                    if (tProp.ValueKind == JsonValueKind.String) technicalAnalysis = tProp.GetString() ?? "";
                    else technicalAnalysis = tProp.GetRawText(); // Capture as string even if it's a JSON object/array
                }

                return new AIAdjudicationResult
                {
                    IsApprovedRecommendation = decision.Equals("Approved", StringComparison.OrdinalIgnoreCase),
                    ConfidenceScore = confidence,
                    Reasoning = string.IsNullOrEmpty(reasoning) ? "No reasoning provided by AI." : reasoning,
                    AnalysisDetails = technicalAnalysis
                };
            }
            catch (Exception ex)
            {
                return new AIAdjudicationResult
                {
                    IsApprovedRecommendation = false,
                    ConfidenceScore = 0,
                    Reasoning = $"AI Adjudication Failed: {ex.Message}",
                    AnalysisDetails = ex.ToString()
                };
            }
        }

        public async Task<string> GetEndorsementExplanationAsync(
            decimal adjustment, 
            int remainingDays, 
            string type, 
            string description,
            string billingFrequency,
            decimal recurringChange,
            decimal nextRecurringTotal)
        {
            var fallback = $"An adjustment of INR {adjustment:N2} will be applied for the remaining {remainingDays} days.";
            if (string.IsNullOrEmpty(_apiKey)) return fallback;

            try
            {
                var prompt = $"Explain this premium adjustment to a Corporate HR manager in 1-2 concise, friendly, plain-English sentences: \n" +
                             $"- Action: {type} ({description})\n" +
                             $"- Immediate Adjustment Amount: INR {adjustment:N2}\n" +
                             $"- Proration Period: {remainingDays} days (until the end of the current {billingFrequency} billing cycle)\n" +
                             $"- Billing Frequency: {billingFrequency}\n" +
                             $"- Recurring Premium Change: { (recurringChange >= 0 ? "+" : "-") } INR {Math.Abs(recurringChange):N2}\n" +
                             $"- New Total {billingFrequency} Bill (Starting Next Cycle): INR {nextRecurringTotal:N2}\n\n" +
                             "Rules:\n" +
                             "1. If adjustment is NEGATIVE, use words like 'credit' or 'refund'.\n" +
                             "2. If POSITIVE, use words like 'invoice' or 'addition'.\n" +
                             "3. Always mention the new total for the next cycle.\n" +
                             "4. Use the currency symbol '₹' in the output.\n" +
                             "5. NO MARKDOWN: Do not use bold (**) or headers (###).\n" +
                             "6. NO META-TALK: Return ONLY the explanation text. Do not include 'Here is an explanation...' or similar conversational filler.\n\n" +
                             "Example Output (Monthly ADD): Adding John Doe will generate a one-time adjustment of ₹450 for the remaining 12 days of this month. From next month, your total monthly bill will increase to ₹12,500.\n" +
                             "Example Output (Monthly REMOVE): Removing Jane Smith will result in a credit of ₹320 for the rest of this month. Starting next cycle, your monthly premium will be reduced to ₹11,200.\n";

                var requestBody = new
                {
                    model = _model,
                    messages = new[]
                    {
                        new { role = "system", content = "You are a professional insurance advisor. You explain complex billing changes simply and accurately. Avoid jargon." },
                        new { role = "user", content = prompt }
                    },
                    temperature = 0.5
                };

                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.groq.com/openai/v1/chat/completions");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode) return fallback;

                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonDocument.Parse(responseContent);
                var aiContent = jsonResponse.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

                return aiContent?.Trim() ?? fallback;
            }
            catch
            {
                return fallback;
            }
        }

        private string BuildPrompt(Claim claim, string text)
        {
            var sb = new StringBuilder();
            sb.AppendLine("## CLAIM ADJUDICATION TASK");
            sb.AppendLine("Analyze the following claim details and medical text extracted from the discharge summary. Your goal is to determine if the claim should be approved or rejected based on coverage rules.");
            
            sb.AppendLine("\n### 1. CLAIM DATA (USER SUBMISSION)");
            sb.AppendLine($"- Claim Number: {claim.ClaimNumber}");
            sb.AppendLine($"- Patient Name: {(claim.Dependent != null ? claim.Dependent.FullName : claim.Member?.FullName)}");
            sb.AppendLine($"- Claim Type: {claim.ClaimType}");
            sb.AppendLine($"- Claim Amount: INR {claim.ClaimAmount:N2}");
            sb.AppendLine($"- Reason Stated by User: {claim.ClaimReason}");
            sb.AppendLine($"- Incident Date: {claim.IncidentDate:yyyy-MM-dd}");

            sb.AppendLine("\n### 2. SYSTEM OCR ANCHORS (VERIFIED DATA)");
            sb.AppendLine($"- Extracted Hospital: {claim.ExtractedHospitalName ?? "Unknown"}");
            sb.AppendLine($"- Extracted Bill Date: {claim.ExtractedBillDate?.ToString("yyyy-MM-dd") ?? "Unknown"}");
            sb.AppendLine($"- Extracted Bill Amount: INR {claim.ExtractedBillAmount?.ToString("N2") ?? "Unknown"}");
            sb.AppendLine($"- Extracted Diagnosis/Reason: {claim.ExtractedDiagnosis ?? "Not found"}");
            
            if (claim.ClaimType == CoverageType.Accident)
            {
                sb.AppendLine($"- FIR Number (Police): {claim.ExtractedFirNumber ?? "Not Found"}");
                sb.AppendLine($"- Police Station: {claim.ExtractedPoliceStation ?? "Not Found"}");
            }
            if (claim.ClaimType == CoverageType.Life)
            {
                sb.AppendLine($"- Date of Death: {claim.ExtractedDateOfDeath?.ToString("yyyy-MM-dd") ?? "Unknown"}");
                sb.AppendLine($"- Cause of Death: {claim.ExtractedCauseOfDeath ?? "Unknown"}");
            }

            sb.AppendLine("\n### 3. POLICY COVERAGE RULES");
            if (claim.PolicyAssignment?.InsurancePlan != null)
            {
                var plan = claim.PolicyAssignment.InsurancePlan;
                sb.AppendLine($"- Plan Name: {plan.PlanName}");
                var matchingCoverage = plan.Coverages.FirstOrDefault(c => c.Type == claim.ClaimType);
                if (matchingCoverage != null)
                {
                    sb.AppendLine($"- Maximum Allowed: INR {matchingCoverage.CoverageAmount:N2}");
                    sb.AppendLine($"- Included Beneficiaries: {matchingCoverage.CoveredGroup}");
                }
            }

            sb.AppendLine("\n### 4. MEDICAL TEXT (RAW DATA)");
            sb.AppendLine("--- START ---");
            sb.AppendLine(text.Length > 2000 ? text.Substring(0, 2000) + "..." : text);
            sb.AppendLine("--- END ---");
            
            sb.AppendLine("\n### 5. PRELIMINARY RISK ASSESSMENT");
            if (claim.IsSuspectedFraud)
            {
                sb.AppendLine("[CRITICAL WARNING]: The system has flagged this claim for high fraud risk.");
                sb.AppendLine($"- Risk Reason: {claim.FraudAnalysis}");
            }
            else
            {
                sb.AppendLine("- No automated duplicates or amount mismatches detected.");
            }

            sb.AppendLine("\n### 6. MANDATORY AUDIT CHECKLIST");
            sb.AppendLine("You must perform these 5 checks and report the result for EACH in your 'technicalAnalysis':");
            
            if (claim.ClaimType == CoverageType.Life)
            {
                sb.AppendLine("1. [Identity]: Does the patient name match between form and medical text? (Hospital match is NOT required for Life claims).");
                sb.AppendLine("2. [Temporal]: Does the date of death match the medical notes?");
                sb.AppendLine("3. [Medical]: Is the cause of death clearly stated and consistent with policy scope?");
                sb.AppendLine("4. [Financial]: OK (Skip check: Bill amount is not applicable for lump-sum Life benefits).");
                sb.AppendLine("5. [Policy]: Does the cause of death fall within the plan's coverage scope?");
            }
            else
            {
                sb.AppendLine("1. [Identity]: Does the patient name and hospital match between form and medical text?");
                sb.AppendLine("2. [Temporal]: Does the treatment date ({claim.IncidentDate:yyyy-MM-dd}) match the medical notes?");
                sb.AppendLine("3. [Medical]: Is the treatment provided medically necessary for the diagnosed condition?");
                sb.AppendLine("4. [Financial]: Is the bill amount (INR {claim.ClaimAmount:N2}) consistent with the procedures described?");
                sb.AppendLine("5. [Policy]: Does the claim fall within the plan's coverage scope?");
            }

            sb.AppendLine("\n### 7. FINAL VERDICT RULES");
            if (claim.ClaimType == CoverageType.Life)
            {
                sb.AppendLine("- DO NOT REJECT based on 'Hospital mismatch' or 'Unknown Bill Amount'. These are irrelevant for Life claims.");
                sb.AppendLine("- REJECT if Patient Name is a different individual.");
                sb.AppendLine("- REJECT if Cause of Death is explicitly listed as an exclusion (e.g., suicide within 1yr if per-policy).");
            }
            else
            {
                sb.AppendLine("- REJECT immediately if [CRITICAL WARNING] is present and text does not provide a 100% convincing medical override.");
                sb.AppendLine("- REJECT if any Date Mismatch is > 48 hours.");
                sb.AppendLine("- REJECT if hospital name in text is DIFFERENT from the claimed hospital.");
            }

            sb.AppendLine("\nReturn ONLY a JSON object with this exact structure (The 'reasoning' MUST be a comprehensive list of ALL findings, separated by semicolons):");
            sb.AppendLine("{");
            sb.AppendLine("  \"decision\": \"Approved\" or \"Rejected\",");
            sb.AppendLine("  \"confidenceScore\": 0 to 100,");
            sb.AppendLine("  \"reasoning\": \"Detailed summary listing ALL primary reasons/findings for the decision\",");
            sb.AppendLine("  \"technicalAnalysis\": \"Markdown Checklist: \\n- [Identity]: OK/Fail...\\n- [Temporal]: OK/Fail...\\n[Full Forensic Report listing all contradictions]\"");
            sb.AppendLine("}");
            
            return sb.ToString();
        }

        public async Task<string> GetClaimRejectionExplanationAsync(string rawReason, string claimType, decimal amount)
        {
            var fallback = $"[SYSTEM FALLBACK]: We noticed a discrepancy in your {claimType} claim for ₹{amount:N2}. Our team is conducting a manual review of the submitted documentation to ensure all policy standards are met.";
            if (string.IsNullOrEmpty(_apiKey)) return fallback;

            try
            {
                             var prompt = $"DATA INPUT (DO NOT IGNORE)\n" +
                             $"- Member Requested: ₹{amount:N2}\n" +
                             $"- Audit Finding: \"{rawReason}\"\n" +
                             $"- Context: {claimType} Claim\n\n" +
                             $"ADJUDICATION MISSION\n" +
                             $"You are a Member Success Advocate. Your job is to take the dry 'Audit Finding' and bridge the gap with the 'Member Requested' amount. You must provide a crystalline explanation that allows a non-expert to see exactly why their request was not approved.\n\n" +
                             $"TRANSFORMATION MANDATE:\n" +
                             $"1. SHOW THE NUMBERS: If 'mismatch' or 'over-limit' is mentioned, explain the math.\n" +
                             $"   - Mandatory Pattern: 'While your request was for ₹{amount:N2}, our validation of the submitted documentation shows [Your interpretation of finding].'\n" +
                             $"   - Currency Rule: ALWAYS use the Rupee symbol '₹' when mentioning amounts. Do not use '?' or other placeholders.\n" +
                             $"2. NO AUDITOR MENTIONS: Do not say 'The system' or 'The auditor'. Speak as the insurance company humanely ('We noticed...', 'Our review process shows...').\n" +
                             $"3. EXPLAIN FAKE/INVALID DATA: If fraud or fake documents are mentioned, do not use the word 'Fake'. Say: 'We encountered significant challenges in validating the authenticity of the records provided, which are essential for processing benefits safely for all members.'\n" +
                             $"4. CLARITY OVER BREVITY: Be descriptive.\n" +
                             $"5. FORCE PLAIN TEXT: Do NOT use any special characters like #, *, _, or -. No markdown headers. No bolding. Use only white space and ALL CAPS for section titles.\n\n" +
                             $"REPLY STRUCTURE:\n" +
                             $"HELLO AND THANK YOU FOR REACHING OUT\n" +
                             $"(Brief empathetic Greeting)\n\n" +
                             $"UNDERSTANDING THE DECISION\n" +
                             $"(2-3 sentences explaining the gap between the ₹{amount:N2} request and findings.)\n\n" +
                             $"PATH TO RESOLUTION\n" +
                             $"1. [TITLE]: [Actionable advice]\n" +
                             $"2. [TITLE]: [Next step for the member]\n" +
                             $"3. SUPPORT STEP: [Invitation to contact support]";

                var requestBody = new
                {
                    model = _model,
                    messages = new[]
                    {
                        new { role = "system", content = "You are a professional Insurance Member Advocate. Your job is to translate technical rejection codes into clear, data-driven, and supportive explanations that help members understand the EXACT reason for a mismatch." },
                        new { role = "user", content = prompt }
                    },
                    temperature = 0.3
                };

                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.groq.com/openai/v1/chat/completions");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode) 
                {
                    Console.WriteLine($"[GROQ ERROR]: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return fallback;
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("\n--- GROQ EXPLANATION RESPONSE ---\n" + responseContent + "\n---------------------\n");
                var jsonResponse = JsonDocument.Parse(responseContent);
                var aiContent = jsonResponse.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

                if (string.IsNullOrWhiteSpace(aiContent)) return fallback;
                return aiContent.Trim();
            }
            catch
            {
                return fallback;
            }
        }

        public async Task<(string Analysis, int ConfidenceScore)> AnalyzeKYBDocumentsAsync(CorporateClient client, EGI_Backend.Domain.Entities.CorporateClientDocument document)
        {
            if (string.IsNullOrEmpty(_apiKey)) return ("AI KYB Intelligence Disabled.", 50);

            try
            {
                var ext = Path.GetExtension(document.FileName)?.ToLowerInvariant();
                if (ext == ".pdf")
                {
                    if (!File.Exists(document.FilePath))
                    {
                        return ("DOCUMENT INTELLIGENCE FAILED: File missing from storage.", 0);
                    }

                    try 
                    {
                        using var pdfDoc = UglyToad.PdfPig.PdfDocument.Open(document.FilePath);
                        var sb = new StringBuilder();
                        foreach (var page in pdfDoc.GetPages())
                        {
                            sb.AppendLine(page.Text);
                        }
                        var extractedText = sb.ToString().Trim();

                        if (string.IsNullOrEmpty(extractedText))
                        {
                            return ("DOCUMENT INTELLIGENCE LIMITED: Document is a scanned PDF (Image-based). Please manually verify details.", 50);
                        }

                        // We can now pass this text to a text model or the vision model as text-only
                        var pdfPrompt = $"KYB DOCUMENT PRE-SCREENING TASK (PDF EXTRACTED TEXT)\n" +
                                     $"We are onboarding a new corporate client for Employee Group Insurance.\n\n" +
                                     $"1. USER-SUBMITTED DATA:\n" +
                                     $"- Company Name: {client.CompanyName}\n" +
                                     $"- Address: {client.Address}\n" +
                                     $"- Document Type Uploaded: {document.DocumentType}\n\n" +
                                     $"2. EXTRACTED PDF TEXT:\n" +
                                     $"{ (extractedText.Length > 2000 ? extractedText.Substring(0, 2000) + "..." : extractedText) }\n\n" +
                                     $"3. YOUR GOAL:\n" +
                                     $"Analyze the extracted text and cross-check the details against the User-Submitted Data.\n\n" +
                                     $"4. OUTPUT MANDATE:\n" +
                                     $"Analyze the extracted text with EXTREME SKEPTICISM. Compare the Company Name and Address character-for-character.\n" +
                                     $"\n" +
                                     $"ZERO TOLERANCE RULE: If the Company Name in the document is DIFFERENT from '{client.CompanyName}' or the Address does NOT match '{client.Address}', you MUST output RECOMMENDATION: Reject.\n" +
                                     $"\n" +
                                     $"FORBIDDEN PHRASES: Do NOT say 'minor discrepancies', 'can be verified manually', or 'similar enough'. If it doesn't match perfectly, it is a FATAL ERROR and must be REJECTED.\n" +
                                     $"\n" +
                                     $"NO MARKDOWN: Do NOT use bolding (**), headers (###), or asterisks (*).\n" +
                                     $"\n" +
                                     $"Use the following EXACT format (No Markdown):\n" +
                                     $"SCORE: [0-40 if mismatched, 90-100 if matching]\n" +
                                     $"SUMMARY: [Identify the exact mismatch and state documents are invalid]\n" +
                                     $"RECOMMENDATION: Reject - [Reason: MANDATORY REJECT due to Name/Address Mismatch]\n" +
                                     $"DISCREPANCIES: [Detailed list of mismatches]";

                        var pdfReqBody = new
                        {
                            model = "llama-3.1-8b-instant",
                            messages = new[]
                            {
                                new { role = "system", content = "You are an expert KYC/KYB AI Document Agent. You extract data accurately and flag discrepancies securely. You respond in plain text ONLY. Never use markdown like asterisks or bolding." },
                                new { role = "user", content = pdfPrompt }
                            },
                            temperature = 0.1,
                            max_tokens = 300
                        };

                        var pdfRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.groq.com/openai/v1/chat/completions");
                        pdfRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                        pdfRequest.Content = new StringContent(JsonSerializer.Serialize(pdfReqBody), Encoding.UTF8, "application/json");

                        var pdfResponse = await _httpClient.SendAsync(pdfRequest);
                        if (!pdfResponse.IsSuccessStatusCode)
                        {
                             return ("DOCUMENT INTELLIGENCE FAILED: AI Provider Error (Text Mode).", 40);
                        }

                        var pdfResponseContent = await pdfResponse.Content.ReadAsStringAsync();
                        var pdfJsonResponse = JsonDocument.Parse(pdfResponseContent);
                        var pdfAiContent = pdfJsonResponse.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "";

                        int pdfScore = 50;
                        var pLines = pdfAiContent.Split('\n');
                        var pScoreLine = pLines.FirstOrDefault(l => l.StartsWith("SCORE:"));
                        if (pScoreLine != null)
                        {
                            var sStr = pScoreLine.Replace("SCORE:", "").Trim();
                            if (int.TryParse(sStr, out int pParsed)) pdfScore = pParsed;
                        }

                        return (pdfAiContent.Trim(), pdfScore);
                    }
                    catch (Exception ex)
                    {
                        return ($"DOCUMENT INTELLIGENCE LIMITED: Could not read PDF text ({ex.Message}). Manual verification required.", 50);
                    }
                }

                if (!File.Exists(document.FilePath))
                {
                    return ("DOCUMENT INTELLIGENCE FAILED: File missing from storage.", 0);
                }

                var fileBytes = await File.ReadAllBytesAsync(document.FilePath);
                var base64Image = Convert.ToBase64String(fileBytes);
                var mimeType = ext == ".png" ? "image/png" : "image/jpeg";

                var prompt = $"KYB DOCUMENT PRE-SCREENING TASK\n" +
                             $"We are onboarding a new corporate client for Employee Group Insurance.\n\n" +
                             $"1. USER-SUBMITTED DATA:\n" +
                             $"- Company Name: {client.CompanyName}\n" +
                             $"- Address: {client.Address}\n" +
                             $"- Document Type Uploaded: {document.DocumentType}\n\n" +
                             $"2. YOUR GOAL:\n" +
                             $"Analyze the attached image and cross-check the extracted details against the User-Submitted Data. Look for matches in Company Name and Registration structures.\n\n" +
                             $"3. OUTPUT MANDATE:\n" +
                             $"Analyze the image with EXTREME SKEPTICISM. Look for the Company Name and Address.\n" +
                             $"\n" +
                             $"ZERO TOLERANCE RULE: If the Company Name in the image is DIFFERENT from '{client.CompanyName}' or the Address does NOT match '{client.Address}', you MUST output RECOMMENDATION: Reject.\n" +
                             $"\n" +
                             $"FORBIDDEN PHRASES: Do NOT say 'minor discrepancies', 'can be verified manually', or 'similar enough'. If it doesn't match perfectly, it is a FATAL ERROR and must be REJECTED.\n" +
                             $"\n" +
                             $"NO MARKDOWN: Do NOT use bolding (**), headers (###), or asterisks (*).\n" +
                             $"\n" +
                             $"Use the following EXACT format (No Markdown):\n" +
                             $"SCORE: [0-40 if mismatched, 90-100 if matching]\n" +
                             $"SUMMARY: [Identify the exact mismatch and state documents are invalid]\n" +
                             $"RECOMMENDATION: Reject - [Reason: MANDATORY REJECT due to Name/Address Mismatch]\n" +
                             $"DISCREPANCIES: [Detailed list of mismatches]";

                var requestBody = new
                {
                    model = "llama-3.2-11b-vision-preview",
                    messages = new object[]
                    {
                        new { role = "system", content = "You are an expert KYC/KYB AI Document Agent. You extract data accurately and flag discrepancies securely. You respond in plain text ONLY. Never use markdown like asterisks or bolding." },
                        new 
                        { 
                            role = "user", 
                            content = new object[] 
                            { 
                                new { type = "text", text = prompt },
                                new { type = "image_url", image_url = new { url = $"data:{mimeType};base64,{base64Image}" } }
                            } 
                        }
                    },
                    temperature = 0.1,
                    max_tokens = 300
                };

                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.groq.com/openai/v1/chat/completions");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                     Console.WriteLine($"[KYB VISION ERROR]: {await response.Content.ReadAsStringAsync()}");
                     return ("DOCUMENT INTELLIGENCE FAILED: AI Vision Provider Error.", 40);
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonDocument.Parse(responseContent);
                var aiContent = jsonResponse.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "";

                int score = 50;
                var lines = aiContent.Split('\n');
                var scoreLine = lines.FirstOrDefault(l => l.StartsWith("SCORE:"));
                if (scoreLine != null)
                {
                    var scoreStr = scoreLine.Replace("SCORE:", "").Trim();
                    if (int.TryParse(scoreStr, out int parsedScore))
                    {
                        score = parsedScore;
                    }
                }

                return (aiContent.Trim(), score);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[KYB VISION EXCEPTION]: {ex.Message}");
                return ("DOCUMENT INTELLIGENCE FAILED: Internal Error during Vision Processing.", 30);
            }
        }
    }
}

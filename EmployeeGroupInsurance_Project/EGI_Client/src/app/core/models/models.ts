// ─── Enums (Matching Backend) ──────────────────────────────
export enum PolicyStatus { Active = 'Active', Inactive = 'Inactive', Expired = 'Expired' }
export enum InvoiceStatus { Pending = 'Pending', Paid = 'Paid', Overdue = 'Overdue', PartiallyPaid = 'PartiallyPaid' }
export enum ClaimStatus { Pending = 'Pending', Approved = 'Approved', Rejected = 'Rejected', InReview = 'InReview' }
export enum BusinessCategory { Small = 'Small', Medium = 'Medium', Large = 'Large', Enterprise = 'Enterprise' }

export enum RelationshipType {
    Spouse = 1,
    Child = 2,
    Father = 3,
    Mother = 4,
    Other = 5
}

export enum DocumentType {
    GSTIN = 1,
    PAN = 2,
    CIN = 3,
    AddressProof = 4,
    Other = 5
}

export enum ClaimDocumentType {
    MedicalBill = 1,
    HospitalDischargeReport = 2,
    DoctorPrescription = 3,
    FIR = 4,
    PostMortemReport = 5,
    DeathCertificate = 6,
    AccidentReport = 7,
    DiagnosisReport = 8,
    InsuranceClaimForm = 9,
    Other = 10
}

export enum BillingFrequency {
    Monthly = 1,
    Annually = 2
}

export enum EndorsementType {
    AddMember = 0,
    RemoveMember = 1,
    AddDependent = 2,
    RemoveDependent = 3,
    ChangeCoverage = 4
}

export enum PaymentMethod {
    Card = 1,
    NetBanking = 2,
    UPI = 3,
    Wallet = 4
}

export enum Gender {
    Male = 1,
    Female = 2,
    Other = 3
}

export enum IndustryType {
    IT = 0,
    Banking = 1,
    Education = 2,
    Others = 3,
    Retail = 4,
    Healthcare = 5,
    Hospitality = 6,
    Logistics = 7,
    Manufacturing = 8,
    Construction = 9,
    OilGas = 10,
    Mining = 11
}

// ─── Core Interfaces ───────────────────────────────────────
export interface UserResponse {
    id: string;
    name: string;
    email: string;
    role: string;
    status: string;
    commissionEarned?: number;
    salaryLPA?: number;
}

export interface InsurancePlan {
    id: string;
    planCode: string;
    planName: string;
    description: string;
    basePremium: number;
    status: boolean;
    hasHealthCheckup: boolean;
    coverages: Coverage[];
}

export interface Coverage {
    type: string;
    coverageAmount: number;
    coveredGroup: string;
}

export interface PolicyAssignment {
    id: string;
    policyNo: string;
    corporateClientId: string;
    clientName?: string;
    insurancePlanId: string;
    planName?: string;
    agentId: string;
    startDate: string;
    endDate: string;
    status: PolicyStatus;
    annualPremium: number;
    totalPremium: number;
    commissionAmount: number;
    billingFrequency: string;
    businessCategory: BusinessCategory;
}

export interface Invoice {
    id: string;
    invoiceNo: string;
    amount: number;
    totalPaid: number;
    status: InvoiceStatus;
    dueDate: string;
    billingPeriodFrom: string;
    billingPeriodTo: string;
}

export interface Claim {
    id: string;
    claimNumber: string;
    claimType: string;
    claimAmount: number;
    claimDate: string;
    status: ClaimStatus;
    memberId: string;
    memberName?: string;
    dependentId?: string;
    dependentName?: string;
    rejectionReason?: string;
    isCashless?: boolean;
    hospitalId?: string;
    hospitalName?: string;
}

export interface Hospital {
    id: string;
    name: string;
    address: string;
    city: string;
    contactPerson: string;
    contactNumber: string;
    isNetworkHospital: boolean;
    googleMapsUrl?: string;
    latitude?: number;
    longitude?: number;
    specialties?: string;
}

export interface CorporateClientDocumentDto {
    id: string;
    documentType: string;
    fileName: string;
    fileUrl: string;
}

export interface CorporateClientResponseDto {
    id: string;
    companyName: string;
    address: string;
    status: string;
    isBlocked: boolean;
    email: string;
    phone: string;
    industryType: number;
    kybAiAnalysis?: string;
    kybAiConfidenceScore?: number;
    KybAiAnalysis?: string; // Adding PascalCase variant just in case of casing issues
    KybAiConfidenceScore?: number;
    documents: CorporateClientDocumentDto[];
    lastHealthCheckupDate?: string;
    healthCheckupHospitalId?: string;
    healthCheckupHospitalName?: string;
    healthCheckupActualMemberCount?: number;
    healthCheckupActualDependentCount?: number;
    isHealthCheckupClaimPending?: boolean;
    healthCheckupVerifiedAt?: string;
}



// ─── Enums (Matching Backend) ──────────────────────────────
export enum PolicyStatus { Active = 'Active', Inactive = 'Inactive', Expired = 'Expired' }
export enum InvoiceStatus { Pending = 'Pending', Paid = 'Paid', Overdue = 'Overdue', PartiallyPaid = 'PartiallyPaid' }
export enum ClaimStatus { Pending = 'Pending', Approved = 'Approved', Rejected = 'Rejected', InReview = 'InReview' }
export enum BusinessCategory { Small = 'Small', Medium = 'Medium', Large = 'Large', Enterprise = 'Enterprise' }

// ─── Core Interfaces ───────────────────────────────────────
export interface UserResponse {
    id: string;
    name: string;
    email: string;
    role: string;
    status: string;
    commissionEarned?: number;
}

export interface InsurancePlan {
    id: string;
    planCode: string;
    planName: string;
    description: string;
    basePremium: number;
    status: boolean;
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
    rejectionReason?: string;
}

import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

export const API_BASE = environment.apiBase;

import { InsurancePlan, PolicyAssignment, Claim, UserResponse, Invoice } from '../core/models/models';

@Injectable({ providedIn: 'root' })
export class AdminService {
    private http = inject(HttpClient);

    getSummary(): Observable<any> { return this.http.get(`${API_BASE}/admin/dashboard/summary`); }
    getPendingClients(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/admin/dashboard/pending-clients`); }
    getAllClients(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/admin/dashboard/clients`); }
    approveClient(id: string, dto: any): Observable<any> { return this.http.post(`${API_BASE}/admin/dashboard/approve-client/${id}`, dto); }

    getAllPlans(): Observable<InsurancePlan[]> { return this.http.get<InsurancePlan[]>(`${API_BASE}/admin/dashboard/insurance-plans`); }
    getPlanById(id: string): Observable<InsurancePlan> { return this.http.get<InsurancePlan>(`${API_BASE}/admin/dashboard/insurance-plans/${id}`); }
    createPlan(dto: any): Observable<any> { return this.http.post(`${API_BASE}/admin/dashboard/insurance-plans`, dto); }
    updatePlan(id: string, dto: any): Observable<any> { return this.http.put(`${API_BASE}/admin/dashboard/insurance-plans/${id}`, dto); }
    deactivatePlan(id: string): Observable<any> { return this.http.delete(`${API_BASE}/admin/dashboard/insurance-plans/${id}/deactivate`); }
    deletePlan(id: string): Observable<any> { return this.http.delete(`${API_BASE}/admin/dashboard/insurance-plans/${id}`); }

    getStaff(role: string): Observable<UserResponse[]> { return this.http.get<UserResponse[]>(`${API_BASE}/admin/dashboard/staff/${role}`); }
    createStaff(dto: any): Observable<any> { return this.http.post(`${API_BASE}/auth/admin/create-staff`, dto); }
    toggleUserStatus(userId: string): Observable<any> { return this.http.post(`${API_BASE}/admin/dashboard/toggle-user-status/${userId}`, {}); }

    getPolicyAssignments(): Observable<PolicyAssignment[]> { return this.http.get<PolicyAssignment[]>(`${API_BASE}/admin/dashboard/policy-assignments`); }
    getClaims(): Observable<Claim[]> { return this.http.get<Claim[]>(`${API_BASE}/admin/dashboard/claims`); }
    getClaimDetail(claimId: string): Observable<any> { return this.http.get(`${API_BASE}/admin/dashboard/claims/${claimId}/detail`); }
    getPendingEndorsements(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/admin/dashboard/endorsements/pending`); }
    getEndorsementsByPolicy(policyId: string): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/admin/dashboard/endorsements/policy/${policyId}`); }

    getInvoiceDetail(invoiceId: string): Observable<Invoice> { return this.http.get<Invoice>(`${API_BASE}/admin/dashboard/invoices/${invoiceId}`); }
    getPayments(invoiceId: string): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/admin/dashboard/invoices/${invoiceId}/payments`); }
    getInvoicesByPolicy(policyId: string): Observable<Invoice[]> { return this.http.get<Invoice[]>(`${API_BASE}/admin/dashboard/invoices/policy/${policyId}`); }

    getAuditLogs(userId?: string, entityName?: string): Observable<any[]> {
        return this.http.get<any[]>(`${API_BASE}/admin/dashboard/audit-logs?userId=${userId || ''}&entityName=${entityName || ''}`);
    }

}

@Injectable({ providedIn: 'root' })
export class AgentService {
    private http = inject(HttpClient);

    getSummary(): Observable<any> { return this.http.get(`${API_BASE}/agent/dashboard/summary`); }
    getMyCustomers(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/agent/dashboard/my-customers`); }
    getMyPolicies(): Observable<PolicyAssignment[]> { return this.http.get<PolicyAssignment[]>(`${API_BASE}/agent/dashboard/my-policies`); }

    createCustomer(formData: FormData): Observable<any> { return this.http.post(`${API_BASE}/agent/dashboard/create-customer`, formData); }

    getPendingEndorsements(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/agent/dashboard/pending-endorsements`); }
    getEndorsementsByPolicy(policyId: string): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/agent/dashboard/endorsements/policy/${policyId}`); }
    reviewEndorsement(id: string, dto: any): Observable<any> { return this.http.post(`${API_BASE}/agent/dashboard/review-endorsement/${id}`, dto); }
    getCommissions(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/agent/dashboard/commissions`); }

}

@Injectable({ providedIn: 'root' })
export class CustomerService {
    private http = inject(HttpClient);

    getSummary(): Observable<any> { return this.http.get(`${API_BASE}/customer/dashboard/summary`); }
    getOverview(): Observable<any> { return this.http.get(`${API_BASE}/customer/dashboard/overview`); }
    getMyPolicies(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/my-policies`); }
    getMyMembers(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/my-members`); }
    getMyClaims(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/my-claims`); }
    getMyInvoices(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/my-invoices`); }
    getMyEndorsements(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/my-endorsements`); }
    getProfile(): Observable<any> { return this.http.get(`${API_BASE}/customer/dashboard/profile`); }

    completeProfile(dto: any): Observable<any> { return this.http.post(`${API_BASE}/customer/dashboard/complete-profile`, dto); }
    uploadDocument(formData: FormData): Observable<any> { return this.http.post(`${API_BASE}/customer/dashboard/upload-document`, formData); }
    submitClaim(formData: FormData): Observable<any> { return this.http.post(`${API_BASE}/customer/dashboard/submit-claim`, formData); }
    submitEndorsement(dto: any): Observable<any> { return this.http.post(`${API_BASE}/customer/dashboard/submit-endorsement`, dto); }
    uploadMembersExcel(formData: FormData): Observable<any> { return this.http.post(`${API_BASE}/customer/dashboard/upload-members`, formData); }

    payInvoice(invoiceId: string, dto: any): Observable<any> { return this.http.post(`${API_BASE}/customer/dashboard/pay-invoice/${invoiceId}`, dto); }
    getAllPlans(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/insurance-plans`); }
    getPlanById(id: string): Observable<any> { return this.http.get(`${API_BASE}/customer/dashboard/insurance-plans/${id}`); }

    getEndorsementsByPolicy(policyId: string): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/endorsements/policy/${policyId}`); }
    getInvoiceDetail(invoiceId: string): Observable<any> { return this.http.get(`${API_BASE}/customer/dashboard/invoices/${invoiceId}`); }
    getPayments(invoiceId: string): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/invoices/${invoiceId}/payments`); }
    getInvoicesByPolicy(policyId: string): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/invoices/policy/${policyId}`); }
    getClaimsByPolicy(policyId: string): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/claims/policy/${policyId}`); }
}

@Injectable({ providedIn: 'root' })
export class ClaimsOfficerService {
    private http = inject(HttpClient);

    getSummary(): Observable<any> { return this.http.get(`${API_BASE}/claims-officer/dashboard/summary`); }
    getPendingClaims(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/claims-officer/dashboard/pending-claims`); }
    reviewClaim(claimId: string, dto: any): Observable<any> { return this.http.post(`${API_BASE}/claims-officer/dashboard/review/${claimId}`, dto); }
    getClaimDetail(claimId: string): Observable<any> { return this.http.get(`${API_BASE}/claims-officer/dashboard/claims/${claimId}/detail`); }

    getMemberCoverageSummary(memberId: string): Observable<any> { return this.http.get(`${API_BASE}/claims-officer/dashboard/coverage-summary/member/${memberId}`); }
    getDependentCoverageSummary(memberId: string, dependentId: string): Observable<any> { return this.http.get(`${API_BASE}/claims-officer/dashboard/coverage-summary/member/${memberId}/dependent/${dependentId}`); }
    getHistory(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/claims-officer/dashboard/history`); }
    takeClaim(id: string): Observable<any> { return this.http.post(`${API_BASE}/claims-officer/dashboard/claims/${id}/take`, {}); }
    releaseClaim(id: string): Observable<any> { return this.http.post(`${API_BASE}/claims-officer/dashboard/claims/${id}/release`, {}); }

    getInvoicesByPolicy(policyId: string): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/claims-officer/dashboard/invoices/policy/${policyId}`); }
    getAllPlans(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/claims-officer/dashboard/insurance-plans`); }
    getPlanById(id: string): Observable<any> { return this.http.get(`${API_BASE}/claims-officer/dashboard/insurance-plans/${id}`); }
    getClaimsByPolicy(policyId: string): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/claims-officer/dashboard/claims/policy/${policyId}`); }
    getClaimsByMember(memberId: string): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/claims-officer/dashboard/claims/member/${memberId}/history`); }
}

@Injectable({ providedIn: 'root' })
export class PublicService {
    private http = inject(HttpClient);
    getInsurancePlans(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/public/insurance-plans`); }
}

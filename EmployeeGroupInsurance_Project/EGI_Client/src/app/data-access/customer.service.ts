import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

const API_BASE = environment.apiBase;

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
    getEndorsementPreview(dto: any): Observable<any> { return this.http.post(`${API_BASE}/customer/dashboard/preview-endorsement`, dto); }
    uploadMembersExcel(formData: FormData): Observable<any> { return this.http.post(`${API_BASE}/customer/dashboard/upload-members`, formData); }
    renewPolicy(policyId: string, years: number, billingFrequency: number): Observable<any> { 
        return this.http.post(`${API_BASE}/customer/dashboard/renew-policy/${policyId}`, { years, billingFrequency }); 
    }
    getRenewalQuote(policyId: string, years: number, frequency: number): Observable<any> { 
        return this.http.get(`${API_BASE}/customer/dashboard/renewal-quote/${policyId}?years=${years}&frequency=${frequency}`); 
    }

    payInvoice(invoiceId: string, dto: any): Observable<any> { return this.http.post(`${API_BASE}/customer/dashboard/pay-invoice/${invoiceId}`, dto); }
    getAllPlans(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/insurance-plans`); }
    getPlanById(id: string): Observable<any> { return this.http.get(`${API_BASE}/customer/dashboard/insurance-plans/${id}`); }

    getEndorsementsByPolicy(policyId: string): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/endorsements/policy/${policyId}`); }
    getInvoiceDetail(invoiceId: string): Observable<any> { return this.http.get(`${API_BASE}/customer/dashboard/invoices/${invoiceId}`); }
    getPayments(invoiceId: string): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/invoices/${invoiceId}/payments`); }
    getInvoicesByPolicy(policyId: string): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/invoices/policy/${policyId}`); }
    getClaimsByPolicy(policyId: string): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/customer/dashboard/claims/policy/${policyId}`); }
    getClaimRejectionExplanation(claimId: string): Observable<any> { return this.http.get(`${API_BASE}/customer/dashboard/claims/${claimId}/rejection-explanation`); }
}

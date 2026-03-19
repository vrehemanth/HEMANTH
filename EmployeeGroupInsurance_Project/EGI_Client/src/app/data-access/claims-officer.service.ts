import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

const API_BASE = environment.apiBase;

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
    runAI(claimId: string): Observable<any> { return this.http.post(`${API_BASE}/claims-officer/dashboard/claims/${claimId}/run-ai`, {}); }
}

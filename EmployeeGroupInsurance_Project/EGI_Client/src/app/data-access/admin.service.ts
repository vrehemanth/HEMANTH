import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { InsurancePlan, PolicyAssignment, Claim, UserResponse, Invoice } from '../core/models/models';

const API_BASE = environment.apiBase;

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

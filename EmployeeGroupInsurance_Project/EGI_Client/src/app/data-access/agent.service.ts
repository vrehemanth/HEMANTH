import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { PolicyAssignment } from '../core/models/models';

const API_BASE = environment.apiBase;

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

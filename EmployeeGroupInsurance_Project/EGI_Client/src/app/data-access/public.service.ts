import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

const API_BASE = environment.apiBase;

@Injectable({ providedIn: 'root' })
export class PublicService {
    private http = inject(HttpClient);
    getInsurancePlans(): Observable<any[]> { return this.http.get<any[]>(`${API_BASE}/public/insurance-plans`); }
}

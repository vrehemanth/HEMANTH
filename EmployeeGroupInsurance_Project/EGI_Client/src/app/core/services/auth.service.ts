import { Injectable, signal, inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';

const API_BASE = environment.apiBase;

export type UserRole = 'Admin' | 'Agent' | 'Customer' | 'ClaimsOfficer';

export interface AuthUser {
    token: string;
    role: UserRole;
    email: string;
    name?: string;
    requirePasswordChange?: boolean;
    isProfileCompleted: boolean;
}

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private http = inject(HttpClient);
    private router = inject(Router);

    currentUser = signal<AuthUser | null>(null);

    constructor() {
        this.loadUserFromStorage();
    }

    private loadUserFromStorage() {
        if (typeof window !== 'undefined' && localStorage) {
            const token = localStorage.getItem('jwt');
            const role = localStorage.getItem('role') as UserRole;
            const email = localStorage.getItem('email');
            const name = localStorage.getItem('name') || '';

            if (token && role && email) {
                const isProfileCompleted = localStorage.getItem('isProfileCompleted') === 'true';
                const requirePasswordChange = localStorage.getItem('requirePasswordChange') === 'true';
                this.currentUser.set({ token, role, email, name, isProfileCompleted, requirePasswordChange });
            }
        }
    }

    register(data: any): Observable<any> {
        return this.http.post<any>(`${API_BASE}/Auth/register`, data).pipe(
            tap(res => {
                const responseData = res.data ? res.data : res;
                if (responseData.token) {
                    const payload = this.decodeToken(responseData.token);
                    const role = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || payload.role;
                    const email = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'] || payload.email || data.email;
                    const name = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || payload.name || '';

                    const user: AuthUser = {
                        token: responseData.token,
                        role: role as UserRole,
                        email,
                        name,
                        isProfileCompleted: responseData.isProfileCompleted || false
                    };

                    if (typeof window !== 'undefined' && localStorage) {
                        localStorage.setItem('jwt', user.token);
                        localStorage.setItem('role', user.role);
                        localStorage.setItem('email', user.email);
                        localStorage.setItem('name', user.name || '');
                        localStorage.setItem('isProfileCompleted', String(user.isProfileCompleted));
                        localStorage.setItem('requirePasswordChange', String(user.requirePasswordChange || false));
                    }

                    this.currentUser.set(user);
                }
            })
        );
    }

    login(credentials: { email: string; password: string }): Observable<any> {
        return this.http.post<any>(`${API_BASE}/Auth/login`, credentials).pipe(
            tap(res => {
                const data = res.data ? res.data : res;

                if (data.token) {
                    const payload = this.decodeToken(data.token);

                    // Claim mappings based on .NET standards
                    const role = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || payload.role;
                    const email = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'] || payload.email || credentials.email;
                    const name = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || payload.name || '';

                    const user: AuthUser = {
                        token: data.token,
                        role: role as UserRole,
                        email,
                        name,
                        requirePasswordChange: data.requirePasswordChange,
                        isProfileCompleted: data.isProfileCompleted
                    };

                    if (typeof window !== 'undefined' && localStorage) {
                        localStorage.setItem('jwt', user.token);
                        localStorage.setItem('role', user.role);
                        localStorage.setItem('email', user.email);
                        localStorage.setItem('name', user.name || '');
                        localStorage.setItem('isProfileCompleted', String(user.isProfileCompleted));
                        localStorage.setItem('requirePasswordChange', String(user.requirePasswordChange || false));
                    }

                    this.currentUser.set(user);

                    if (user.requirePasswordChange) {
                        this.router.navigate(['/settings/change-password']);
                    } else {
                        this.navigateByRole(user.role, user.isProfileCompleted);
                    }
                } else {
                    console.error("Login succeeded but no token was found in the response.", res);
                }
            })
        );
    }

    logout() {
        this.clearSession();
        this.router.navigate(['/auth/login']);
    }

    clearSession() {
        if (typeof window !== 'undefined' && localStorage) {
            localStorage.removeItem('jwt');
            localStorage.removeItem('role');
            localStorage.removeItem('email');
            localStorage.removeItem('name');
            localStorage.removeItem('isProfileCompleted');
            localStorage.removeItem('requirePasswordChange');
        }
        this.currentUser.set(null);
    }

    forgotPassword(email: string): Observable<any> {
        return this.http.post<any>(`${API_BASE}/Auth/forgot-password`, { email });
    }

    resetPassword(data: { email: string; token: string; newPassword: string }): Observable<any> {
        return this.http.post<any>(`${API_BASE}/Auth/reset-password`, data);
    }

    changePassword(newPassword: string): Observable<any> {
        return this.http.post<any>(`${API_BASE}/Auth/change-password`, { newPassword }).pipe(
            tap(() => {
                const user = this.currentUser();
                if (user) {
                    const updatedUser = { ...user, requirePasswordChange: false };
                    this.currentUser.set(updatedUser);
                    if (typeof window !== 'undefined' && localStorage) {
                        localStorage.setItem('requirePasswordChange', 'false');
                    }
                }
            })
        );
    }

    private navigateByRole(role: UserRole, isProfileCompleted: boolean = true) {
        switch (role?.toLowerCase()) {
            case 'admin':
                this.router.navigate(['/admin/dashboard']);
                break;
            case 'agent':
                this.router.navigate(['/agent/dashboard']);
                break;
            case 'customer':
                if (!isProfileCompleted) {
                    this.router.navigate(['/customer/profile']);
                } else {
                    this.router.navigate(['/customer/dashboard']);
                }
                break;
            case 'claimsofficer':
                this.router.navigate(['/claims-officer/dashboard']);
                break;
            default:
                this.router.navigate(['/auth/login']);
        }
    }

    private decodeToken(token: string): any {
        try {
            return JSON.parse(atob(token.split('.')[1]));
        } catch (e) {
            console.error("Failed to decode token", e);
            return {};
        }
    }
}

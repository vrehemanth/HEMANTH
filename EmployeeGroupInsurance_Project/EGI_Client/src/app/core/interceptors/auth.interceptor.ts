import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const router = inject(Router);
    const authService = inject(AuthService);
    const token = typeof window !== 'undefined' ? localStorage.getItem('jwt') : null;

    if (token) {
        req = req.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });
    }

    return next(req).pipe(
        catchError((error: HttpErrorResponse) => {
            if (error.status === 401 || error.status === 403) {
                authService.currentUser.set(null);
                if (typeof window !== 'undefined') {
                    localStorage.removeItem('jwt');
                    localStorage.removeItem('role');
                }
                router.navigate(['/auth/login']);
            }
            return throwError(() => error);
        })
    );
};

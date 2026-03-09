import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { ErrorStateService } from '../services/error-state.service';

export const httpErrorInterceptor: HttpInterceptorFn = (req, next) => {
    const errorState = inject(ErrorStateService);
    const router = inject(Router);

    return next(req).pipe(
        catchError((error: HttpErrorResponse) => {
            // Don't intercept 400, 401, 403, 404, or 422 here if the auth interceptor or components handle it inline
            // For this global error page, we strictly want to catch 500s or network failures (0)
            if (error.status === 0 || error.status >= 500) {
                let title = 'Network Error';
                let message = 'An unknown network error occurred.';
                let statusCode = error.status;

                if (error.error instanceof ErrorEvent) {
                    message = error.error.message;
                } else {
                    title = error.status === 0 ? 'Connection Refused' : `HTTP Error ${error.status}`;
                    message = error.message;
                    if (error.error && typeof error.error === 'object') {
                        message = error.error?.message || error.error?.title || error.error?.detail || message;
                    }
                }

                errorState.setError(title, message, error.url || '', statusCode);
                router.navigate(['/error']);
            }

            return throwError(() => error);
        })
    );
};

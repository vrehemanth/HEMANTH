import { ErrorHandler, Injectable, Injector, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { ErrorStateService } from '../services/error-state.service';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
    constructor(private injector: Injector, private zone: NgZone) { }

    handleError(error: any) {
        const errorState = this.injector.get(ErrorStateService);
        const router = this.injector.get(Router);

        let message = error.message ? error.message : error.toString();
        let stack = error.stack;

        // Skip if it's an HTTP error handled by the HttpInterceptor, UNLESS it's an unhandled rejected promise of an HttpError
        if (!(error instanceof HttpErrorResponse) && !error?.rejection?.status) {
            errorState.setError(
                'Application Error',
                message,
                stack
            );

            this.zone.run(() => {
                router.navigate(['/error']);
            });
        }

        console.error('Captured by GlobalErrorHandler:', error);
    }
}

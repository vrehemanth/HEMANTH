import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class ErrorStateService {
    private _errorTitle: string = 'Oops! Something went wrong.';
    private _errorMessage: string = 'An unexpected error has occurred.';
    private _errorStack: string | null = null;
    private _statusCode: number | null = null;

    setError(title: string, message: string, stack?: string, statusCode?: number) {
        this._errorTitle = title;
        this._errorMessage = message;
        this._errorStack = stack || null;
        this._statusCode = statusCode || null;
    }

    getError() {
        return {
            title: this._errorTitle,
            message: this._errorMessage,
            stack: this._errorStack,
            statusCode: this._statusCode
        };
    }

    clearError() {
        this._errorTitle = 'Oops! Something went wrong.';
        this._errorMessage = 'An unexpected error has occurred.';
        this._errorStack = null;
        this._statusCode = null;
    }
}

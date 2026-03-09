import { Component, OnInit } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { Router } from '@angular/router';
import { ErrorStateService } from '../../core/services/error-state.service';

@Component({
    selector: 'app-error',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './error.html',
    styles: []
})
export class ErrorComponent implements OnInit {
    errorData: any = {};

    constructor(
        private errorState: ErrorStateService,
        private router: Router,
        private location: Location
    ) { }

    ngOnInit() {
        this.errorData = this.errorState.getError();

        // If there is no error data (user directly navigated to /error), try to setup a default fallback
        if (!this.errorData.message || this.errorData.message === 'An unexpected error has occurred.') {
            this.errorData.title = 'System Alert';
            this.errorData.message = 'No recent errors were detected. If you arrived here by mistake, please return home.';
        }
    }

    goBack() {
        this.location.back();
    }

    goHome() {
        this.errorState.clearError();
        this.router.navigate(['/']);
    }
}

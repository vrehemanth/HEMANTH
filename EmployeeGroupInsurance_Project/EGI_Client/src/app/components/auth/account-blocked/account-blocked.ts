import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { AuthService } from '../../../core/services/auth.service';

@Component({
    selector: 'app-account-blocked',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './account-blocked.html',
    styleUrl: './account-blocked.css'
})
export class AccountBlockedComponent {
    private router = inject(Router);
    private authService = inject(AuthService);

    supportEmail = 'security@egi-insurance.com';
    supportContact = '+91 9876543210';

    logout() {
        this.authService.clearSession();
        this.router.navigate(['/auth/login']);
    }
}

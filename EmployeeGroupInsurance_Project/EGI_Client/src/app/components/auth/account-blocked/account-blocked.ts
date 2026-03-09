import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
    selector: 'app-account-blocked',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './account-blocked.html',
    styleUrl: './account-blocked.css'
})
export class AccountBlockedComponent {
    private router = inject(Router);

    supportEmail = 'security@egi-insurance.com';
    supportContact = '+1 (800) EGI-PROT';

    logout() {
        localStorage.clear();
        sessionStorage.clear();
        this.router.navigate(['/auth/login']);
    }
}

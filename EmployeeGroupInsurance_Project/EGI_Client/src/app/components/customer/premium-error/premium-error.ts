import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-premium-error',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './premium-error.html'
})
export class PremiumErrorComponent {
    private router = inject(Router);

    goToBilling() {
        this.router.navigate(['/customer/invoices']);
    }
}

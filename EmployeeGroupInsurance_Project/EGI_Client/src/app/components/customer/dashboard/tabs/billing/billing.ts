import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-billing-tab',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './billing.html'
})
export class BillingTabComponent {
  @Input() p!: CustomerDashboardComponent;
}

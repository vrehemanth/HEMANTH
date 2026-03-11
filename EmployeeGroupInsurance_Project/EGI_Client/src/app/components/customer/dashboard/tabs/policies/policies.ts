import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { CustomerDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-policies-tab',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './policies.html'
})
export class PoliciesTabComponent {
  @Input() p!: CustomerDashboardComponent;
}

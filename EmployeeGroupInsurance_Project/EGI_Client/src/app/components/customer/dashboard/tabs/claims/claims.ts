import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { CustomerDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-claims-tab',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './claims.html'
})
export class ClaimsTabComponent {
  @Input() p!: CustomerDashboardComponent;
}

import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-policies-tab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './policies.html'
})
export class PoliciesTabComponent {
  @Input() p!: AdminDashboardComponent;
}

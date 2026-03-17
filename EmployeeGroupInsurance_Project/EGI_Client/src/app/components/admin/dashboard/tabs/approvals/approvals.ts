import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-approvals-tab',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './approvals.html'
})
export class ApprovalsTabComponent {
  @Input() p!: AdminDashboardComponent;
}

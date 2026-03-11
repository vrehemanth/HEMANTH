import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgentDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-policies-tab',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './policies.html'
})
export class PoliciesTabComponent {
  @Input() p!: AgentDashboardComponent;
}

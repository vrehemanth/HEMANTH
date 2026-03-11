import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgentDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-commissions-tab',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './commissions.html'
})
export class CommissionsTabComponent {
  @Input() p!: AgentDashboardComponent;
}

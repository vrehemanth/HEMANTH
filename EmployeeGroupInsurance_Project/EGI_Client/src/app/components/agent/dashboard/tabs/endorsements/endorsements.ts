import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgentDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-endorsements-tab',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './endorsements.html'
})
export class EndorsementsTabComponent {
  @Input() p!: AgentDashboardComponent;
}

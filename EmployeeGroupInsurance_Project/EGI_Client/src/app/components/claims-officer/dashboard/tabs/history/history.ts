import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClaimsOfficerDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-history-tab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './history.html'
})
export class HistoryTabComponent {
  @Input() p!: ClaimsOfficerDashboardComponent;
}

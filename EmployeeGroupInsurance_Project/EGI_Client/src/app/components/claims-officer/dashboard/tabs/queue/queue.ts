import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClaimsOfficerDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-queue-tab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './queue.html'
})
export class QueueTabComponent {
  @Input() p!: ClaimsOfficerDashboardComponent;
}

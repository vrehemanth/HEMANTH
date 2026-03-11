import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-logs-tab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './logs.html'
})
export class LogsTabComponent {
  @Input() p!: AdminDashboardComponent;
}

import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-staff-tab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './staff.html'
})
export class StaffTabComponent {
  @Input() p!: AdminDashboardComponent;
}

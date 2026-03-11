import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-plans-tab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './plans.html'
})
export class PlansTabComponent {
  @Input() p!: AdminDashboardComponent;
}

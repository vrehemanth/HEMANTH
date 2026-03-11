import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-claims-tab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './claims.html'
})
export class ClaimsTabComponent {
  @Input() p!: AdminDashboardComponent;
}

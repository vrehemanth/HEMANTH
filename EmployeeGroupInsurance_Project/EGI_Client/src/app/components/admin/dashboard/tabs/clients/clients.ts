import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-clients-tab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './clients.html'
})
export class ClientsTabComponent {
  @Input() p!: AdminDashboardComponent;
}

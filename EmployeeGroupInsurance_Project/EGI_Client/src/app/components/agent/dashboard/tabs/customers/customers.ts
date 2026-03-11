import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AgentDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-customers-tab',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './customers.html'
})
export class CustomersTabComponent {
  @Input() p!: AgentDashboardComponent;
}

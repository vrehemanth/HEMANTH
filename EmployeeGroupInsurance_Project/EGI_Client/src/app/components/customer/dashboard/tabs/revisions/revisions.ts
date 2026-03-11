import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { CustomerDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-revisions-tab',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './revisions.html'
})
export class RevisionsTabComponent {
  @Input() p!: CustomerDashboardComponent;
}

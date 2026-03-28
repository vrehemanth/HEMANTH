import { Component, Input, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClaimsOfficerDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-health-checkup-tab',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './health-checkups.html'
})
export class HealthCheckupTabComponent {
  @Input() p!: ClaimsOfficerDashboardComponent;

  activeCheckups = computed(() => {
    return this.p.pendingHealthCheckups().filter(c => 
        c.isHealthCheckupClaimPending || this.isEditable(c)
    );
  });

  archivedCheckups = computed(() => {
    return this.p.pendingHealthCheckups().filter(c => 
        !c.isHealthCheckupClaimPending && !this.isEditable(c)
    );
  });

  isEditable(client: any): boolean {
    if (client.isHealthCheckupClaimPending) return true;
    if (!client.healthCheckupVerifiedAt) return false;
    const verifiedAt = new Date(client.healthCheckupVerifiedAt);
    const diffDays = (new Date().getTime() - verifiedAt.getTime()) / (1000 * 3600 * 24);
    return diffDays <= 7;
  }
}

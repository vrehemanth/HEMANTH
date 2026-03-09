import { Component, inject, signal, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, NavigationEnd } from '@angular/router';
import { ClaimsOfficerService } from '../../../data-access/api.services';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { ToastService } from '../../../core/services/toast.service';

@Component({
  selector: 'app-claims-officer-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html'
})
export class ClaimsOfficerDashboardComponent implements OnInit, OnDestroy {
  officerService = inject(ClaimsOfficerService);
  router = inject(Router);
  toastService = inject(ToastService);
  private routerSub?: Subscription;

  activeTab = signal<'overview' | 'queue' | 'history'>('overview');
  pageTitle = signal('Claims Adjudication Dashboard');
  isLoading = signal(false);

  summary = signal<any>(null);
  pendingClaims = signal<any[]>([]);
  claimHistory = signal<any[]>([]);

  selectedClaim = signal<any>(null);
  coverageSummary = signal<any>(null);
  memberHistory = signal<any[]>([]);

  private extractArray(payload: any): any[] {
    if (!payload) return [];
    if (Array.isArray(payload)) return payload;
    if (payload.data) {
      if (Array.isArray(payload.data)) return payload.data;
      if (payload.data.$values && Array.isArray(payload.data.$values)) return payload.data.$values;
    }
    if (payload.$values && Array.isArray(payload.$values)) return payload.$values;
    return [];
  }

  ngOnInit() {
    this.updateTab(this.router.url);
    this.routerSub = this.router.events.pipe(
      filter(e => e instanceof NavigationEnd)
    ).subscribe((e: any) => this.updateTab(e.urlAfterRedirects));
  }

  refreshAll() {
    this.officerService.getSummary().subscribe((res: any) => this.summary.set(res?.data || res));
    this.loadPendingClaims();
    this.loadHistory();
  }

  ngOnDestroy() {
    this.routerSub?.unsubscribe();
  }

  updateTab(url: string) {
    let tab: 'overview' | 'queue' | 'history' = 'overview';
    this.pageTitle.set('Claims Adjudication Dashboard');

    if (url.includes('/queue')) {
      tab = 'queue';
      this.pageTitle.set('Actionable Queue');
    } else if (url.includes('/history')) {
      tab = 'history';
      this.pageTitle.set('Review History');
    }

    this.activeTab.set(tab);
    this.lazyLoadTab(tab);
  }

  private pendingReqs = new Set<string>();

  lazyLoadTab(tab: string) {
    if (tab === 'overview' && !this.summary() && !this.pendingReqs.has('overview')) {
      this.pendingReqs.add('overview');
      this.officerService.getSummary().subscribe({
        next: res => { this.summary.set(res?.data || res); this.pendingReqs.delete('overview'); },
        error: () => this.pendingReqs.delete('overview')
      });
    } else if (tab === 'queue' && this.pendingClaims().length === 0) {
      this.loadPendingClaims();
    } else if (tab === 'history' && this.claimHistory().length === 0) {
      this.loadHistory();
    }
  }

  loadPendingClaims() {
    if (this.pendingReqs.has('queue')) return;
    this.pendingReqs.add('queue');
    this.officerService.getPendingClaims().subscribe({
      next: (res: any) => {
        this.pendingClaims.set(this.extractArray(res));
        this.pendingReqs.delete('queue');
      },
      error: () => this.pendingReqs.delete('queue')
    });
  }

  loadHistory() {
    if (this.pendingReqs.has('history')) return;
    this.pendingReqs.add('history');
    this.officerService.getHistory().subscribe({
      next: (res: any) => {
        this.claimHistory.set(this.extractArray(res));
        this.pendingReqs.delete('history');
      },
      error: () => this.pendingReqs.delete('history')
    });
  }

  viewClaimDetails(id: string) {
    this.officerService.takeClaim(id).subscribe({
      next: () => {
        this.officerService.getClaimDetail(id).subscribe((c: any) => {
          const claim = c?.data || c;
          this.selectedClaim.set(claim);
          this.memberHistory.set([]);

          // Load Additional Insights
          if (claim?.memberId) {
            this.officerService.getClaimsByMember(claim.memberId).subscribe(res => {
              this.memberHistory.set(this.extractArray(res));
            });

            if (claim?.dependentId) {
              this.officerService.getDependentCoverageSummary(claim.memberId, claim.dependentId).subscribe((cov: any) => {
                this.coverageSummary.set(cov?.data || cov);
              });
            } else {
              this.officerService.getMemberCoverageSummary(claim.memberId).subscribe((cov: any) => {
                this.coverageSummary.set(cov?.data || cov);
              });
            }
          }
        });
      },
      error: (err) => {
        this.toastService.error(err.error?.message || "This claim is already being reviewed by another officer.");
        this.loadPendingClaims();
      }
    });
  }

  closeModal() {
    if (this.selectedClaim()) {
      this.officerService.releaseClaim(this.selectedClaim().id).subscribe();
    }
    this.selectedClaim.set(null);
    this.coverageSummary.set(null);
    this.memberHistory.set([]);
  }

  adjudicate(claimId: string, isApproved: boolean) {
    let reason = '';
    if (!isApproved) {
      reason = prompt("Please provide a reason for rejection:") || '';
      if (!reason.trim()) {
        this.toastService.warning("A rejection reason is mandatory.");
        return;
      }
    }

    const payload = {
      isApproved,
      rejectionReason: isApproved ? null : reason
    };

    this.officerService.reviewClaim(claimId, payload).subscribe(() => {
      this.selectedClaim.set(null);
      this.coverageSummary.set(null);
      this.memberHistory.set([]);
      this.toastService.success(`Claim marked as ${isApproved ? 'Approved' : 'Rejected'}.`);
      this.refreshAll();
    });
  }

  exportData() {
    let data: any[] = [];
    let filename = 'EGI_Report_';

    switch (this.activeTab()) {
      case 'queue':
        data = this.pendingClaims();
        filename += 'ClaimsQueue.csv';
        break;
      case 'history':
        data = this.claimHistory();
        filename += 'ReviewHistory.csv';
        break;
      default:
        this.toastService.warning("No exportable data available for this view.");
        return;
    }

    if (!data || data.length === 0) {
      this.toastService.warning("No data found to export.");
      return;
    }

    const headers = Object.keys(data[0]).filter(k => typeof data[0][k] !== 'object');
    let csv = headers.join(',') + '\n';

    csv += data.map(row =>
      headers.map(h => `"${(row[h] !== null && row[h] !== undefined ? row[h].toString().replace(/"/g, '""') : '')}"`).join(',')
    ).join('\n');

    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });
    const link = document.createElement('a');
    if (link.download !== undefined) {
      const url = URL.createObjectURL(blob);
      link.setAttribute('href', url);
      link.setAttribute('download', filename);
      link.style.visibility = 'hidden';
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    }
  }
}

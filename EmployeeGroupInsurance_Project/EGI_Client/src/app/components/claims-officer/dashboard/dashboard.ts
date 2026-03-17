import { Component, inject, signal, OnInit, OnDestroy, computed, viewChild, ElementRef, effect } from '@angular/core';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';
import { Router, NavigationEnd } from '@angular/router';
import { ClaimsOfficerService } from '../../../data-access/claims-officer.service';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { ToastService } from '../../../core/services/toast.service';
import { AuthService } from '../../../core/services/auth.service';
import { Chart, registerables } from 'chart.js';
import { OverviewTabComponent } from './tabs/overview/overview';
import { QueueTabComponent } from './tabs/queue/queue';
import { HistoryTabComponent } from './tabs/history/history';
Chart.register(...registerables);

@Component({
  selector: 'app-claims-officer-dashboard',
  standalone: true,
  imports: [CommonModule, OverviewTabComponent, QueueTabComponent, HistoryTabComponent],
  templateUrl: './dashboard.html'
})
export class ClaimsOfficerDashboardComponent implements OnInit, OnDestroy {
  officerService = inject(ClaimsOfficerService);
  router = inject(Router);
  toastService = inject(ToastService);
  authService = inject(AuthService);
  private routerSub?: Subscription;

  private decisionChart?: Chart;
  private adjudicationChart?: Chart;
  private typeChart?: Chart;

  activeTab = signal<'overview' | 'queue' | 'history'>('overview');
  pageTitle = signal('Claims Adjudication Dashboard');
  isLoading = signal(false);

  summary = signal<any>(null);
  pendingClaims = signal<any[]>([]);
  claimHistory = signal<any[]>([]);

  // Filtering signals
  pendingSearchTerm = signal('');
  pendingSortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'date', direction: 'desc' });

  historySearchTerm = signal('');
  historyStatusFilter = signal('All');
  historySortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'date', direction: 'desc' });

  // Computed filtered lists
  filteredPendingClaims = computed(() => {
    let result = this.pendingClaims();
    const search = this.pendingSearchTerm().toLowerCase().trim();
    if (search) {
      result = result.filter(c =>
        (c.id && c.id.toLowerCase().includes(search)) ||
        (c.memberName && c.memberName.toLowerCase().includes(search)) ||
        (c.claimType && c.claimType.toLowerCase().includes(search))
      );
    }
    const sort = this.pendingSortConfig();
    return [...result].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'id': comparison = (a.id || '').localeCompare(b.id || ''); break;
        case 'member': comparison = (a.memberName || '').localeCompare(b.memberName || ''); break;
        case 'type': comparison = (a.claimType || '').localeCompare(b.claimType || ''); break;
        case 'amount': comparison = (a.claimAmount || 0) - (b.claimAmount || 0); break;
        case 'date': comparison = new Date(a.date || 0).getTime() - new Date(b.date || 0).getTime(); break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  filteredClaimHistory = computed(() => {
    let result = this.claimHistory();
    const statusIdx = this.historyStatusFilter();
    if (statusIdx !== 'All') {
      result = result.filter(c => c.status === statusIdx);
    }
    const search = this.historySearchTerm().toLowerCase().trim();
    if (search) {
      result = result.filter(c =>
        (c.id && c.id.toLowerCase().includes(search)) ||
        (c.memberName && c.memberName.toLowerCase().includes(search)) ||
        (c.claimType && c.claimType.toLowerCase().includes(search)) ||
        (c.rejectionReason && c.rejectionReason.toLowerCase().includes(search))
      );
    }
    const sort = this.historySortConfig();
    return [...result].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'date': comparison = new Date(a.reviewedAt || 0).getTime() - new Date(b.reviewedAt || 0).getTime(); break;
        case 'member': comparison = (a.memberName || '').localeCompare(b.memberName || ''); break;
        case 'type': comparison = (a.claimType || '').localeCompare(b.claimType || ''); break;
        case 'amount': comparison = (a.claimAmount || 0) - (b.claimAmount || 0); break;
        case 'status': comparison = (a.status || '').localeCompare(b.status || ''); break;
        case 'reason': comparison = (a.rejectionReason || '').localeCompare(b.rejectionReason || ''); break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  updateSort(configSignal: any, column: string) {
    const current = configSignal();
    if (current.column === column) {
      configSignal.set({ column, direction: current.direction === 'asc' ? 'desc' : 'asc' });
    } else {
      configSignal.set({ column, direction: 'asc' });
    }
  }

  sortPending(column: string) { this.updateSort(this.pendingSortConfig, column); }
  sortHistory(column: string) { this.updateSort(this.historySortConfig, column); }

  selectedClaim = signal<any>(null);
  coverageSummary = signal<any>(null);
  memberHistory = signal<any[]>([]);

  getDocumentUrl(docId: string): string {
    const token = this.authService.currentUser()?.token;
    if (!token) return '';
    return `https://localhost:7146/api/Public/documents/${docId}?access_token=${token}`;
  }

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
        next: res => {
          this.summary.set(res?.data || res);
          this.pendingReqs.delete('overview');
        },
        error: () => this.pendingReqs.delete('overview')
      });
      // Load history in parallel for analytics/charts
      this.lazyLoadTab('history');
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

  async adjudicate(claimId: string, isApproved: boolean) {
    const claim = this.selectedClaim();
    let reason = '';
    let overrideReason = '';
    let overrideFraud = false;

    // 1. Mandatory Rejection Reason
    if (!isApproved) {
      const { value: text } = await Swal.fire({
        title: 'Rejection Reason',
        input: 'textarea',
        inputLabel: 'Please provide a detailed justification for rejection:',
        inputPlaceholder: 'Enter reason here...',
        inputAttributes: {
          'aria-label': 'Enter reason here'
        },
        showCancelButton: true,
        confirmButtonColor: '#ef4444',
        cancelButtonColor: '#64748b',
        confirmButtonText: 'Confirm Rejection'
      });

      if (text === undefined) return; // Cancelled
      reason = text || '';

      if (!reason.trim()) {
        this.toastService.warning("A rejection reason is mandatory.");
        return;
      }
    }

    // 2. Fraud Logic: If approved but risky, ask for override
    if (isApproved && claim?.isSuspectedFraud && !claim?.isFraudOverridden) {
      const riskConfirm = await Swal.fire({
        title: 'High AI Risk Alert',
        html: `
          <div class="text-left">
            <p class="text-red-600 font-bold mb-2">Risk Score: ${claim.fraudScore}%</p>
            <p class="text-gray-600 text-sm mb-4">Analysis: ${claim.fraudAnalysis}</p>
            <p class="font-semibold text-gray-800">Do you wish to override this risk and proceed with approval?</p>
          </div>
        `,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#10b981',
        cancelButtonColor: '#64748b',
        confirmButtonText: 'Yes, I Justify Override',
        cancelButtonText: 'Cancel'
      });

      if (!riskConfirm.isConfirmed) return;

      const { value: text } = await Swal.fire({
        title: 'Override Justification',
        input: 'textarea',
        inputLabel: 'CRITICAL: Provide professional justification for overriding AI Risk:',
        inputPlaceholder: 'Evidence for override...',
        showCancelButton: true,
        confirmButtonColor: '#10b981',
        cancelButtonColor: '#64748b',
        confirmButtonText: 'Finalize Approval'
      });

      if (text === undefined) return; // Cancelled
      overrideReason = text || '';

      if (!overrideReason.trim()) {
        this.toastService.warning("An override justification is mandatory for high-risk claims.");
        return;
      }
      overrideFraud = true;
    }

    const payload = {
      isApproved,
      rejectionReason: isApproved ? null : reason,
      overrideFraud: overrideFraud,
      fraudOverrideReason: overrideReason || null
    };

    this.officerService.reviewClaim(claimId, payload).subscribe({
      next: () => {
        this.selectedClaim.set(null);
        this.coverageSummary.set(null);
        this.memberHistory.set([]);
        this.toastService.success(`Claim marked as ${isApproved ? 'Approved' : 'Rejected'}.`);
        this.refreshAll();
      },
      error: (err) => {
        this.toastService.error(err.error?.message || "Failed to finalize claim.");
      }
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

  public destroyCharts() {
    this.decisionChart?.destroy();
    this.adjudicationChart?.destroy();
    this.typeChart?.destroy();
    
    this.decisionChart = undefined;
    this.adjudicationChart = undefined;
    this.typeChart = undefined;
  }

  public initCharts(dCtx: HTMLCanvasElement, aCtx: HTMLCanvasElement, tCtx: HTMLCanvasElement) {
    if (!dCtx || !aCtx || !tCtx) return;

    this.initDecisionChart(dCtx);
    this.initAdjudicationChart(aCtx);
    this.initTypeChart(tCtx);
  }

  private initDecisionChart(ctx: HTMLCanvasElement) {
    const s = this.summary();
    const approved = s?.approvedClaimsCount || 0;
    const rejected = s?.rejectedClaimsCount || 0;
    const pending = s?.pendingClaimsCount || 0;

    this.decisionChart = new Chart(ctx, {
      type: 'doughnut',
      data: {
        labels: ['Approved', 'Rejected', 'Pending'],
        datasets: [{
          data: [approved, rejected, pending],
          backgroundColor: ['#10b981', '#ef4444', '#f59e0b'],
          borderWidth: 0,
          borderRadius: 10,
          hoverOffset: 12
        }]
      },
      options: {
        cutout: '75%',
        responsive: true,
        plugins: {
          legend: { display: false },
          tooltip: {
            backgroundColor: 'rgba(15, 23, 42, 0.95)',
            padding: 14,
            usePointStyle: true,
            callbacks: { label: (i: any) => ` ${i.label}: ${i.raw} Claims` }
          }
        }
      }
    });
  }

  private initAdjudicationChart(ctx: HTMLCanvasElement) {
    const history = this.claimHistory();
    const last7Days = [];
    const counts = new Array(7).fill(0);

    for (let i = 6; i >= 0; i--) {
      const d = new Date();
      d.setDate(d.getDate() - i);
      last7Days.push(d.toLocaleDateString('en-US', { weekday: 'short' }));
    }

    history.forEach(c => {
      const d = new Date(c.reviewedAt);
      const diff = Math.floor((new Date().getTime() - d.getTime()) / (1000 * 3600 * 24));
      if (diff >= 0 && diff < 7) {
        counts[6 - diff]++;
      }
    });

    const grad = ctx.getContext('2d')?.createLinearGradient(0, 0, 0, 400);
    grad?.addColorStop(0, 'rgba(99, 102, 241, 0.4)');
    grad?.addColorStop(1, 'rgba(99, 102, 241, 0)');

    this.adjudicationChart = new Chart(ctx, {
      type: 'line',
      data: {
        labels: last7Days,
        datasets: [{
          label: 'Reviewed Claims',
          data: counts,
          borderColor: '#6366f1',
          backgroundColor: grad,
          fill: true,
          tension: 0.4,
          pointRadius: 4,
          borderWidth: 3
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: { legend: { display: false } },
        scales: {
          x: { grid: { display: false }, ticks: { font: { size: 10, weight: 600 } } },
          y: { beginAtZero: true, grid: { color: 'rgba(0,0,0,0.05)' }, ticks: { stepSize: 1, font: { size: 10 } } }
        }
      }
    });
  }

  private initTypeChart(ctx: HTMLCanvasElement) {
    const history = this.claimHistory();
    const types: { [key: string]: number } = {};
    history.slice(0, 50).forEach(c => { // Sample last 50
      types[c.claimType] = (types[c.claimType] || 0) + 1;
    });

    this.typeChart = new Chart(ctx, {
      type: 'polarArea',
      data: {
        labels: Object.keys(types),
        datasets: [{
          data: Object.values(types),
          backgroundColor: [
            'rgba(59, 130, 246, 0.6)',
            'rgba(16, 185, 129, 0.6)',
            'rgba(245, 158, 11, 0.6)',
            'rgba(139, 92, 246, 0.6)',
            'rgba(236, 72, 153, 0.6)'
          ]
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          legend: {
            position: 'bottom',
            labels: { boxWidth: 8, font: { size: 9 }, usePointStyle: true }
          }
        },
        scales: {
          r: { ticks: { display: false }, grid: { color: 'rgba(0,0,0,0.05)' } }
        }
      }
    });
  }
}

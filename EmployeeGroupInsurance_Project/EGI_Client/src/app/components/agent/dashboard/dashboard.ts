import { Component, inject, signal, OnInit, OnDestroy, computed, viewChild, ElementRef, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, NavigationEnd } from '@angular/router';
import { AgentService } from '../../../data-access/agent.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { filter, tap } from 'rxjs/operators';
import { ToastService } from '../../../core/services/toast.service';
import { AuthService } from '../../../core/services/auth.service';
import { Chart, registerables } from 'chart.js';
import { OverviewTabComponent } from './tabs/overview/overview';
import { CustomersTabComponent } from './tabs/customers/customers';
import { PoliciesTabComponent } from './tabs/policies/policies';
import { EndorsementsTabComponent } from './tabs/endorsements/endorsements';
import { CommissionsTabComponent } from './tabs/commissions/commissions';
Chart.register(...registerables);

@Component({
  selector: 'app-agent-dashboard',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, OverviewTabComponent, CustomersTabComponent, PoliciesTabComponent, EndorsementsTabComponent, CommissionsTabComponent],
  templateUrl: './dashboard.html'
})
export class AgentDashboardComponent implements OnInit, OnDestroy {
  agentService = inject(AgentService);
  router = inject(Router);
  fb = inject(FormBuilder);
  toastService = inject(ToastService);
  authService = inject(AuthService);
  private routerSub?: Subscription;

  private commissionChart?: Chart;
  private outstandingChart?: Chart;
  private salesMixChart?: Chart;

  activeTab = signal<'overview' | 'customers' | 'policies' | 'endorsements' | 'commissions'>('overview');
  pageTitle = signal('Agent Dashboard');
  showCustomerForm = signal(false);
  isLoading = signal(false);

  summary = signal<any>(null);
  customers = signal<any[]>([]);
  policies = signal<any[]>([]);
  pendingEndorsements = signal<any[]>([]);
  commissionLogs = signal<any[]>([]);

  // Filtering signals
  customerSearchTerm = signal('');
  customerStatusFilter = signal('All');
  customerSortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'company', direction: 'asc' });

  policySearchTerm = signal('');
  policySortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'policyNo', direction: 'asc' });

  endorsementSortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'date', direction: 'desc' });
  commissionSortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'timestamp', direction: 'desc' });

  // Computed filtered lists
  filteredCustomers = computed(() => {
    let result = this.customers();

    const statusIdx = this.customerStatusFilter();
    if (statusIdx !== 'All') {
      result = result.filter(c => c.status === statusIdx);
    }

    const search = this.customerSearchTerm().toLowerCase().trim();
    if (search) {
      result = result.filter(c =>
        (c.companyName && c.companyName.toLowerCase().includes(search)) ||
        (c.address && c.address.toLowerCase().includes(search)) ||
        (c.name && c.name.toLowerCase().includes(search))
      );
    }

    const sort = this.customerSortConfig();
    return [...result].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'company': comparison = (a.companyName || '').localeCompare(b.companyName || ''); break;
        case 'category': comparison = (a.businessCategory || '').localeCompare(b.businessCategory || ''); break;
        case 'status': comparison = (a.status || '').localeCompare(b.status || ''); break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  filteredPolicies = computed(() => {
    let result = this.policies();

    const search = this.policySearchTerm().toLowerCase().trim();
    if (search) {
      result = result.filter(p =>
        (p.policyNo && p.policyNo.toLowerCase().includes(search)) ||
        (p.companyName && p.companyName.toLowerCase().includes(search)) ||
        (p.planName && p.planName.toLowerCase().includes(search))
      );
    }

    const sort = this.policySortConfig();
    return [...result].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'policyNo': comparison = (a.policyNo || '').localeCompare(b.policyNo || ''); break;
        case 'company': comparison = (a.companyName || '').localeCompare(b.companyName || ''); break;
        case 'plan': comparison = (a.planName || '').localeCompare(b.planName || ''); break;
        case 'period': comparison = new Date(a.startDate || 0).getTime() - new Date(b.startDate || 0).getTime(); break;
        case 'premium': comparison = (a.annualPremium || 0) - (b.annualPremium || 0); break;
        case 'commission': comparison = (a.commissionAmount || 0) - (b.commissionAmount || 0); break;
        case 'status': comparison = (a.status || '').localeCompare(b.status || ''); break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  filteredEndorsements = computed(() => {
    const sort = this.endorsementSortConfig();
    return [...this.pendingEndorsements()].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'type': comparison = (a.type || '').localeCompare(b.type || ''); break;
        case 'policy': comparison = (a.policyAssignmentId || '').localeCompare(b.policyAssignmentId || ''); break;
        case 'description': comparison = (a.description || '').localeCompare(b.description || ''); break;
        case 'adjustment': comparison = (a.premiumAdjustment || 0) - (b.premiumAdjustment || 0); break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  filteredCommissions = computed(() => {
    const sort = this.commissionSortConfig();
    return [...this.commissionLogs()].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'timestamp': comparison = new Date(a.timestamp || 0).getTime() - new Date(b.timestamp || 0).getTime(); break;
        case 'event': comparison = (a.id || '').localeCompare(b.id || ''); break;
        case 'detail': comparison = (a.newValues || '').localeCompare(b.newValues || ''); break;
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

  sortCustomers(column: string) { this.updateSort(this.customerSortConfig, column); }
  sortPolicies(column: string) { this.updateSort(this.policySortConfig, column); }
  sortEndorsements(column: string) { this.updateSort(this.endorsementSortConfig, column); }
  sortCommissions(column: string) { this.updateSort(this.commissionSortConfig, column); }

  getDocumentUrl(docId: string): string {
    const token = this.authService.currentUser()?.token;
    if (!token) return '';
    return `https://localhost:7146/api/Public/documents/${docId}?access_token=${token}`;
  }

  selectedFile: File | null = null;

  customerForm = this.fb.group({
    companyName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    name: ['', Validators.required],
    address: ['', Validators.required]
  });

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
    this.routerSub = this.router.events.pipe(filter(e => e instanceof NavigationEnd)).subscribe((e: any) => this.updateTab(e.urlAfterRedirects));
  }

  ngOnDestroy() {
    this.routerSub?.unsubscribe();
    this.destroyCharts();
  }

  updateTab(url: string) {
    let tab: 'overview' | 'customers' | 'policies' | 'endorsements' | 'commissions' = 'overview';
    if (url.includes('/customers')) { tab = 'customers'; this.pageTitle.set('My Customers'); }
    else if (url.includes('/policies')) { tab = 'policies'; this.pageTitle.set('My Policies'); }
    else if (url.includes('/endorsements')) { tab = 'endorsements'; this.pageTitle.set('Endorsements'); }
    else if (url.includes('/commissions')) { tab = 'commissions'; this.pageTitle.set('Commission Ledger'); }
    else { tab = 'overview'; this.pageTitle.set('Agent Dashboard'); }

    this.activeTab.set(tab);
    this.lazyLoadTab(tab);
  }

  private pendingReqs = new Set<string>();

  lazyLoadTab(tab: string) {
    if (tab === 'overview' && !this.summary() && !this.pendingReqs.has('overview')) {
      this.pendingReqs.add('overview');
      this.agentService.getSummary().subscribe({
        next: res => {
          this.summary.set(res?.data || res);
          this.pendingReqs.delete('overview');
          // Load policies for charts
          this.lazyLoadTab('policies');
        },
        error: () => this.pendingReqs.delete('overview')
      });
    } else if (tab === 'customers') {
      if (this.customers().length === 0 && !this.pendingReqs.has('customers')) {
        this.pendingReqs.add('customers');
        this.agentService.getMyCustomers().subscribe({
          next: res => { this.customers.set(this.extractArray(res)); this.pendingReqs.delete('customers'); },
          error: () => this.pendingReqs.delete('customers')
        });
      }
    } else if (tab === 'policies') {
      if (this.policies().length === 0 && !this.pendingReqs.has('policies')) {
        this.pendingReqs.add('policies');
        this.agentService.getMyPolicies().subscribe({
          next: res => { this.policies.set(this.extractArray(res)); this.pendingReqs.delete('policies'); },
          error: () => this.pendingReqs.delete('policies')
        });
      }
    } else if (tab === 'endorsements') {
      if (this.pendingEndorsements().length === 0 && !this.pendingReqs.has('endorsements')) {
        this.pendingReqs.add('endorsements');
        this.agentService.getPendingEndorsements().subscribe({
          next: res => { this.pendingEndorsements.set(this.extractArray(res)); this.pendingReqs.delete('endorsements'); },
          error: () => this.pendingReqs.delete('endorsements')
        });
      }
    } else if (tab === 'commissions') {
      if (this.commissionLogs().length === 0 && !this.pendingReqs.has('commissions')) {
        this.pendingReqs.add('commissions');
        this.agentService.getCommissions().subscribe({
          next: res => { this.commissionLogs.set(this.extractArray(res)); this.pendingReqs.delete('commissions'); },
          error: () => this.pendingReqs.delete('commissions')
        });
      }
    }
  }

  onFileSelect(event: any) {
    if (event.target.files.length > 0) this.selectedFile = event.target.files[0];
  }

  createCustomer() {
    if (this.customerForm.invalid || !this.selectedFile) return;

    const formData = new FormData();
    const val = this.customerForm.value as any;

    formData.append('Email', val.email);
    formData.append('Name', val.name);
    formData.append('CompanyName', val.companyName);
    formData.append('Address', val.address);

    if (this.selectedFile) {
      formData.append('Documents', this.selectedFile);
    }

    this.agentService.createCustomer(formData).subscribe({
      next: () => {
        this.toastService.success('Customer onboarding request submitted successfully. The customer will receive login credentials after administrator approval.', 6000);
        this.showCustomerForm.set(false);
        this.customerForm.reset();
        this.selectedFile = null;
        this.agentService.getMyCustomers().subscribe((res: any) => this.customers.set(this.extractArray(res)));
      },
      error: (err) => {
        this.toastService.error('Error initiating onboarding: ' + (err?.error?.message || err?.message || 'Unknown error'));
      }
    });
  }

  reviewEndorsement(id: string, isApproved: boolean) {
    // Backend ReviewEndorsementDto expects: { status: EndorsementStatus } - enum value "Approved" or "Rejected"
    const dto = { status: isApproved ? 'Approved' : 'Rejected' };
    this.agentService.reviewEndorsement(id, dto).subscribe({
      next: () => {
        this.toastService.success(`Endorsement ${isApproved ? 'approved' : 'rejected'} successfully.`);
        this.agentService.getPendingEndorsements().subscribe((res: any) => this.pendingEndorsements.set(this.extractArray(res)));
      },
      error: (err) => {
        this.toastService.error('Error processing endorsement: ' + (err?.error?.message || err?.message || 'Unknown error'));
      }
    });
  }

  exportReport() {
    const pendingPremiums: any[] = this.summary()?.customerPendingPremiums;
    if (!pendingPremiums || pendingPremiums.length === 0) {
      this.toastService.warning('No pending premium data available to export.');
      return;
    }

    const headers = ['Company Name', 'Outstanding Amount (₹)'];
    let csv = headers.join(',') + '\n';
    csv += pendingPremiums.map((item: any) =>
      `"${item.companyName || ''}","${item.amount || 0}"`
    ).join('\n');

    // Add summary row
    const total = this.summary()?.pendingPremium || 0;
    csv += `\n"TOTAL OUTSTANDING","${total}"`;

    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });
    const url = URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.setAttribute('href', url);
    link.setAttribute('download', `Agent_PendingPremiums_${new Date().toISOString().split('T')[0]}.csv`);
    link.style.visibility = 'hidden';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    URL.revokeObjectURL(url);

    this.toastService.success('Pending Premium Report exported successfully.');
  }

  public destroyCharts() {
    this.commissionChart?.destroy();
    this.outstandingChart?.destroy();
    this.salesMixChart?.destroy();

    this.commissionChart = undefined;
    this.outstandingChart = undefined;
    this.salesMixChart = undefined;
  }

  public initCharts(cCtx: HTMLCanvasElement, oCtx: HTMLCanvasElement, sCtx: HTMLCanvasElement) {
    if (!cCtx || !oCtx || !sCtx) return;

    this.initCommissionChart(cCtx);
    this.initOutstandingChart(oCtx);
    this.initSalesMixChart(sCtx);
  }

  private initCommissionChart(ctx: HTMLCanvasElement) {
    const s = this.summary();
    const earned = s?.totalCommissionEarned || 0;
    const projected = s?.projectedCommission || 0;

    this.commissionChart = new Chart(ctx, {
      type: 'doughnut',
      data: {
        labels: ['Earned', 'Projected'],
        datasets: [{
          data: [earned, projected],
          backgroundColor: ['#10b981', '#6366f1'],
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
            titleFont: { size: 13, weight: 'bold' },
            bodyFont: { size: 12 },
            padding: 14,
            displayColors: true,
            boxWidth: 10,
            boxHeight: 10,
            boxPadding: 6,
            usePointStyle: true,
            borderColor: 'rgba(255,255,255,0.1)',
            borderWidth: 1,
            callbacks: {
              label: (i: any) => ` ${i.label}: ₹${i.raw.toLocaleString()}`
            }
          }
        }
      }
    });
  }

  private initOutstandingChart(ctx: HTMLCanvasElement) {
    const pending = this.summary()?.customerPendingPremiums || [];
    const labels = pending.map((p: any) => p.companyName.slice(0, 10) + (p.companyName.length > 10 ? '..' : ''));
    const data = pending.map((p: any) => p.amount);

    this.outstandingChart = new Chart(ctx, {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [{
          label: 'Outstanding (₹)',
          data: data,
          backgroundColor: '#ef4444',
          borderRadius: 8,
          barThickness: 20
        }]
      },
      options: {
        indexAxis: 'y',
        responsive: true,
        maintainAspectRatio: false,
        plugins: { legend: { display: false } },
        scales: {
          x: { grid: { display: false }, ticks: { font: { size: 9 } } },
          y: { grid: { display: false }, ticks: { font: { size: 9, weight: 600 } } }
        }
      }
    });
  }

  private initSalesMixChart(ctx: HTMLCanvasElement) {
    const policies = this.policies();
    const mix: { [key: string]: number } = {};
    policies.forEach(p => { mix[p.planName] = (mix[p.planName] || 0) + 1; });

    this.salesMixChart = new Chart(ctx, {
      type: 'pie',
      data: {
        labels: Object.keys(mix),
        datasets: [{
          data: Object.values(mix),
          backgroundColor: ['#3b82f6', '#10b981', '#f59e0b', '#8b5cf6', '#ec4899'],
          borderWidth: 0
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          legend: {
            position: 'bottom',
            labels: { boxWidth: 8, font: { size: 9, weight: 600 }, padding: 10, usePointStyle: true }
          }
        }
      }
    });
  }
}

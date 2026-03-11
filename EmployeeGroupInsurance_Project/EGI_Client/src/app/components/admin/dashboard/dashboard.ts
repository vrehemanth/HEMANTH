import { Component, inject, signal, OnInit, OnDestroy, computed, viewChild, ElementRef, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminService } from '../../../data-access/api.services';
import { FormsModule } from '@angular/forms';
import { Router, NavigationEnd } from '@angular/router';
import { filter, Subscription, forkJoin, of, catchError } from 'rxjs';
import { ToastService } from '../../../core/services/toast.service';
import { Chart, registerables } from 'chart.js';
Chart.register(...registerables);

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './dashboard.html'
})
export class AdminDashboardComponent implements OnInit, OnDestroy {
  private adminService = inject(AdminService);
  private router = inject(Router);
  private toastService = inject(ToastService);

  activeTab = signal<'dashboard' | 'plans' | 'clients' | 'staff' | 'policies' | 'claims' | 'logs'>('dashboard');
  routerSub: Subscription | undefined;
  isLoading = signal(false);

  // --- Chart Canvases ---
  financialCanvas = viewChild<ElementRef<HTMLCanvasElement>>('financialChart');
  claimTrendCanvas = viewChild<ElementRef<HTMLCanvasElement>>('claimTrendChart');
  planMixCanvas = viewChild<ElementRef<HTMLCanvasElement>>('planMixChart');

  private financialChart?: Chart;
  private claimTrendChart?: Chart;
  private planMixChart?: Chart;

  // --- Chart Lifecycle Effect ---
  private chartEffect = effect(() => {
    const fCanvas = this.financialCanvas();
    const cCanvas = this.claimTrendCanvas();
    const pCanvas = this.planMixCanvas();

    // Track signals for reactivity
    this.summary();
    this.allClaimsRegistry();
    this.allPolicyAssignments();

    if (fCanvas && cCanvas && pCanvas && this.activeTab() === 'dashboard') {
      this.destroyCharts();
      setTimeout(() => this.initCharts(), 300);
    }
  });

  summary = signal<any>(null);
  pendingClients = signal<any[]>([]);
  allClients = signal<any[]>([]);
  allPolicyAssignments = signal<any[]>([]);
  allClaimsRegistry = signal<any[]>([]);
  auditLogs = signal<any[]>([]);

  // --- Filtering & Sorting Signals ---
  claimSearchTerm = signal('');
  claimStatusFilter = signal('All');
  claimSortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'date', direction: 'desc' });

  clientSearchTerm = signal('');
  clientSortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'companyName', direction: 'asc' });

  policySearchTerm = signal('');
  policySortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'policyNo', direction: 'asc' });

  logSearchTerm = signal('');
  logDateFilter = signal('All');
  logSortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'timestamp', direction: 'desc' });

  planSortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'planCode', direction: 'asc' });
  agentSortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'name', direction: 'asc' });
  officerSortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'name', direction: 'asc' });

  // --- Filtered Computeds ---
  filteredClaims = computed(() => {
    let result = this.allClaimsRegistry();
    const statusIdx = this.claimStatusFilter();
    if (statusIdx !== 'All') {
      result = result.filter(c => c.status === statusIdx);
    }
    const search = this.claimSearchTerm().toLowerCase().trim();
    if (search) {
      result = result.filter(c =>
        (c.claimNumber && c.claimNumber.toLowerCase().includes(search)) ||
        (c.memberName && c.memberName.toLowerCase().includes(search)) ||
        (c.claimType && c.claimType.toLowerCase().includes(search))
      );
    }
    const sort = this.claimSortConfig();
    return [...result].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'id': comparison = (a.claimNumber || '').localeCompare(b.claimNumber || ''); break;
        case 'date': comparison = new Date(a.claimDate || 0).getTime() - new Date(b.claimDate || 0).getTime(); break;
        case 'amount': comparison = a.claimAmount - b.claimAmount; break;
        case 'member': comparison = (a.memberName || '').localeCompare(b.memberName || ''); break;
        case 'status': comparison = (a.status || '').localeCompare(b.status || ''); break;
        case 'reviewer': comparison = (a.reviewerName || '').localeCompare(b.reviewerName || ''); break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  filteredClients = computed(() => {
    let result = this.allClients();
    const search = this.clientSearchTerm().toLowerCase().trim();
    if (search) {
      result = result.filter(c =>
        (c.companyName && c.companyName.toLowerCase().includes(search)) ||
        (c.address && c.address.toLowerCase().includes(search))
      );
    }
    const sort = this.clientSortConfig();
    return [...result].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'companyName': comparison = (a.companyName || '').localeCompare(b.companyName || ''); break;
        case 'address': comparison = (a.address || '').localeCompare(b.address || ''); break;
        case 'status': comparison = (a.status || '').localeCompare(b.status || ''); break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  filteredPolicies = computed(() => {
    let result = this.allPolicyAssignments();
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
        case 'status': comparison = (a.status || '').localeCompare(b.status || ''); break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  filteredLogs = computed(() => {
    let result = this.auditLogs();
    const search = this.logSearchTerm().toLowerCase().trim();
    const dateFilter = this.logDateFilter();

    if (dateFilter !== 'All') {
      const today = new Date();
      result = result.filter(log => {
        const logDate = new Date(log.timestamp);
        if (dateFilter === 'Today') {
          return logDate.toDateString() === today.toDateString();
        } else if (dateFilter === 'Past 7 Days') {
          const sevenDaysAgo = new Date(today.getTime() - (7 * 24 * 60 * 60 * 1000));
          return logDate >= sevenDaysAgo;
        }
        return true;
      });
    }

    if (search) {
      result = result.filter(l =>
        (l.action && l.action.toLowerCase().includes(search)) ||
        (l.entityAffected && l.entityAffected.toLowerCase().includes(search)) ||
        (l.ipAddress && l.ipAddress.toLowerCase().includes(search))
      );
    }

    const sort = this.logSortConfig();
    return [...result].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'timestamp': comparison = new Date(a.timestamp || 0).getTime() - new Date(b.timestamp || 0).getTime(); break;
        case 'action': comparison = (a.action || '').localeCompare(b.action || ''); break;
        case 'entity': comparison = (a.entityAffected || '').localeCompare(b.entityAffected || ''); break;
        case 'user': comparison = (a.userId || '').localeCompare(b.userId || ''); break;
        case 'ip': comparison = (a.ipAddress || '').localeCompare(b.ipAddress || ''); break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  filteredPlans = computed(() => {
    const sort = this.planSortConfig();
    return [...this.plans()].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'planCode': comparison = (a.planCode || '').localeCompare(b.planCode || ''); break;
        case 'planName': comparison = (a.planName || '').localeCompare(b.planName || ''); break;
        case 'premium': comparison = (a.basePremium || 0) - (b.basePremium || 0); break;
        case 'status': comparison = Number(b.status) - Number(a.status); break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  filteredAgents = computed(() => {
    const sort = this.agentSortConfig();
    return [...this.agents()].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'name': comparison = (a.name || '').localeCompare(b.name || ''); break;
        case 'email': comparison = (a.email || '').localeCompare(b.email || ''); break;
        case 'status': comparison = (a.status || '').localeCompare(b.status || ''); break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  filteredOfficers = computed(() => {
    const sort = this.officerSortConfig();
    return [...this.claimsOfficers()].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'name': comparison = (a.name || '').localeCompare(b.name || ''); break;
        case 'email': comparison = (a.email || '').localeCompare(b.email || ''); break;
        case 'status': comparison = (a.status || '').localeCompare(b.status || ''); break;
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

  sortClaims(column: string) { this.updateSort(this.claimSortConfig, column); }
  sortClients(column: string) { this.updateSort(this.clientSortConfig, column); }
  sortPolicies(column: string) { this.updateSort(this.policySortConfig, column); }
  sortLogs(column: string) { this.updateSort(this.logSortConfig, column); }
  sortPlans(column: string) { this.updateSort(this.planSortConfig, column); }
  sortAgents(column: string) { this.updateSort(this.agentSortConfig, column); }
  sortOfficers(column: string) { this.updateSort(this.officerSortConfig, column); }

  selectedPolicyHistory = signal<any>(null);
  policyInvoices = signal<any[]>([]);
  policyEndorsements = signal<any[]>([]);
  selectedClaimDetail = signal<any>(null);

  agents = signal<any[]>([]);
  claimsOfficers = signal<any[]>([]);
  plans = signal<any[]>([]);

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

  showNewPlanForm = signal(false);
  isEditingPlan = signal(false);
  editingPlanId: string | null = null;
  newPlan = { planCode: '', planName: '', description: '', basePremium: 0, status: true };

  isStaffLoading = signal(false);
  showNewStaffForm = signal(false);
  newStaff = { name: '', email: '', role: 'Agent' as any };

  approvalCategory: { [key: string]: string } = {};

  showRejectionModal = signal(false);
  rejectionForm = { clientId: '', reason: '' };

  ngOnInit() {
    this.updateTab(this.router.url);
    this.routerSub = this.router.events.pipe(
      filter(e => e instanceof NavigationEnd)
    ).subscribe((e: any) => this.updateTab(e.urlAfterRedirects));
  }

  ngOnDestroy() {
    this.routerSub?.unsubscribe();
  }

  updateTab(url: string) {
    let tab: 'dashboard' | 'plans' | 'clients' | 'staff' | 'policies' | 'claims' | 'logs' = 'dashboard';
    if (url.includes('plans')) tab = 'plans';
    else if (url.includes('clients')) tab = 'clients';
    else if (url.includes('staff')) tab = 'staff';
    else if (url.includes('policies')) tab = 'policies';
    else if (url.includes('claims')) tab = 'claims';
    else if (url.includes('logs')) tab = 'logs';

    this.activeTab.set(tab);
    this.lazyLoadTab(tab);
  }

  private pendingReqs = new Set<string>();

  lazyLoadTab(tab: string) {
    if (tab === 'dashboard') {
      this.loadDashboardData();
    } else if (tab === 'plans') {
      if (this.plans().length === 0) this.loadPlansData();
    } else if (tab === 'clients') {
      if (this.allClients().length === 0) this.loadClientsData();
    } else if (tab === 'staff') {
      if (this.agents().length === 0) this.loadStaffData();
    } else if (tab === 'policies') {
      if (this.allPolicyAssignments().length === 0) this.loadPoliciesData();
    } else if (tab === 'claims') {
      if (this.allClaimsRegistry().length === 0) this.loadClaimsRegistry();
    } else if (tab === 'logs') {
      if (this.auditLogs().length === 0) this.loadAuditLogs();
    }
  }

  loadDashboardData() {
    if (this.pendingReqs.has('dashboard')) return;
    this.pendingReqs.add('dashboard');
    this.adminService.getSummary().subscribe({
      next: (res: any) => {
        this.summary.set(res?.data || res);

        // Trigger registry loads for charts
        this.loadClaimsRegistry();
        this.loadPoliciesData();

        this.pendingReqs.delete('dashboard');
      },
      error: () => this.pendingReqs.delete('dashboard')
    });
  }

  private destroyCharts() {
    this.financialChart?.destroy();
    this.claimTrendChart?.destroy();
    this.planMixChart?.destroy();
  }

  private initCharts() {
    const fEl = this.financialCanvas()?.nativeElement;
    const cEl = this.claimTrendCanvas()?.nativeElement;
    const pEl = this.planMixCanvas()?.nativeElement;

    if (!fEl || !cEl || !pEl) return;

    this.initFinancialChart(fEl);
    this.initClaimTrendChart(cEl);
    this.initPlanMixChart(pEl);
  }

  private initFinancialChart(ctx: HTMLCanvasElement) {
    const s = this.summary();
    const revenue = s?.totalRevenue || 0;
    const payouts = s?.totalPayouts || 0;
    const commissions = s?.totalCommissionPayouts || 0;
    const profit = s?.netProfit || 0;

    this.financialChart = new Chart(ctx, {
      type: 'doughnut',
      data: {
        labels: ['Net Profit', 'Claim Payouts', 'Agent Commissions'],
        datasets: [{
          data: [profit, payouts, commissions],
          backgroundColor: ['#10b981', '#f59e0b', '#6366f1'],
          borderWidth: 0,
          borderRadius: 8,
          hoverOffset: 15
        }]
      },
      options: {
        cutout: '75%',
        responsive: true,
        maintainAspectRatio: false,
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

  private initClaimTrendChart(ctx: HTMLCanvasElement) {
    const claims = this.allClaimsRegistry();
    if (claims.length === 0) return;

    const months = [];
    for (let i = 5; i >= 0; i--) {
      const d = new Date();
      d.setMonth(d.getMonth() - i);
      months.push(d.toLocaleString('default', { month: 'short' }));
    }

    const groupedTotal = new Array(6).fill(0);
    const groupedApproved = new Array(6).fill(0);

    claims.forEach(cl => {
      const date = new Date(cl.claimDate);
      const diffMonths = (new Date().getFullYear() - date.getFullYear()) * 12 + (new Date().getMonth() - date.getMonth());
      if (diffMonths >= 0 && diffMonths < 6) {
        const idx = 5 - diffMonths;
        groupedTotal[idx] += 1;
        if (cl.status === 'Approved') groupedApproved[idx] += 1;
      }
    });

    const grad = ctx.getContext('2d')?.createLinearGradient(0, 0, 0, 400);
    grad?.addColorStop(0, 'rgba(59, 130, 246, 0.4)');
    grad?.addColorStop(1, 'rgba(59, 130, 246, 0)');

    this.claimTrendChart = new Chart(ctx, {
      type: 'line',
      data: {
        labels: months,
        datasets: [{
          label: 'Total Claims',
          data: groupedTotal,
          borderColor: '#3b82f6',
          backgroundColor: grad,
          fill: true,
          tension: 0.4,
          pointRadius: 6,
          pointBackgroundColor: '#3b82f6',
          borderWidth: 3
        }, {
          label: 'Approved',
          data: groupedApproved,
          borderColor: '#10b981',
          borderWidth: 2,
          pointRadius: 0,
          borderDash: [5, 5]
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

  private initPlanMixChart(ctx: HTMLCanvasElement) {
    const policies = this.allPolicyAssignments();
    const mix: { [key: string]: number } = {};
    policies.forEach(p => { mix[p.planName] = (mix[p.planName] || 0) + 1; });

    this.planMixChart = new Chart(ctx, {
      type: 'bar',
      data: {
        labels: Object.keys(mix),
        datasets: [{
          label: 'Active Policies',
          data: Object.values(mix),
          backgroundColor: '#6366f1',
          borderRadius: 8,
          barThickness: 24
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: { legend: { display: false } },
        scales: {
          x: { grid: { display: false }, ticks: { font: { size: 10, weight: 700 } } },
          y: { beginAtZero: true, grid: { color: 'rgba(0,0,0,0.05)' }, ticks: { stepSize: 1, font: { size: 10 } } }
        }
      }
    });
  }

  loadClientsData() {
    if (this.pendingReqs.has('clients')) return;
    this.pendingReqs.add('clients');

    // Breaking forkJoin for instant render
    this.adminService.getPendingClients().subscribe({
      next: (res: any) => {
        const pendingData = this.extractArray(res);
        this.pendingClients.set(pendingData);
        pendingData.forEach((r: any) => { if (!this.approvalCategory[r.id]) this.approvalCategory[r.id] = 'Enterprise'; });
      }
    });

    this.adminService.getAllClients().subscribe({
      next: (res: any) => {
        this.allClients.set(this.extractArray(res));
        this.pendingReqs.delete('clients');
      },
      error: () => this.pendingReqs.delete('clients')
    });
  }

  loadStaffData() {
    if (this.pendingReqs.has('staff')) return;
    this.pendingReqs.add('staff');

    this.adminService.getStaff('Agent').subscribe({
      next: (res: any) => {
        this.agents.set(this.extractArray(res));
      }
    });

    this.adminService.getStaff('ClaimsOfficer').subscribe({
      next: (res: any) => {
        this.claimsOfficers.set(this.extractArray(res));
        this.pendingReqs.delete('staff');
      },
      error: () => this.pendingReqs.delete('staff')
    });
  }

  loadPlansData() {
    if (this.pendingReqs.has('plans')) return;
    this.pendingReqs.add('plans');
    this.adminService.getAllPlans().subscribe({
      next: (res: any) => {
        this.plans.set(this.extractArray(res));
        this.pendingReqs.delete('plans');
      },
      error: () => this.pendingReqs.delete('plans')
    });
  }

  loadPoliciesData() {
    if (this.pendingReqs.has('policies')) return;
    this.pendingReqs.add('policies');
    this.adminService.getPolicyAssignments().subscribe({
      next: (res: any) => {
        this.allPolicyAssignments.set(this.extractArray(res));
        this.pendingReqs.delete('policies');
      },
      error: () => this.pendingReqs.delete('policies')
    });
  }

  loadClaimsRegistry() {
    if (this.pendingReqs.has('claimsRegistry')) return;
    this.pendingReqs.add('claimsRegistry');
    this.adminService.getClaims().subscribe({
      next: (res: any) => {
        this.allClaimsRegistry.set(this.extractArray(res));
        this.pendingReqs.delete('claimsRegistry');
      },
      error: () => this.pendingReqs.delete('claimsRegistry')
    });
  }

  loadAuditLogs() {
    if (this.pendingReqs.has('logs')) return;
    this.pendingReqs.add('logs');
    this.adminService.getAuditLogs().subscribe({
      next: (res: any) => {
        this.auditLogs.set(this.extractArray(res));
        this.pendingReqs.delete('logs');
      },
      error: () => this.pendingReqs.delete('logs')
    });
  }


  viewPolicyDetails(policy: any) {
    this.selectedPolicyHistory.set(policy);
    this.policyInvoices.set([]);
    this.policyEndorsements.set([]);

    // Fetch financial history
    this.adminService.getInvoicesByPolicy(policy.id).subscribe(res => {
      this.policyInvoices.set(this.extractArray(res));
    });

    // Fetch endorsement history
    this.adminService.getEndorsementsByPolicy(policy.id).subscribe(res => {
      this.policyEndorsements.set(this.extractArray(res));
    });
  }

  viewClaimDetails(id: string) {
    this.adminService.getClaimDetail(id).subscribe(res => {
      this.selectedClaimDetail.set(res.data || res);
    });
  }

  viewInvoicePayments(invId: string) {
    this.adminService.getPayments(invId).subscribe(res => {
      const payments = this.extractArray(res);
      if (payments.length === 0) {
        this.toastService.info("No payments received for this invoice yet.");
        return;
      }
      let msg = "Payment History:\n";
      payments.forEach((p: any) => {
        msg += `- ₹${p.paidAmount} via ${p.paymentMethod} on ${new Date(p.paymentDate).toLocaleDateString()} (${p.status})\n`;
      });
      this.toastService.info(msg);
    });
  }

  reviewClient(id: string, isApproved: boolean) {
    if (!isApproved) {
      this.rejectionForm = { clientId: id, reason: 'Application documents are incomplete or invalid.' };
      this.showRejectionModal.set(true);
      return;
    }

    const dto = {
      isApproved: true,
      rejectionReason: '',
      businessCategory: this.approvalCategory[id] || 'Small'
    };
    this.adminService.approveClient(id, dto).subscribe(() => {
      this.toastService.success('Corporate entity approved successfully.');
      this.loadClientsData();
      this.loadDashboardData();
    });
  }

  cancelRejection() {
    this.showRejectionModal.set(false);
    this.rejectionForm = { clientId: '', reason: '' };
  }

  submitRejection() {
    if (!this.rejectionForm.reason.trim()) {
      this.toastService.warning("A rejection reason is mandatory.");
      return;
    }

    const dto = {
      isApproved: false,
      rejectionReason: this.rejectionForm.reason,
      businessCategory: 'Small'
    };

    this.adminService.approveClient(this.rejectionForm.clientId, dto).subscribe({
      next: () => {
        this.toastService.success('Corporate entity application rejected.');
        this.cancelRejection();
        this.loadClientsData();
        this.loadDashboardData();
      },
      error: (err) => {
        this.toastService.error(err.error?.message || "Failed to reject application.");
      }
    });
  }

  toggleStaffStatus(id: string) {
    this.adminService.toggleUserStatus(id).subscribe(() => {
      this.loadStaffData();
    });
  }

  toggleNewStaffForm() {
    this.showNewStaffForm.update(v => !v);
    this.newStaff = { name: '', email: '', role: 'Agent' };
  }

  createStaff() {
    if (!this.newStaff.name || !this.newStaff.email) {
      this.toastService.error('Name and Email are strictly required.');
      return;
    }

    this.isStaffLoading.set(true);
    console.log('[DEBUG] Onboarding staff:', this.newStaff);

    this.adminService.createStaff(this.newStaff).subscribe({
      next: () => {
        this.toastService.success('Staff onboarding successful. Credentials have been dispatched to their email.');
        this.isStaffLoading.set(false);
        this.showNewStaffForm.set(false);
        this.loadStaffData();
        this.loadDashboardData();
      },
      error: (err) => {
        console.error('[ERROR] Onboarding failed:', err);
        this.toastService.error(err.error?.message || 'Onboarding failed. Please verify the email is unique.');
        this.isStaffLoading.set(false);
      }
    });
  }

  toggleNewPlanForm() {
    if (this.showNewPlanForm()) {
      this.showNewPlanForm.set(false);
      this.isEditingPlan.set(false);
      this.editingPlanId = null;
      this.newPlan = { planCode: '', planName: '', description: '', basePremium: 0, status: true };
    } else {
      this.showNewPlanForm.set(true);
    }
  }

  editPlan(plan: any) {
    this.isEditingPlan.set(true);
    this.editingPlanId = plan.id;
    this.newPlan = {
      planCode: plan.planCode,
      planName: plan.planName,
      description: plan.description,
      basePremium: plan.basePremium,
      status: plan.status
    };
    this.showNewPlanForm.set(true);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  savePlan() {
    if (this.isEditingPlan()) {
      this.updatePlan();
    } else {
      this.createPlan();
    }
  }

  createPlan() {
    if (!this.newPlan.planName || !this.newPlan.planCode || this.newPlan.basePremium <= 0) {
      this.toastService.warning("All package elements strictly required.");
      return;
    }
    this.adminService.createPlan(this.newPlan).subscribe(() => {
      this.toastService.success('Package Created AND Deployed.');
      this.toggleNewPlanForm();
      this.loadPlansData();
    });
  }

  updatePlan() {
    if (!this.editingPlanId) return;

    const updateDto = {
      planName: this.newPlan.planName,
      basePremium: this.newPlan.basePremium,
      description: this.newPlan.description,
      status: this.newPlan.status,
      coverages: [] // Keeping simplest update for now as per DTO
    };

    this.adminService.updatePlan(this.editingPlanId, updateDto).subscribe(() => {
      this.toastService.success('Package Updated Successfully.');
      this.toggleNewPlanForm();
      this.loadPlansData();
    });
  }

  deactivatePlan(id: string) {
    if (confirm("Deactivate this plan? It will be marked Inactive and hidden from new assignments, but existing policies are preserved.")) {
      this.adminService.deactivatePlan(id).subscribe(() => {
        this.toastService.success('Plan deactivated successfully.');
        this.loadPlansData();
      });
    }
  }

  hardDeletePlan(id: string) {
    if (confirm("⚠️ PERMANENT DELETE — This will permanently remove the plan from the database. This action CANNOT be undone. Are you absolutely sure?")) {
      this.adminService.deletePlan(id).subscribe(() => {
        this.toastService.success('Plan permanently deleted.');
        this.loadPlansData();
      });
    }
  }


  getActionClass(action: string): string {
    const a = action.toLowerCase();
    if (a.includes('add')) return 'bg-emerald-50 text-emerald-700 border-emerald-200';
    if (a.includes('mod')) return 'bg-blue-50 text-blue-700 border-blue-200';
    if (a.includes('del')) return 'bg-red-50 text-red-700 border-red-200';
    return 'bg-gray-50 text-gray-500 border-gray-200';
  }

  formatJson(json: string | null | undefined): string {
    if (!json) return 'None';
    try {
      const data = JSON.parse(json);
      if (Array.isArray(data)) return data.join(', ');

      return Object.entries(data)
        .filter(([key, val]) => val !== null && val !== undefined && val !== '')
        .map(([key, val]) => `${key}: ${val}`)
        .join(' | ');
    } catch {
      return json;
    }
  }

  exportData() {
    let data: any[] = [];
    let filename = 'EGI_Report_';

    switch (this.activeTab()) {
      case 'claims':
        data = this.allClaimsRegistry();
        filename += 'Claims.csv';
        break;
      case 'logs':
        data = this.auditLogs();
        filename += 'AuditLogs.csv';
        break;
      case 'policies':
        data = this.allPolicyAssignments();
        filename += 'Policies.csv';
        break;
      case 'clients':
        data = this.allClients();
        filename += 'Clients.csv';
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

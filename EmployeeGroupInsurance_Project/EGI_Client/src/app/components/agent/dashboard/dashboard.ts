import { Component, inject, signal, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, NavigationEnd } from '@angular/router';
import { AgentService } from '../../../data-access/api.services';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { ToastService } from '../../../core/services/toast.service'; // Added ToastService import

@Component({
  selector: 'app-agent-dashboard',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './dashboard.html'
})
export class AgentDashboardComponent implements OnInit, OnDestroy {
  agentService = inject(AgentService);
  router = inject(Router);
  fb = inject(FormBuilder);
  toastService = inject(ToastService);
  private routerSub?: Subscription;

  activeTab = signal<'overview' | 'customers' | 'policies' | 'endorsements' | 'commissions'>('overview');
  pageTitle = signal('Agent Dashboard');
  showCustomerForm = signal(false);
  isLoading = signal(false);

  summary = signal<any>(null);
  customers = signal<any[]>([]);
  policies = signal<any[]>([]);
  pendingEndorsements = signal<any[]>([]);
  commissionLogs = signal<any[]>([]);

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
        next: res => { this.summary.set(res?.data || res); this.pendingReqs.delete('overview'); },
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
}

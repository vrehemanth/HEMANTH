import { Component, inject, signal, OnInit, OnDestroy, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerService } from '../../../data-access/customer.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, NavigationEnd } from '@angular/router';
import { Subscription, forkJoin, of } from 'rxjs';
import { filter, catchError } from 'rxjs/operators';
import { ToastService } from '../../../core/services/toast.service';
import { AuthService } from '../../../core/services/auth.service';
import * as htmlToImage from 'html-to-image';
import { jsPDF } from 'jspdf';

import { OverviewTabComponent } from './tabs/overview/overview';
import { ProfileTabComponent } from './tabs/profile/profile';
import { PoliciesTabComponent } from './tabs/policies/policies';
import { ClaimsTabComponent } from './tabs/claims/claims';
import { BillingTabComponent } from './tabs/billing/billing';
import { RevisionsTabComponent } from './tabs/revisions/revisions';

import {
  RelationshipType,
  DocumentType,
  ClaimDocumentType,
  BillingFrequency,
  EndorsementType,
  PaymentMethod,
  Gender
} from '../../../core/models/models';

@Component({
  selector: 'app-customer-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    OverviewTabComponent,
    ProfileTabComponent,
    PoliciesTabComponent,
    ClaimsTabComponent,
    BillingTabComponent,
    RevisionsTabComponent
  ],
  templateUrl: './dashboard.html'
})
export class CustomerDashboardComponent implements OnInit, OnDestroy {
  customerService = inject(CustomerService);
  fb = inject(FormBuilder);
  router = inject(Router);
  toastService = inject(ToastService);
  authService = inject(AuthService);
  private routerSub?: Subscription;


  activeTab = signal<'overview' | 'profile' | 'policies' | 'claims' | 'billing' | 'revisions'>('overview');
  pageTitle = signal('Strategic Portal');
  isLoading = signal(false);
  isBlocked = signal(false);

  summary = signal<any>(null);
  policies = signal<any[]>([]);
  members = signal<any[]>([]);
  claims = signal<any[]>([]);
  invoices = signal<any[]>([]);
  endorsements = signal<any[]>([]);
  plans = signal<any[]>([]);
  profileData = signal<any>(null);

  // --- Data Loading Flags (Ensures full lists are fetched even if partials exist from Overview) ---
  fullInvoicesLoaded = signal(false);
  fullClaimsLoaded = signal(false);
  fullMembersLoaded = signal(false);
  fullEndorsementsLoaded = signal(false);
  fullPoliciesLoaded = signal(false);

  // --- Wizard/Stepper State ---
  policyOnboardingStep = signal(1);
  claimSubmissionStep = signal(1);
  activeActionMenu = signal<string | null>(null);
  isProfileApproved = computed(() => this.profileData()?.status === 'Approved');

  isPremiumDueSoon = computed(() => {
    const today = new Date();
    const threeDaysFromNow = new Date();
    threeDaysFromNow.setDate(today.getDate() + 3);
    return this.invoices().some(inv => {
      if (inv.status === 'Paid') return false;
      const dueDate = new Date(inv.dueDate);
      return dueDate <= threeDaysFromNow && dueDate >= today;
    });
  });

  todayStr = signal(new Date().toISOString().split('T')[0]);
  maxStartDateStr = computed(() => {
    let d = new Date();
    d.setMonth(d.getMonth() + 1);
    return d.toISOString().split('T')[0];
  });

  // --- Grid Filters & Sorting State ---
  claimSearchTerm = signal('');
  claimStatusFilter = signal('All');
  claimSortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'date', direction: 'desc' });
  selectedClaimForView = signal<any | null>(null);
  showClaimDetailModal = signal(false);
  
  // --- Renewal State ---
  showRenewalModal = signal(false);
  activeRenewalPolicyId = signal<string | null>(null);
  renewalYears = signal(1);
  renewalFrequency = signal<number>(BillingFrequency.Annually);
  renewalQuote = signal<any>(null);
  isFetchingQuote = signal(false);

  // --- Filtered and Sorted Computeds ---
  filteredClaims = computed(() => {
    let result = this.claims();

    // 1. Filter by Status
    const statusIdx = this.claimStatusFilter();
    if (statusIdx !== 'All') {
      result = result.filter(c => c.status === statusIdx);
    }

    // 2. Filter by Search Term (Faceted Search)
    const search = this.claimSearchTerm().toLowerCase().trim();
    if (search) {
      result = result.filter(c =>
        (c.memberName && c.memberName.toLowerCase().includes(search)) ||
        (c.dependentName && c.dependentName.toLowerCase().includes(search)) ||
        (c.claimType && c.claimType.toLowerCase().includes(search)) ||
        (c.claimAmount && c.claimAmount.toString().includes(search))
      );
    }

    // 3. Sorting
    const sort = this.claimSortConfig();
    return [...result].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'date':
          comparison = new Date(a.claimDate).getTime() - new Date(b.claimDate).getTime();
          break;
        case 'amount':
          comparison = a.claimAmount - b.claimAmount;
          break;
        case 'name':
          comparison = (a.memberName || '').localeCompare(b.memberName || '');
          break;
        case 'type':
          comparison = (a.claimType || '').localeCompare(b.claimType || '');
          break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  // Method to update sorting
  sortClaims(column: string) {
    const current = this.claimSortConfig();
    if (current.column === column) {
      this.claimSortConfig.set({ column, direction: current.direction === 'asc' ? 'desc' : 'asc' });
    } else {
      this.claimSortConfig.set({ column, direction: 'asc' });
    }
  }

  getDocumentUrl(docId: string): string {
    const token = this.authService.currentUser()?.token;
    if (!token) return '';
    return `https://localhost:7146/api/Public/documents/${docId}?access_token=${token}`;
  }

  viewClaimDetail(claim: any) {
    this.selectedClaimForView.set(claim);
    this.showClaimDetailModal.set(true);
  }

  openRenewalModal(policyId: string) {
    this.activeRenewalPolicyId.set(policyId);
    this.renewalYears.set(1);
    this.renewalFrequency.set(BillingFrequency.Annually);
    this.renewalQuote.set(null);
    this.showRenewalModal.set(true);
    this.fetchRenewalQuote();
  }

  fetchRenewalQuote() {
    const id = this.activeRenewalPolicyId();
    if (!id) return;
    this.isFetchingQuote.set(true);
    this.customerService.getRenewalQuote(id, this.renewalYears(), this.renewalFrequency()).subscribe({
      next: (res) => {
        const quote = res?.data || res;
        this.renewalQuote.set(quote);
        this.isFetchingQuote.set(false);
      },
      error: (err) => {
        this.toastService.error(err.error?.message || 'Failed to fetch quote');
        this.isFetchingQuote.set(false);
      }
    });
  }

  updateRenewalYears(years: number) {
    this.renewalYears.set(years);
    this.fetchRenewalQuote();
  }

  updateRenewalFrequency(freq: number) {
    this.renewalFrequency.set(freq);
    this.fetchRenewalQuote();
  }

  confirmRenewal() {
    const id = this.activeRenewalPolicyId();
    if (!id) return;
    
    this.isLoading.set(true);
    this.customerService.renewPolicy(id, this.renewalYears(), this.renewalFrequency()).subscribe({
      next: (res) => {
        this.showRenewalModal.set(false);
        this.customerService.getMyPolicies().subscribe(p => {
          this.policies.set(this.extractArray(p));
          this.refreshSummary();
          this.isLoading.set(false);
          this.toastService.success(res.message);
        });
      },
      error: (err) => {
        this.toastService.error(err.error?.message || 'Failed to renew policy');
        this.isLoading.set(false);
      }
    });
  }

  // --- Grid Filters & Sorting State for Members ---
  memberSearchTerm = signal('');
  memberStatusFilter = signal('All');
  memberSortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'name', direction: 'asc' });

  // --- Filtered and Sorted Computeds for Members ---
  filteredMembersList = computed(() => {
    let result = this.members();

    // 1. Filter by Status
    const statusIdx = this.memberStatusFilter();
    if (statusIdx !== 'All') {
      const isActive = statusIdx === 'Active';
      result = result.filter(m => m.status === isActive);
    }

    // 2. Filter by Search Term
    const search = this.memberSearchTerm().toLowerCase().trim();
    if (search) {
      result = result.filter(m =>
        (m.fullName && m.fullName.toLowerCase().includes(search)) ||
        (m.relationship && m.relationship.toLowerCase().includes(search))
      );
    }

    // 3. Sorting
    const sort = this.memberSortConfig();
    return [...result].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'name':
          comparison = (a.fullName || '').localeCompare(b.fullName || '');
          break;
        case 'relationship':
          comparison = (a.relationship || '').localeCompare(b.relationship || '');
          break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  // Method to update member sorting
  sortMembers(column: string) {
    const current = this.memberSortConfig();
    if (current.column === column) {
      this.memberSortConfig.set({ column, direction: current.direction === 'asc' ? 'desc' : 'asc' });
    } else {
      this.memberSortConfig.set({ column, direction: 'asc' });
    }
  }

  // --- Grid Filters & Sorting State for Invoices ---
  invoiceSearchTerm = signal('');
  invoiceStatusFilter = signal('All');
  invoiceSortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'date', direction: 'desc' });

  // --- Filtered and Sorted Computeds for Invoices ---
  filteredInvoiceList = computed(() => {
    let result = this.invoices();

    // 1. Filter by Status
    const statusIdx = this.invoiceStatusFilter();
    if (statusIdx !== 'All') {
      result = result.filter(i => i.status === statusIdx);
    }

    // 2. Filter by Search Term
    const search = this.invoiceSearchTerm().toLowerCase().trim();
    if (search) {
      result = result.filter(i =>
        (i.invoiceNo && i.invoiceNo.toLowerCase().includes(search)) ||
        (i.id && i.id.toLowerCase().includes(search)) ||
        (i.amount && i.amount.toString().includes(search))
      );
    }

    // 3. Sorting
    const sort = this.invoiceSortConfig();
    return [...result].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'date':
          comparison = new Date(a.invoiceDate).getTime() - new Date(b.invoiceDate).getTime();
          break;
        case 'amount':
          comparison = a.amount - b.amount;
          break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  // Method to update invoice sorting
  sortInvoices(column: string) {
    const current = this.invoiceSortConfig();
    if (current.column === column) {
      this.invoiceSortConfig.set({ column, direction: current.direction === 'asc' ? 'desc' : 'asc' });
    } else {
      this.invoiceSortConfig.set({ column, direction: 'asc' });
    }
  }

  // Computed signals for Claim Form filtering
  filteredMembers = computed(() => {
    const policyId = this.claimForm.get('policyAssignmentId')?.value;
    if (!policyId) return [];
    // Only show ACTIVE members (status === true)
    return this.members().filter(m => m.policyAssignmentId === policyId && m.status === true);
  });

  selectedClaimMember = signal<any>(null);
  selectedEndorsementMember = signal<any>(null);
  isInvalidEmployeeCode = signal(false);

  // Helper methods to navigate steps
  setPolicyStep(step: number) {
    if (step < 1) return;
    // Basic validation before moving forward
    if (step === 2 && !this.selectedPlanForOnboarding()) {
      this.toastService.warning('Please select a plan deployment blueprint first.');
      return;
    }
    if (step === 3 && (this.policyForm.get('billingFrequency')?.invalid || this.policyForm.get('startDate')?.invalid)) {
      this.toastService.warning('Please complete the billing and activation dates.');
      return;
    }
    if (step === 4 && (!this.membersFile)) {
      this.toastService.warning('Roster artifacts are required for deployment review.');
      return;
    }
    this.policyOnboardingStep.set(step);
  }

  setClaimStep(step: number) {
    if (step < 1) return;
    if (step === 2 && !this.claimForm.get('policyAssignmentId')?.value) {
      this.toastService.warning('Please select a target policy segment first.');
      return;
    }
    if (step === 3 && !this.selectedClaimMember()) {
      this.toastService.warning('Please verify a valid employee ID first.');
      return;
    }
    if (step === 4 && (this.claimForm.get('claimType')?.invalid || this.claimForm.get('claimAmount')?.invalid)) {
      if (this.claimForm.get('claimType')?.value === 'Life') {
        this.autoFillLifeClaimAmount();
      }
      this.toastService.warning('Please specify claim classification and value.');
      return;
    }
    this.claimSubmissionStep.set(step);

    // If moving to step 3 and it's life, ensure amount is filled
    if (step === 3 && this.claimForm.get('claimType')?.value === 'Life') {
      this.autoFillLifeClaimAmount();
    }
  }

  // Drilldown states
  selectedPolicy = signal<any>(null);
  policyInvoices = signal<any[]>([]);
  policyClaims = signal<any[]>([]);
  policyEndorsements = signal<any[]>([]);
  showEndorsementForm = signal(false);

  selectedInvoice = signal<any>(null);
  invoicePayments = signal<any[]>([]);

  showClaimForm = signal(false);
  toggleClaimForm() {
    this.showClaimForm.set(!this.showClaimForm());
    if (this.showClaimForm()) {
      this.claimSubmissionStep.set(1);
    } else {
      this.claimForm.reset();
      this.claimForm.get('claimAmount')?.enable();
    }
  }

  autoFillLifeClaimAmount() {
    const member = this.selectedClaimMember();
    if (!member) return;

    const policyId = this.claimForm.get('policyAssignmentId')?.value;
    const policy = this.policies().find(p => p.id === policyId);
    if (!policy) return;

    // Find the plan to get the specific "Life" coverage amount
    const plan = this.plans().find(pl => pl.id === policy.insurancePlanId || pl.planName === policy.planName);
    
    let lifeCoverageAmount = 0;
    if (plan) {
      const lifeCoverage = plan.coverages?.find((c: any) => c.type === 'Life');
      if (lifeCoverage) {
        lifeCoverageAmount = lifeCoverage.coverageAmount;
      }
    }

    // Fallback if specific plan coverage not found, but we really want the Plan's Life amount
    if (lifeCoverageAmount > 0) {
      this.claimForm.get('claimAmount')?.setValue(lifeCoverageAmount.toString());
      this.claimForm.get('claimAmount')?.disable();
      this.toastService.info(`Life Settlement auto-filled: ₹${this.formatINR(lifeCoverageAmount)}. Standard protocol enforced.`);
    } else {
      // If we can't find specific Life coverage, don't use the aggregate sumInsured as it includes Health/Accident
      this.claimForm.get('claimAmount')?.enable(); 
      this.toastService.warning("Specific Life coverage amount not found in plan. Please enter manually.");
    }
  }
  showPolicyForm = signal(false);
  showPaymentModal = signal(false);
  selectedInvoiceForPayment = signal<any>(null);
  selectedPlanForOnboarding = signal<any>(null);

  selectedProfileDoc: File | null = null;

  // Track selected files along with their document types
  selectedClaimFiles: { file: File, type: string }[] = [];

  // Document Types from Backend Enum
  docTypes = [
    { id: ClaimDocumentType.MedicalBill.toString(), name: 'Medical Bill' },
    { id: ClaimDocumentType.HospitalDischargeReport.toString(), name: 'Hospital Discharge' },
    { id: ClaimDocumentType.DoctorPrescription.toString(), name: 'Prescription' },
    { id: ClaimDocumentType.FIR.toString(), name: 'FIR (Accident)' },
    { id: ClaimDocumentType.AccidentReport.toString(), name: 'Accident Report' },
    { id: ClaimDocumentType.DeathCertificate.toString(), name: 'Death Certificate' },
    { id: ClaimDocumentType.PostMortemReport.toString(), name: 'Post Mortem Report' },
    { id: ClaimDocumentType.DiagnosisReport.toString(), name: 'Diagnosis Report' },
    { id: ClaimDocumentType.Other.toString(), name: 'Other' }
  ];

  // Making Enums available to the template
  EnumRelationship = RelationshipType;
  EnumEndorsement = EndorsementType;
  EnumBilling = BillingFrequency;
  EnumPayment = PaymentMethod;
  EnumGender = Gender;
  EnumDocType = DocumentType;
  EnumClaimDocType = ClaimDocumentType;
  membersFile: File | null = null;
  dependentsFile: File | null = null;

  profileForm = this.fb.group({
    companyName: ['', Validators.required],
    address: ['', Validators.required],
    phone: ['', Validators.required]
  });

  claimForm = this.fb.group({
    policyAssignmentId: ['', Validators.required],
    employeeCode: ['', Validators.required],
    memberId: ['', Validators.required],
    dependentId: [{ value: '', disabled: true }],
    claimType: ['', Validators.required],
    claimAmount: ['', Validators.required],
    description: ['', Validators.required]
  });

  policyForm = this.fb.group({
    insurancePlanId: ['', Validators.required],
    billingFrequency: [BillingFrequency.Monthly, Validators.required],
    startDate: [new Date().toISOString().split('T')[0], Validators.required],
    durationInYears: [1, [Validators.required, Validators.min(1), Validators.max(10)]]
  });

  paymentForm = this.fb.group({
    amount: [0, [Validators.required, Validators.min(1)]],
    paymentMethod: [PaymentMethod.Card, Validators.required],
    transactionId: ['']
  });

  endorsementForm = this.fb.group({
    policyAssignmentId: ['', Validators.required],
    type: [EndorsementType.AddMember.toString(), Validators.required],
    description: ['', Validators.required],
    firstName: [''],
    lastName: [''],
    employeeCode: [''],
    email: [''],
    phoneNo: [''],
    dateOfBirth: [''],
    gender: [Gender.Male.toString()],
    memberId: [''],
    fullName: [''],
    relationship: [RelationshipType.Spouse.toString()]
  });

  // Helper for Indian Currency Formatting (Thousands and Lakhs)
  formatINR(val: any): string {
    if (val === null || val === undefined) return '0';
    const num = typeof val === 'string' ? parseFloat(val) : val;
    return new Intl.NumberFormat('en-IN', {
      minimumFractionDigits: 0,
      maximumFractionDigits: 2
    }).format(num);
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

    this.initialLoad();

    this.claimForm.get('policyAssignmentId')?.valueChanges.subscribe(() => {
      this.claimForm.patchValue({ memberId: '', employeeCode: '', dependentId: '' });
      this.selectedClaimMember.set(null);
      this.claimForm.get('dependentId')?.disable();
    });

    this.claimForm.get('claimType')?.valueChanges.subscribe(type => {
      if (type === 'Life') {
        this.autoFillLifeClaimAmount();
      } else {
        this.claimForm.get('claimAmount')?.enable();
      }
    });

    this.claimForm.get('dependentId')?.valueChanges.subscribe(() => {
      if (this.claimForm.get('claimType')?.value === 'Life') {
        this.autoFillLifeClaimAmount();
      }
    });
  }




  // Chart initialization moved to OverviewTabComponent





  ngOnDestroy() {
    this.routerSub?.unsubscribe();
  }

  // OPTIMIZATION: Track pending to prevent duplicate HTTP calls
  private pendingReqs = new Set<string>();

  // OPTIMIZATION: Initial load captures core overview state at once WITHOUT forkJoin blocking
  initialLoad() {
    this.isLoading.set(true);

    // Reduced immediate loading - only fetch what's needed for Overview
    this.customerService.getOverview().pipe(catchError(() => of(null))).subscribe(res => {
      if (res) {
        const ov = res?.data || res;
        this.summary.set(ov.summary);

        // Prevent overwriting full lists if they were already fetched by lazyLoadTab
        if (!this.fullPoliciesLoaded()) this.policies.set(this.extractArray(ov.recentPolicies));
        if (!this.fullClaimsLoaded()) this.claims.set(this.extractArray(ov.recentClaims));
        if (!this.fullInvoicesLoaded()) this.invoices.set(this.extractArray(ov.recentInvoices));
        if (!this.fullEndorsementsLoaded()) this.endorsements.set(this.extractArray(ov.recentEndorsements));

        // Process members if present in overview
        if (ov.recentMembers && !this.fullMembersLoaded()) {
          const rawMembers = this.extractArray(ov.recentMembers);
          this.members.set(rawMembers.map(m => ({
            ...m,
            dependents: this.extractArray(m.dependents)
          })));
        }
      }
      this.isLoading.set(false);

      // --- Proactive Fetch for Accurate Charts (Overview depends on this) ---
      if (!this.fullInvoicesLoaded()) {
        this.customerService.getMyInvoices().subscribe({
          next: res => { this.invoices.set(this.extractArray(res)); this.fullInvoicesLoaded.set(true); }
        });
      }
      if (!this.fullClaimsLoaded()) {
        this.customerService.getMyClaims().subscribe({
          next: res => { this.claims.set(this.extractArray(res)); this.fullClaimsLoaded.set(true); }
        });
      }
    });

    this.customerService.getAllPlans().pipe(catchError(() => of([]))).subscribe(res => {
      this.plans.set(this.extractArray(res));
    });

    this.customerService.getProfile().pipe(catchError(() => of(null))).subscribe(res => {
      if (res) this.handleProfileData(res?.data || res);
      else this.handleProfileData({ status: 'Draft' });
    });
  }

  private handleProfileData(data: any) {
    this.profileData.set(data);
    if (data) {
      this.isBlocked.set(!!data.isBlocked);
      this.profileForm.patchValue({
        companyName: data.companyName,
        address: data.address,
        phone: data.phone
      });

      // Force navigation to profile if not approved OR blocked
      if (data.isBlocked) {
        this.router.navigate(['/account-blocked']);
        return;
      }
      if (data.status !== 'Approved') {
        this.activeTab.set('profile');
        this.pageTitle.set(this.getTitle('profile'));
        this.profileForm.enable();
      } else {
        this.profileForm.disable();
      }
    }
  }

  setActiveTab(tab: any) {
    const data = this.profileData();
    if (data && data.isBlocked) {
      this.router.navigate(['/account-blocked']);
      return;
    }
    if (data && data.status !== 'Approved' && tab !== 'profile') {
      this.toastService.warning("Account verification pending. Please complete your profile first.");
      this.activeTab.set('profile');
      this.pageTitle.set(this.getTitle('profile'));
      return;
    }
    this.activeTab.set(tab);
    this.pageTitle.set(this.getTitle(tab));
    this.lazyLoadTab(tab);
  }

  updateTab(url: string) {
    let tab: any = 'overview';
    if (url.includes('/profile')) tab = 'profile';
    else if (url.includes('/policies')) tab = 'policies';
    else if (url.includes('/invoices')) tab = 'billing';
    else if (url.includes('/claims')) tab = 'claims';
    else if (url.includes('/revisions')) tab = 'revisions';

    this.setActiveTab(tab);
  }

  private getTitle(tab: string): string {
    switch (tab) {
      case 'profile': return 'Identity & Verification';
      case 'policies': return 'Policies & Member Pool';
      case 'billing': return 'Financial Settlement';
      case 'claims': return 'Adjudication Ledger';
      case 'revisions': return 'Policy Revision Ledger';
      default: return 'Strategic Overview';
    }
  }

  lazyLoadTab(tab: string) {
    if (tab === 'policies') {
      if (!this.fullPoliciesLoaded() && !this.pendingReqs.has('policies')) {
        this.pendingReqs.add('policies');
        this.customerService.getMyPolicies().subscribe({
          next: res => {
            this.policies.set(this.extractArray(res));
            this.fullPoliciesLoaded.set(true);
            this.pendingReqs.delete('policies');
          },
          error: () => this.pendingReqs.delete('policies')
        });
      }
      if (!this.fullMembersLoaded() && !this.pendingReqs.has('members')) {
        this.pendingReqs.add('members');
        this.customerService.getMyMembers().subscribe({
          next: res => {
            const rawMembers = this.extractArray(res);
            this.members.set(rawMembers.map(m => ({
              ...m,
              dependents: this.extractArray(m.dependents)
            })));
            this.fullMembersLoaded.set(true);
            this.pendingReqs.delete('members');
          },
          error: () => this.pendingReqs.delete('members')
        });
      }
    } else if (tab === 'claims') {
      if (!this.fullClaimsLoaded() && !this.pendingReqs.has('claims')) {
        this.pendingReqs.add('claims');
        this.customerService.getMyClaims().subscribe({
          next: res => {
            this.claims.set(this.extractArray(res));
            this.fullClaimsLoaded.set(true);
            this.pendingReqs.delete('claims');
          },
          error: () => this.pendingReqs.delete('claims')
        });
      }
      if (!this.fullMembersLoaded() && !this.pendingReqs.has('members')) {
        this.pendingReqs.add('members');
        this.customerService.getMyMembers().subscribe({
          next: res => {
            const rawMembers = this.extractArray(res);
            this.members.set(rawMembers.map(m => ({
              ...m,
              dependents: this.extractArray(m.dependents)
            })));
            this.fullMembersLoaded.set(true);
            this.pendingReqs.delete('members');
          },
          error: () => this.pendingReqs.delete('members')
        });
      }
    } else if (tab === 'billing' && !this.fullInvoicesLoaded() && !this.pendingReqs.has('invoices')) {
      this.pendingReqs.add('invoices');
      this.customerService.getMyInvoices().subscribe({
        next: res => {
          this.invoices.set(this.extractArray(res));
          this.fullInvoicesLoaded.set(true);
          this.pendingReqs.delete('invoices');
        },
        error: () => this.pendingReqs.delete('invoices')
      });
    } else if (tab === 'revisions') {
      if (!this.fullEndorsementsLoaded() && !this.pendingReqs.has('endorsements')) {
        this.pendingReqs.add('endorsements');
        this.customerService.getMyEndorsements().subscribe({
          next: res => {
            this.endorsements.set(this.extractArray(res));
            this.fullEndorsementsLoaded.set(true);
            this.pendingReqs.delete('endorsements');
          },
          error: () => this.pendingReqs.delete('endorsements')
        });
      }
      if (!this.fullMembersLoaded() && !this.pendingReqs.has('members')) {
        this.pendingReqs.add('members');
        this.customerService.getMyMembers().subscribe({
          next: res => {
            const rawMembers = this.extractArray(res);
            this.members.set(rawMembers.map(m => ({
              ...m,
              dependents: this.extractArray(m.dependents)
            })));
            this.fullMembersLoaded.set(true);
            this.pendingReqs.delete('members');
          },
          error: () => this.pendingReqs.delete('members')
        });
      }
    }
  }

  refreshSummary() {
    this.customerService.getSummary().subscribe({
      next: res => this.summary.set(res?.data || res),
      error: () => { }
    });
  }

  // Operation Details Drilldowns
  viewPolicyDetails(p: any) {
    this.isLoading.set(true);
    this.selectedPolicy.set(p);
    forkJoin({
      invoices: this.customerService.getInvoicesByPolicy(p.id),
      claims: this.customerService.getClaimsByPolicy(p.id),
      endorsements: this.customerService.getEndorsementsByPolicy(p.id)
    }).subscribe(res => {
      this.policyInvoices.set(this.extractArray(res.invoices));
      this.policyClaims.set(this.extractArray(res.claims));
      this.policyEndorsements.set(this.extractArray(res.endorsements));
      this.endorsementForm.patchValue({ policyAssignmentId: p.id });
      this.isLoading.set(false);
    });
  }

  toggleGlobalEndorsementForm() {
    this.endorsementForm.reset({ type: '0', policyAssignmentId: '' });
    this.selectedEndorsementMember.set(null);
    this.showEndorsementForm.set(!this.showEndorsementForm());

    // Safety net: if members still not loaded (e.g. direct URL access), fetch now
    if (this.showEndorsementForm() && this.members().length === 0) {
      this.customerService.getMyMembers().subscribe({
        next: res => {
          const rawMembers = this.extractArray(res);
          this.members.set(rawMembers.map(m => ({
            ...m,
            dependents: this.extractArray(m.dependents)
          })));
        },
        error: () => { }
      });
    }
  }

  viewInvoiceDetailDrilldown(id: string) {
    this.isLoading.set(true);
    this.customerService.getInvoiceDetail(id).subscribe(res => {
      this.selectedInvoice.set(res?.data || res);
      this.customerService.getPayments(id).subscribe(p => {
        this.invoicePayments.set(this.extractArray(p));
        this.isLoading.set(false);
      });
    });
  }

  viewInvoiceDetail(id: string) {
    this.selectedPolicy.set(null);
    this.viewInvoiceDetailDrilldown(id);
  }

  submitEndorsement(policyId?: string) {
    const finalId = policyId || this.endorsementForm.get('policyAssignmentId')?.value;
    if (!finalId || this.endorsementForm.invalid) {
      if (this.endorsementForm.invalid) this.toastService.warning("Validation context: Revision data required.");
      return;
    }

    // Guard: For Remove Member & Add Dependent, a valid employee ID must be resolved to a member
    const endType = parseInt(this.endorsementForm.get('type')?.value || EndorsementType.ChangeCoverage.toString(), 10);
    if ((endType === EndorsementType.RemoveMember || endType === EndorsementType.AddDependent) && !this.endorsementForm.get('memberId')?.value) {
      this.toastService.error("Employee ID not found. Please enter a valid, existing Employee ID to identify the member.");
      return;
    }

    // Guard: Revisions only allowed for active policies
    const policy = this.policies().find(p => p.id === finalId);
    if (policy && policy.status !== 'Active') {
      this.toastService.error(`Revisions are restricted for ${policy.status} policies.`);
      return;
    }

    this.isLoading.set(true);

    const val = this.endorsementForm.value;
    let endData: any = {};

    if (endType === EndorsementType.AddMember) { // AddMember
      endData = {
        FirstName: val.firstName,
        LastName: val.lastName,
        FullName: `${val.firstName} ${val.lastName}`.trim(),
        EmployeeCode: val.employeeCode,
        Email: val.email,
        PhoneNo: val.phoneNo,
        DateOfBirth: val.dateOfBirth,
        Gender: parseInt(val.gender || Gender.Male.toString(), 10)
      };
    } else if (endType === EndorsementType.RemoveMember) { // RemoveMember
      endData = { MemberId: val.memberId };
    } else if (endType === EndorsementType.AddDependent) { // AddDependent
      endData = {
        MemberId: val.memberId,
        FullName: val.fullName,
        Relationship: parseInt(val.relationship || RelationshipType.Spouse.toString(), 10),
        DateOfBirth: val.dateOfBirth,
        Gender: parseInt(val.gender || Gender.Male.toString(), 10)
      };
    } else { // Other
      endData = { Instruction: val.description };
    }

    const payload = {
      PolicyAssignmentId: finalId,
      Type: endType,
      Description: val.description,
      EndorsementData: endData
    };

    this.customerService.submitEndorsement(payload).subscribe({
      next: () => {
        this.toastService.success("Revision request dispatched successfully.");
        this.showEndorsementForm.set(false);
        this.endorsementForm.reset({ type: '0', policyAssignmentId: '' });
        this.selectedEndorsementMember.set(null);

        // Refresh local policy view if any
        this.customerService.getEndorsementsByPolicy(finalId).subscribe(res => {
          this.policyEndorsements.set(this.extractArray(res));
        });

        // Refresh GLOBAL revision ledger
        this.customerService.getMyEndorsements().subscribe(res => {
          this.endorsements.set(this.extractArray(res));
          this.isLoading.set(false);
        });
      },
      error: (err) => {
        this.isLoading.set(false);
        const errorMsg = err.error?.message || err.error || "Validation block: Error dispatching endorsement.";
        this.toastService.error("Endorsement Failed: " + errorMsg);
        console.error("Endorsement Submission Error:", err);
      }
    });
  }

  completeProfile() {
    if (this.profileForm.invalid) return;
    this.isLoading.set(true);
    this.customerService.completeProfile(this.profileForm.value).subscribe({
      next: () => {
        this.customerService.getProfile().subscribe(res => {
          this.handleProfileData(res?.data || res);
          this.isLoading.set(false);
          this.toastService.success("Profile committed.");
          // Also refresh current tab since status changed
          this.updateTab(this.router.url);
        });
      },
      error: (err) => {
        this.isLoading.set(false);
        this.toastService.error("Profile Failed: " + (err.error?.message || "Operation failed."));
      }
    });
  }

  onDocSelect(event: any) { this.selectedProfileDoc = event.target.files[0]; }

  uploadDoc() {
    if (!this.selectedProfileDoc) return;
    this.isLoading.set(true);
    const fd = new FormData();
    fd.append('documentType', DocumentType.Other.toString()); // Other
    fd.append('file', this.selectedProfileDoc);
    this.customerService.uploadDocument(fd).subscribe({
      next: () => {
        this.customerService.getProfile().subscribe(res => {
          this.handleProfileData(res?.data || res);
          this.isLoading.set(false);
          this.toastService.info("Artifact uploaded.");
          this.selectedProfileDoc = null;
        });
      },
      error: (err) => {
        this.isLoading.set(false);
        const errorMessage = err.error?.message || "Operation failed.";
        this.toastService.error("Upload Failed: " + errorMessage);

        // If blocked, the backend returns 400. Redirect to the dedicated blocked page.
        this.customerService.getProfile().subscribe(res => {
          const profile = res?.data || res;
          if (profile?.isBlocked) {
            this.router.navigate(['/account-blocked']);
          }
        });
      }
    });
  }

  onEmployeeCodeInput(event: any) {
    const code = event.target.value?.trim().toUpperCase();
    if (!code) {
      this.claimForm.patchValue({ memberId: '', dependentId: '' });
      this.selectedClaimMember.set(null);
      this.claimForm.get('dependentId')?.disable();
      return;
    }

    // Try finding in current filtered list (Only ACTIVE members)
    let member = this.filteredMembers().find(m => m.employeeCode?.toUpperCase() === code && m.status === true);

    // If not found, search through ALL loaded members and auto-select the policy context (Only ACTIVE members)
    if (!member) {
      member = this.members().find(m => m.employeeCode?.toUpperCase() === code && m.status === true);
      if (member && member.policyAssignmentId) {
        this.claimForm.patchValue({ policyAssignmentId: member.policyAssignmentId }, { emitEvent: false });
      }
    }

    if (member) {
      this.claimForm.patchValue({ memberId: member.id, dependentId: '' });
      this.selectedClaimMember.set(member);
      this.isInvalidEmployeeCode.set(false);
      this.claimForm.get('dependentId')?.enable();
    } else {
      this.claimForm.patchValue({ memberId: '', dependentId: '' });
      this.selectedClaimMember.set(null);
      this.isInvalidEmployeeCode.set(true);
      this.claimForm.get('dependentId')?.disable();
    }
  }

  onEndorsementEmployeeCodeInput(event: any) {
    const code = event.target.value?.trim().toUpperCase();
    if (!code) {
      this.endorsementForm.patchValue({ memberId: '' });
      this.selectedEndorsementMember.set(null);
      return;
    }

    // Search through ALL loaded members
    const member = this.members().find(m => m.employeeCode?.toUpperCase() === code);

    if (member) {
      this.endorsementForm.patchValue({ memberId: member.id });
      this.selectedEndorsementMember.set(member);

      // If we're in global mode (no selectedPolicy) and haven't chosen a policy yet, auto-select it
      if (!this.selectedPolicy() && !this.endorsementForm.get('policyAssignmentId')?.value) {
        this.endorsementForm.patchValue({ policyAssignmentId: member.policyAssignmentId });
      }
    } else {
      this.endorsementForm.patchValue({ memberId: '' });
      this.selectedEndorsementMember.set(null);
    }
  }

  onClaimDocsSelect(event: any) {
    const files = event.target.files;
    for (let i = 0; i < files.length; i++) {
      this.selectedClaimFiles.push({ file: files[i], type: ClaimDocumentType.MedicalBill.toString() }); // Default to Medical Bill
    }
  }

  removeClaimFile(index: number) {
    this.selectedClaimFiles.splice(index, 1);
  }

  updateFileType(index: number, typeId: string) {
    this.selectedClaimFiles[index].type = typeId;
  }

  submitClaim() {
    if (this.claimForm.invalid) return;
    this.isLoading.set(true);
    const fd = new FormData();
    const val = this.claimForm.getRawValue() as any;

    fd.append('PolicyAssignmentId', val.policyAssignmentId);
    fd.append('MemberId', val.memberId);
    if (val.dependentId) fd.append('DependentId', val.dependentId);
    fd.append('ClaimType', val.claimType);
    fd.append('ClaimAmount', val.claimAmount.toString());
    fd.append('ClaimReason', val.description);

    if (this.selectedClaimFiles.length > 0) {
      this.selectedClaimFiles.forEach(item => {
        fd.append('Documents', item.file);
        fd.append('DocumentTypes', item.type);
      });
    }

    this.customerService.submitClaim(fd).subscribe({
      next: () => {
        this.customerService.getMyClaims().subscribe(res => {
          this.claims.set(this.extractArray(res));
          this.refreshSummary();
          this.isLoading.set(false);
          this.showClaimForm.set(false);
          this.claimForm.reset({
            policyAssignmentId: val.policyAssignmentId,
            claimType: ''
          });
          this.selectedClaimFiles = [];
          this.selectedClaimMember.set(null);
          this.toastService.success("Claim successfully dispatched for adjudication!");
        });
      },
      error: (err) => {
        this.isLoading.set(false);
        const errorMsg = err.error?.message || err.error || "Internal adjudication rejection.";

        if (typeof errorMsg === 'string' && errorMsg.includes('Premium not paid')) {
          this.router.navigate(['/customer/premium-error']);
          return;
        }

        this.toastService.error("Submission Failed: " + errorMsg);
        console.error("Claim Error:", err);
      }
    });
  }

  async downloadInvoicePDF() {
    const data = document.getElementById('invoice-content');
    if (!data) return;

    this.isLoading.set(true);

    try {
      const originalStyle = data.style.cssText;
      data.style.overflow = 'visible';
      data.style.maxHeight = 'none';
      data.style.height = 'auto';

      const dataUrl = await htmlToImage.toPng(data, {
        pixelRatio: 2,
        backgroundColor: '#ffffff',
        style: {
          margin: '0',
          padding: '40px', // Generous padding for a clean document look
          width: '800px'
        }
      });

      data.style.cssText = originalStyle;

      const pdf = new jsPDF('p', 'mm', 'a4');
      const pageWidth = pdf.internal.pageSize.getWidth();
      const pageHeight = pdf.internal.pageSize.getHeight();

      const img = new Image();
      img.src = dataUrl;

      img.onload = () => {
        // Center the invoice with a consistent margin
        const margin = 10; // 10mm margin on all sides
        const imgWidth = pageWidth - (margin * 2);
        const imgHeight = (img.naturalHeight * imgWidth) / img.naturalWidth;
        const xOffset = margin;

        let heightLeft = imgHeight;
        let position = 0;

        // First page
        pdf.addImage(dataUrl, 'PNG', xOffset, position, imgWidth, imgHeight);
        heightLeft -= pageHeight;

        while (heightLeft > 0) {
          position -= pageHeight;
          pdf.addPage();
          pdf.addImage(dataUrl, 'PNG', xOffset, position, imgWidth, imgHeight);
          heightLeft -= pageHeight;
        }

        const fileName = `EGI_Invoice_${this.selectedInvoice()?.invoiceNo || 'DOC'}.pdf`;
        pdf.save(fileName);

        this.isLoading.set(false);
        this.toastService.success("Perfectly Centered PDF Generated.");
      };
    } catch (error) {
      this.isLoading.set(false);
      this.toastService.error("PDF generation failed.");
      console.error("PDF Failure:", error);
    }
  }

  payInvoice(invoiceId: string, balance: number, totalAmount: number) {
    this.selectedInvoiceForPayment.set({ id: invoiceId, balance: balance, totalAmount: totalAmount });
    this.paymentForm.patchValue({
      amount: balance,
      paymentMethod: 1,
      transactionId: 'TXN-' + Math.random().toString(36).substring(2, 9).toUpperCase()
    });
    this.showPaymentModal.set(true);
  }

  submitPayment() {
    if (this.paymentForm.invalid || this.isLoading()) return;

    const inv = this.selectedInvoiceForPayment();
    if (!inv) return;

    this.isLoading.set(true);
    const val = this.paymentForm.value;

    const payload = {
      PaidAmount: val.amount,
      PaymentMethod: parseInt(val.paymentMethod as any),
      TransactionId: val.transactionId || 'TXN-' + Math.random().toString(36).substring(2, 9).toUpperCase()
    };

    this.customerService.payInvoice(inv.id, payload).subscribe({
      next: () => {
        this.customerService.getMyInvoices().subscribe(res => {
          this.invoices.set(this.extractArray(res));
          this.refreshSummary();
          this.isLoading.set(false);
          this.showPaymentModal.set(false);
          this.selectedInvoice.set(null);
          this.toastService.success("Payment processed successfully!", 5000);
        });
      },
      error: (err) => {
        this.isLoading.set(false);
        const errorMsg = err.error?.message || err.error || "Internal collection rejection.";
        this.toastService.error("Payment Failed: " + errorMsg);
        console.error("Payment Error:", err);
      }
    });
  }

  openPolicyOnboarding() {
    this.showPolicyForm.set(true);
    this.policyOnboardingStep.set(1);
    this.selectedPlanForOnboarding.set(null);
    this.policyForm.reset({
      insurancePlanId: '',
      billingFrequency: 1,
      startDate: new Date().toISOString().split('T')[0],
      durationInYears: 1
    });
    this.membersFile = null;
    this.dependentsFile = null;
  }

  selectPlan(plan: any) {
    this.selectedPlanForOnboarding.set(plan);
    this.policyForm.patchValue({
      insurancePlanId: plan.id
    });
  }

  deselectPlan() {
    this.selectedPlanForOnboarding.set(null);
    this.policyForm.patchValue({
      insurancePlanId: ''
    });
  }

  onMembersFileSelect(event: any) {
    if (event.target.files.length > 0) this.membersFile = event.target.files[0];
  }

  onDependentsFileSelect(event: any) {
    if (event.target.files.length > 0) this.dependentsFile = event.target.files[0];
  }

  onboardToPolicy() {
    if (this.policyForm.invalid || !this.membersFile) return;
    this.isLoading.set(true);
    const formData = new FormData();
    const val = this.policyForm.value as any;
    const pd = this.profileData() || {};
    const corporateId = pd.id || pd.Id;
    if (corporateId) formData.append('CorporateClientId', corporateId);
    formData.append('InsurancePlanId', val.insurancePlanId);
    formData.append('BillingFrequency', val.billingFrequency);
    formData.append('StartDate', val.startDate);
    formData.append('DurationInYears', val.durationInYears);
    formData.append('MembersFile', this.membersFile);
    if (this.dependentsFile) formData.append('DependentsFile', this.dependentsFile);

    this.customerService.uploadMembersExcel(formData).subscribe({
      next: (res: any) => {
        this.customerService.getMyPolicies().subscribe(p => {
          this.policies.set(this.extractArray(p));
          this.refreshSummary();
          this.isLoading.set(false);
          this.showPolicyForm.set(false);
          this.toastService.success(res.message || 'Deployed!');
        });
      },
      error: (err: any) => {
        this.isLoading.set(false);
        this.toastService.error('Error: ' + (err?.error?.message || err?.message));
      }
    });
  }
}

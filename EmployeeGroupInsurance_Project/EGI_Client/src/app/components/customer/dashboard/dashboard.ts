import { Component, inject, signal, OnInit, OnDestroy, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerService } from '../../../data-access/api.services';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, NavigationEnd } from '@angular/router';
import { Subscription, forkJoin, of } from 'rxjs';
import { filter, catchError } from 'rxjs/operators';
import { ToastService } from '../../../core/services/toast.service';
import * as htmlToImage from 'html-to-image';
import { jsPDF } from 'jspdf';

@Component({
  selector: 'app-customer-dashboard',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './dashboard.html'
})
export class CustomerDashboardComponent implements OnInit, OnDestroy {
  customerService = inject(CustomerService);
  fb = inject(FormBuilder);
  router = inject(Router);
  toastService = inject(ToastService);
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
  isProfileApproved = computed(() => this.profileData()?.status === 'Approved');

  todayStr = signal(new Date().toISOString().split('T')[0]);
  maxStartDateStr = computed(() => {
    let d = new Date();
    d.setMonth(d.getMonth() + 1);
    return d.toISOString().split('T')[0];
  });

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

  // Drilldown states
  selectedPolicy = signal<any>(null);
  policyInvoices = signal<any[]>([]);
  policyClaims = signal<any[]>([]);
  policyEndorsements = signal<any[]>([]);
  showEndorsementForm = signal(false);

  selectedInvoice = signal<any>(null);
  invoicePayments = signal<any[]>([]);

  showClaimForm = signal(false);
  showPolicyForm = signal(false);
  showPaymentModal = signal(false);
  selectedInvoiceForPayment = signal<any>(null);
  selectedPlanForOnboarding = signal<any>(null);

  selectedProfileDoc: File | null = null;

  // Track selected files along with their document types
  selectedClaimFiles: { file: File, type: string }[] = [];

  // Document Types from Backend Enum
  docTypes = [
    { id: '1', name: 'Medical Bill' },
    { id: '2', name: 'Hospital Discharge' },
    { id: '3', name: 'Prescription' },
    { id: '4', name: 'FIR (Accident)' },
    { id: '8', name: 'Diagnosis Report' },
    { id: '10', name: 'Other' }
  ];
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
    billingFrequency: [2, Validators.required],
    startDate: [new Date().toISOString().split('T')[0], Validators.required],
    durationInYears: [1, [Validators.required, Validators.min(1), Validators.max(10)]]
  });

  paymentForm = this.fb.group({
    amount: [0, [Validators.required, Validators.min(1)]],
    paymentMethod: [1, Validators.required],
    transactionId: ['']
  });

  endorsementForm = this.fb.group({
    policyAssignmentId: ['', Validators.required],
    type: ['0', Validators.required],
    description: ['', Validators.required],
    firstName: [''],
    lastName: [''],
    employeeCode: [''],
    email: [''],
    phoneNo: [''],
    dateOfBirth: [''],
    gender: ['0'],
    memberId: [''],
    fullName: [''],
    relationship: ['1']
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
    this.routerSub = this.router.events.pipe(
      filter(e => e instanceof NavigationEnd)
    ).subscribe((e: any) => this.updateTab(e.urlAfterRedirects));

    this.initialLoad();

    this.claimForm.get('policyAssignmentId')?.valueChanges.subscribe(() => {
      this.claimForm.patchValue({ memberId: '', employeeCode: '', dependentId: '' });
      this.selectedClaimMember.set(null);
      this.claimForm.get('dependentId')?.disable();
    });
  }

  ngOnDestroy() {
    this.routerSub?.unsubscribe();
  }

  // OPTIMIZATION: Track pending to prevent duplicate HTTP calls
  private pendingReqs = new Set<string>();

  // OPTIMIZATION: Initial load captures core overview state at once WITHOUT forkJoin blocking
  initialLoad() {
    this.isLoading.set(true);

    // Abstracting to an immediate lock
    setTimeout(() => this.isLoading.set(false), 800);

    this.customerService.getOverview().pipe(catchError(() => of(null))).subscribe(res => {
      if (res) {
        const ov = res?.data || res;
        this.summary.set(ov.summary);
        this.policies.set(this.extractArray(ov.recentPolicies));
        if (this.claims().length === 0) this.claims.set(this.extractArray(ov.recentClaims));
        if (this.invoices().length === 0) this.invoices.set(this.extractArray(ov.recentInvoices));
        if (this.endorsements().length <= 5) this.endorsements.set(this.extractArray(ov.recentEndorsements));

        // Process members if present in overview
        if (ov.recentMembers) {
          const rawMembers = this.extractArray(ov.recentMembers);
          this.members.set(rawMembers.map(m => ({
            ...m,
            dependents: this.extractArray(m.dependents)
          })));
        }
      }
    });

    this.customerService.getAllPlans().pipe(catchError(() => of([]))).subscribe(res => {
      this.plans.set(this.extractArray(res));
    });

    this.customerService.getProfile().pipe(catchError(() => of(null))).subscribe(res => {
      if (res) this.handleProfileData(res?.data || res);
      else this.handleProfileData({ status: 'Draft' }); // Enforce verification overlay
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

  // OPTIMIZATION: Individual execution without blocking UI mapping
  lazyLoadTab(tab: string) {
    if (tab === 'policies' && this.members().length === 0 && !this.pendingReqs.has('members')) {
      this.pendingReqs.add('members');
      this.customerService.getMyMembers().subscribe({
        next: res => {
          const rawMembers = this.extractArray(res);
          this.members.set(rawMembers.map(m => ({
            ...m,
            dependents: this.extractArray(m.dependents)
          })));
          this.pendingReqs.delete('members');
        },
        error: () => this.pendingReqs.delete('members')
      });
    } else if (tab === 'claims') {
      if (this.claims().length === 0 && !this.pendingReqs.has('claims')) {
        this.pendingReqs.add('claims');
        this.customerService.getMyClaims().subscribe({
          next: res => { this.claims.set(this.extractArray(res)); this.pendingReqs.delete('claims'); },
          error: () => this.pendingReqs.delete('claims')
        });
      }
      if (this.members().length === 0 && !this.pendingReqs.has('members')) {
        this.pendingReqs.add('members');
        this.customerService.getMyMembers().subscribe({
          next: res => {
            const rawMembers = this.extractArray(res);
            this.members.set(rawMembers.map(m => ({
              ...m,
              dependents: this.extractArray(m.dependents)
            })));
            this.pendingReqs.delete('members');
          },
          error: () => this.pendingReqs.delete('members')
        });
      }
    } else if (tab === 'billing' && this.invoices().length === 0 && !this.pendingReqs.has('invoices')) {
      this.pendingReqs.add('invoices');
      this.customerService.getMyInvoices().subscribe({
        next: res => { this.invoices.set(this.extractArray(res)); this.pendingReqs.delete('invoices'); },
        error: () => this.pendingReqs.delete('invoices')
      });
    } else if (tab === 'revisions') {
      if ((this.endorsements().length === 0 || this.endorsements().length <= 5) && !this.pendingReqs.has('endorsements')) {
        this.pendingReqs.add('endorsements');
        this.customerService.getMyEndorsements().subscribe({
          next: res => { this.endorsements.set(this.extractArray(res)); this.pendingReqs.delete('endorsements'); },
          error: () => this.pendingReqs.delete('endorsements')
        });
      }
      if (this.members().length === 0 && !this.pendingReqs.has('members')) {
        this.pendingReqs.add('members');
        this.customerService.getMyMembers().subscribe({
          next: res => { this.members.set(this.extractArray(res)); this.pendingReqs.delete('members'); },
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
    const endType = parseInt(this.endorsementForm.get('type')?.value || '4', 10);
    if ((endType === 1 || endType === 2) && !this.endorsementForm.get('memberId')?.value) {
      this.toastService.error("Employee ID not found. Please enter a valid, existing Employee ID to identify the member.");
      return;
    }

    this.isLoading.set(true);

    const val = this.endorsementForm.value;
    let endData: any = {};

    if (endType === 0) { // AddMember
      endData = {
        FirstName: val.firstName,
        LastName: val.lastName,
        FullName: `${val.firstName} ${val.lastName}`.trim(),
        EmployeeCode: val.employeeCode,
        Email: val.email,
        PhoneNo: val.phoneNo,
        DateOfBirth: val.dateOfBirth,
        Gender: parseInt(val.gender || '0', 10)
      };
    } else if (endType === 1) { // RemoveMember
      endData = { MemberId: val.memberId };
    } else if (endType === 2) { // AddDependent
      endData = {
        MemberId: val.memberId,
        FullName: val.fullName,
        Relationship: parseInt(val.relationship || '1', 10),
        DateOfBirth: val.dateOfBirth,
        Gender: parseInt(val.gender || '0', 10)
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
    fd.append('documentType', '5'); // Other = 5
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
      this.selectedClaimFiles.push({ file: files[i], type: '1' }); // Default to Medical Bill
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
    const val = this.claimForm.value as any;

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

  payInvoice(invoiceId: string, balance: number) {
    this.selectedInvoiceForPayment.set({ id: invoiceId, balance: balance });
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
    this.selectedPlanForOnboarding.set(null);
    this.policyForm.reset({
      insurancePlanId: '',
      billingFrequency: 2,
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
    formData.append('CorporateClientId', this.profileData()?.id);
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

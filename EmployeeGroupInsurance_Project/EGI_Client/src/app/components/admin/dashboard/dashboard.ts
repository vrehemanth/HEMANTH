import { Component, inject, signal, OnInit, OnDestroy, computed, viewChild, ElementRef, effect } from '@angular/core';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';
import { AdminService } from '../../../data-access/admin.service';
import { FormsModule } from '@angular/forms';
import { Router, NavigationEnd } from '@angular/router';
import { filter, Subscription, forkJoin, of, catchError } from 'rxjs';
import { ToastService } from '../../../core/services/toast.service';
import { AuthService } from '../../../core/services/auth.service';
import { Chart, registerables } from 'chart.js';
import { OverviewTabComponent } from './tabs/overview/overview';
import { PlansTabComponent } from './tabs/plans/plans';
import { ClientsTabComponent } from './tabs/clients/clients';
import { StaffTabComponent } from './tabs/staff/staff';
import { PoliciesTabComponent } from './tabs/policies/policies';
import { ClaimsTabComponent } from './tabs/claims/claims';
import { LogsTabComponent } from './tabs/logs/logs';

import { ApprovalsTabComponent } from './tabs/approvals/approvals';
import { HospitalsTabComponent } from './tabs/hospitals/hospitals';

Chart.register(...registerables);

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, OverviewTabComponent, PlansTabComponent, ClientsTabComponent, StaffTabComponent, PoliciesTabComponent, ClaimsTabComponent, LogsTabComponent, ApprovalsTabComponent, HospitalsTabComponent],
  templateUrl: './dashboard.html'
})
export class AdminDashboardComponent implements OnInit, OnDestroy {
  private adminService = inject(AdminService);
  private router = inject(Router);
  private toastService = inject(ToastService);
  authService = inject(AuthService);

  activeTab = signal<'dashboard' | 'plans' | 'clients' | 'staff' | 'policies' | 'claims' | 'logs' | 'approvals' | 'hospitals'>('dashboard');
  
  hospitals = signal<any[]>([]);
  hospitalSearchTerm = signal('');
  hospitalSearchQuery = signal('');
  hospitalSuggestions = signal<any[]>([]);
  hospitalCenter = signal<{lat: number, lng: number}>({lat: 20.5937, lng: 78.9629});
  userLocation = signal<{lat: number, lng: number} | null>(null);

  hospitalSortConfig = signal<{ column: string, direction: 'asc' | 'desc' }>({ column: 'name', direction: 'asc' });

  filteredHospitals = computed(() => {
    let result = this.hospitals();
    const search = this.hospitalSearchTerm().toLowerCase().trim();
    if (search) {
      result = result.filter(h => 
        (h.name && h.name.toLowerCase().includes(search)) ||
        (h.city && h.city.toLowerCase().includes(search)) ||
        (h.specialties && h.specialties.toLowerCase().includes(search))
      );
    }
    const sort = this.hospitalSortConfig();
    return [...result].sort((a, b) => {
      let comparison = 0;
      switch (sort.column) {
        case 'name': comparison = (a.name || '').localeCompare(b.name || ''); break;
        case 'city': comparison = (a.city || '').localeCompare(b.city || ''); break;
        case 'network': comparison = Number(b.isNetworkHospital) - Number(a.isNetworkHospital); break;
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  sortHospitals(column: string) { this.updateSort(this.hospitalSortConfig, column); }

  showHospitalForm = signal(false);
  isEditingHospital = signal(false);
  editingHospitalId: string | null = null;
  newHospital = {
    name: '', address: '', city: '', state: '', zipCode: '',
    phone: '', email: '', latitude: 20.5937, longitude: 78.9629,
    specialties: '', isNetworkHospital: true, isActive: true
  };
  routerSub: Subscription | undefined;
  isLoading = signal(false);

  // --- Chart Instances ---
  private financialChart?: Chart;
  private claimTrendChart?: Chart;
  private planMixChart?: Chart;

  summary = signal<any>(null);
  pendingClients = signal<any[]>([]);
  allClients = signal<any[]>([]);
  allPolicyAssignments = signal<any[]>([]);
  allClaimsRegistry = signal<any[]>([]);
  auditLogs = signal<any[]>([]);
  pendingHealthCheckups = signal<any[]>([]);

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
      }
      return sort.direction === 'asc' ? comparison : -comparison;
    });
  });

  ngOnInit() {
    // Current user is already loaded by AuthService constructor
    this.getUserLocation();
    
    // Initial tab setup
    this.updateTab(this.router.url);

    this.routerSub = this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((e: any) => {
      this.updateTab(e.urlAfterRedirects || this.router.url);
      this.loadDashboardData();
    });
    
    this.loadDashboardData();
  }

  ngOnDestroy() {
    this.routerSub?.unsubscribe();
  }

  getUserLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (pos) => {
          setTimeout(() => {
            const loc = { lat: pos.coords.latitude, lng: pos.coords.longitude };
            this.userLocation.set(loc);
            if (!this.isEditingHospital()) {
              this.hospitalCenter.set(loc);
              this.newHospital.latitude = Number(loc.lat.toFixed(6));
              this.newHospital.longitude = Number(loc.lng.toFixed(6));
            }
          }, 0);
        },
        (err) => console.warn("Location access denied", err)
      );
    }
  }

  calculateDistance(lat1: number, lon1: number, lat2: number, lon2: number): string {
    const R = 6371;
    const dLat = (lat2 - lat1) * Math.PI / 180;
    const dLon = (lon2 - lon1) * Math.PI / 180;
    const a = Math.sin(dLat/2) * Math.sin(dLat/2) +
              Math.cos(lat1 * Math.PI / 180) * Math.cos(lat2 * Math.PI / 180) * 
              Math.sin(dLon/2) * Math.sin(dLon/2);
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
    const d = R * c;
    return d > 1 ? `${d.toFixed(1)} km` : `${(d * 1000).toFixed(0)} m`;
  }

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
        case 'salaryLPA': comparison = (a.salaryLPA || 0) - (b.salaryLPA || 0); break;
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
  selectedClientForAi = signal<any>(null);

  cleanText(text: string | null | undefined): string {
    if (!text) return '';
    return text.replace(/\*/g, '').replace(/#/g, '').trim();
  }

  getDocumentUrl(docId: string): string {
    const token = this.authService.currentUser()?.token;
    if (!token) return '';
    return `https://localhost:7146/api/Public/documents/${docId}?access_token=${token}`;
  }

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
  newPlan = { planCode: '', planName: '', description: '', basePremium: 0, status: true, hasHealthCheckup: false };

  isStaffLoading = signal(false);
  showNewStaffForm = signal(false);
  newStaff = { name: '', email: '', role: 'Agent' as any, salaryLPA: 0 };


  approvalIndustry: { [key: string]: number } = {};

  getIndustryName(id: any): string {
    const numericId = parseInt(id, 10);
    return this.industryTypesList.find(i => i.id === numericId)?.name || 'Others';
  }

  industryTypesList = [
    { id: 0, name: 'IT' },
    { id: 1, name: 'Banking' },
    { id: 2, name: 'Education' },
    { id: 3, name: 'Others' },
    { id: 4, name: 'Retail' },
    { id: 5, name: 'Healthcare' },
    { id: 6, name: 'Hospitality' },
    { id: 7, name: 'Logistics' },
    { id: 8, name: 'Manufacturing' },
    { id: 9, name: 'Construction' },
    { id: 10, name: 'Oil & Gas' },
    { id: 11, name: 'Mining' }
  ];

  showRejectionModal = signal(false);
  rejectionForm = { clientId: '', reason: '' };


  updateTab(url: string) {
    let tab: 'dashboard' | 'plans' | 'clients' | 'staff' | 'policies' | 'claims' | 'logs' | 'approvals' | 'hospitals' = 'dashboard';
    if (url.includes('plans')) tab = 'plans';
    else if (url.includes('clients')) tab = 'clients';
    else if (url.includes('staff')) tab = 'staff';
    else if (url.includes('policies')) tab = 'policies';
    else if (url.includes('claims')) tab = 'claims';
    else if (url.includes('logs')) tab = 'logs';
    else if (url.includes('approvals')) tab = 'approvals';
    else if (url.includes('hospitals')) tab = 'hospitals';

    this.activeTab.set(tab);
    this.lazyLoadTab(tab);
  }

  loadHospitalsData() {
    if (this.pendingReqs.has('hospitals')) return;
    this.pendingReqs.add('hospitals');
    this.adminService.getAllHospitals().subscribe({
      next: (res: any) => {
        this.hospitals.set(this.extractArray(res));
        this.pendingReqs.delete('hospitals');
      },
      error: () => this.pendingReqs.delete('hospitals')
    });
  }

  toggleHospitalForm() {
    this.showHospitalForm.update(v => !v);
    if (!this.showHospitalForm()) {
      this.isEditingHospital.set(false);
      this.editingHospitalId = null;
      this.resetHospitalForm();
    }
  }

  resetHospitalForm() {
    this.newHospital = {
      name: '', address: '', city: '', state: '', zipCode: '',
      phone: '', email: '', latitude: 20.5937, longitude: 78.9629, specialties: '',
      isNetworkHospital: true, isActive: true
    };
    this.hospitalSearchQuery.set('');
    this.hospitalCenter.set({lat: 20.5937, lng: 78.9629});
  }

  editHospital(h: any) {
    this.isEditingHospital.set(true);
    this.editingHospitalId = h.id;
    this.newHospital = { ...h };
    this.hospitalCenter.set({lat: h.latitude, lng: h.longitude});
    this.showHospitalForm.set(true);
  }

  saveHospital() {
    if (!this.newHospital.name || !this.newHospital.city) {
      this.toastService.warning("Name and City are basically required.");
      return;
    }

    this.isLoading.set(true);
    const obs = this.isEditingHospital() 
      ? this.adminService.updateHospital(this.editingHospitalId!, this.newHospital)
      : this.adminService.createHospital(this.newHospital);

    obs.subscribe({
      next: () => {
        this.toastService.success(`Hospital ${this.isEditingHospital() ? 'updated' : 'registered'} successfully.`);
        this.toggleHospitalForm();
        this.loadHospitalsData();
        this.isLoading.set(false);
      },
      error: (err) => {
        this.toastService.error(err.error?.message || "Failed to save hospital.");
        this.isLoading.set(false);
      }
    });
  }

  selectHospitalSuggestion(s: any) {
    const lat = parseFloat(s.lat);
    const lng = parseFloat(s.lon);
    this.newHospital.latitude = Number(lat.toFixed(6));
    this.newHospital.longitude = Number(lng.toFixed(6));
    this.hospitalCenter.set({lat, lng});
    this.hospitalSearchQuery.set(s.display_name);
    this.hospitalSuggestions.set([]);
    this.toastService.success(`Location set: ${s.display_name.substring(0, 30)}...`);
  }

  private autocompleteTimeout: any;
  handleOsmAutocomplete(lat?: number, lng?: number) {
    const query = this.hospitalSearchQuery();
    if (query.trim().length < 3) {
      this.hospitalSuggestions.set([]);
      return;
    }

    let url = `https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(query)}&limit=10&addressdetails=1`;
    if (lat && lng) {
      const viewbox = `${lng - 0.5},${lat + 0.5},${lng + 0.5},${lat - 0.5}`;
      url += `&lat=${lat}&lon=${lng}&viewbox=${viewbox}&bounded=0`;
    }

    clearTimeout(this.autocompleteTimeout);
    this.autocompleteTimeout = setTimeout(() => {
      fetch(url, { headers: { 'Accept-Language': 'en-US,en;q=0.5' } })
        .then(res => res.json())
        .then(data => {
          let suggestions = data || [];
          const userLoc = this.userLocation() || this.hospitalCenter();
          if (suggestions.length > 0) {
            suggestions = suggestions.sort((a: any, b: any) => {
              const latA = parseFloat(a.lat);
              const lonA = parseFloat(a.lon);
              const latB = parseFloat(b.lat);
              const lonB = parseFloat(b.lon);
              
              const R = 6371;
              const dLatA = (latA - userLoc.lat) * Math.PI / 180;
              const dLonA = (lonA - userLoc.lng) * Math.PI / 180;
              const distA = R * 2 * Math.atan2(Math.sqrt(Math.sin(dLatA/2)**2 + Math.cos(userLoc.lat*Math.PI/180)*Math.cos(latA*Math.PI/180)*Math.sin(dLonA/2)**2), Math.sqrt(1-Math.sin(dLatA/2)**2 - Math.cos(userLoc.lat*Math.PI/180)*Math.cos(latA*Math.PI/180)*Math.sin(dLonA/2)**2));

              const dLatB = (latB - userLoc.lat) * Math.PI / 180;
              const dLonB = (lonB - userLoc.lng) * Math.PI / 180;
              const distB = R * 2 * Math.atan2(Math.sqrt(Math.sin(dLatB/2)**2 + Math.cos(userLoc.lat*Math.PI/180)*Math.cos(latB*Math.PI/180)*Math.sin(dLonB/2)**2), Math.sqrt(1-Math.sin(dLatB/2)**2 - Math.cos(userLoc.lat*Math.PI/180)*Math.cos(latB*Math.PI/180)*Math.sin(dLonB/2)**2));
              
              return distA - distB;
            });
          }
          this.hospitalSuggestions.set(suggestions);
        })
        .catch(err => console.error("Autocomplete ERR", err));
    }, 500); // 500ms debounce
  }

  handleOsmSearch() {
    const query = this.hospitalSearchQuery();
    if (!query) return;

    const lat = this.hospitalCenter().lat;
    const lng = this.hospitalCenter().lng;
    const viewbox = `${lng - 0.5},${lat + 0.5},${lng + 0.5},${lat - 0.5}`;
    
    const url = `https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(query)}&limit=1&lat=${lat}&lon=${lng}&viewbox=${viewbox}&bounded=0`;

    fetch(url, { headers: { 'Accept-Language': 'en-US,en;q=0.5' } })
      .then(res => res.json())
      .then(data => {
        if (data && data.length > 0) {
          const first = data[0];
          this.selectHospitalSuggestion(first);
        } else {
          this.toastService.warning("Location not found on OpenStreetMap.");
        }
      })
      .catch(err => {
        console.error("Nomination Search ERR", err);
        this.toastService.error("Geocoding service unavailable.");
      });
  }

  deleteHospital(id: string) {
    Swal.fire({
      title: 'Are you sure?',
      text: "This will permanently remove the hospital from the system.",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.adminService.deleteHospital(id).subscribe(() => {
          this.toastService.success("Hospital deleted.");
          this.loadHospitalsData();
        });
      }
    });
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
    } else if (tab === 'approvals') {
      if (this.allClaimsRegistry().length === 0) this.loadClaimsRegistry();
    } else if (tab === 'hospitals') {
      if (this.hospitals().length === 0) this.loadHospitalsData();
    }
  }

  loadDashboardData() {
    if (this.pendingReqs.has('dashboard')) return;
    this.pendingReqs.add('dashboard');

    // Parallel fetch for overview data
    this.adminService.getSummary().subscribe({
      next: (res: any) => {
        this.summary.set(res?.data || res);
        this.pendingReqs.delete('dashboard');
      },
      error: () => this.pendingReqs.delete('dashboard')
    });

    // These can run in parallel without blocking summary
    this.loadClaimsRegistry();
    this.loadPoliciesData();
    this.loadPendingHealthCheckups();
  }

  destroyCharts() {
    this.financialChart?.destroy();
    this.claimTrendChart?.destroy();
    this.planMixChart?.destroy();
    this.financialChart = undefined;
    this.claimTrendChart = undefined;
    this.planMixChart = undefined;
  }

  initCharts(fEl: HTMLCanvasElement, cEl: HTMLCanvasElement, pEl: HTMLCanvasElement) {
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
    const salaries = s?.totalSalaryPayouts || 0;
    const profit = s?.netProfit || 0;

    this.financialChart = new Chart(ctx, {
      type: 'doughnut',
      data: {
        labels: ['Net Profit', 'Claim Payouts', 'Agent Commissions', 'Staff Salaries'],
        datasets: [{
          data: [profit, payouts, commissions, salaries],
          backgroundColor: ['#10b981', '#f59e0b', '#6366f1', '#ec4899'],
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
              label: (i: any) => ` ${i.label}: ₹${i.raw.toLocaleString('en-IN')}`
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
        pendingData.forEach((r: any) => { 
          if (this.approvalIndustry[r.id] === undefined) this.approvalIndustry[r.id] = r.industryType !== undefined ? r.industryType : 0;
        });
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


  loadPendingHealthCheckups() {
    this.adminService.getPendingHealthCheckups().subscribe(res => {
      this.pendingHealthCheckups.set(res);
    });
  }

  async verifyHealthCheckupActuals(client: any) {
    const { value: formValues } = await Swal.fire({
      title: 'Verify Participation Counts',
      html: `
        <div class="p-4 bg-gray-50 rounded-2xl text-left">
          <p class="text-[10px] font-black uppercase text-gray-400 mb-4 tracking-widest text-center">Syncing Hospital Feedback with Ledger</p>
          <div class="mb-4">
            <label class="block text-xs font-bold mb-1 uppercase">Actual Employees Treated</label>
            <input id="swal-input1" class="w-full p-3 border-2 border-gray-100 rounded-xl" type="number" value="${client.healthCheckupActualMemberCount || 0}">
          </div>
          <div>
            <label class="block text-xs font-bold mb-1 uppercase">Actual Dependents Treated</label>
            <input id="swal-input2" class="w-full p-3 border-2 border-gray-100 rounded-xl" type="number" value="${client.healthCheckupActualDependentCount || 0}">
          </div>
          <p class="text-[9px] text-gray-500 mt-4 leading-tight italic">By clicking synchronize, you confirm these counts match the formal attendance record sent by <b>${client.healthCheckupHospitalName || 'the network hospital'}</b>.</p>
        </div>
      `,
      focusConfirm: false,
      confirmButtonText: 'Synchronize Data',
      confirmButtonColor: '#10b981',
      showCancelButton: true,
      preConfirm: () => {
        return [
          (document.getElementById('swal-input1') as HTMLInputElement).value,
          (document.getElementById('swal-input2') as HTMLInputElement).value
        ];
      }
    });

    if (formValues) {
      const [mem, dep] = formValues;
      this.adminService.updateHealthCheckupActuals(client.id, { 
        memberCount: parseInt(mem), 
        dependentCount: parseInt(dep) 
      }).subscribe({
        next: () => {
          this.toastService.success("Attendance ledger updated successfully.");
          this.loadPendingHealthCheckups();
          // Assuming loadClientsAsync() is a method that refreshes client data,
          // but it's not present in the provided context.
          // If it exists, it should be called. Otherwise, this line might need adjustment.
          // this.loadClientsAsync(); 
        },
        error: () => this.toastService.error("Failed to update ledge.")
      });
    }
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
      industryType: this.approvalIndustry[id] !== undefined ? this.approvalIndustry[id] : 0
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
      rejectionReason: this.rejectionForm.reason
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

  togglePolicyStatus(id: string) {
    this.adminService.togglePolicyStatus(id).subscribe(() => {
      this.toastService.success('Policy status updated.');
      this.loadPoliciesData();
    });
  }

  toggleNewStaffForm() {
    this.showNewStaffForm.update(v => !v);
    this.newStaff = { name: '', email: '', role: 'Agent', salaryLPA: 0 };
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
      this.newPlan = { planCode: '', planName: '', description: '', basePremium: 0, status: true, hasHealthCheckup: false };
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
      status: plan.status,
      hasHealthCheckup: plan.hasHealthCheckup || false
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
      hasHealthCheckup: this.newPlan.hasHealthCheckup,
      coverages: [] // Keeping simplest update for now as per DTO
    };

    this.adminService.updatePlan(this.editingPlanId, updateDto).subscribe(() => {
      this.toastService.success('Package Updated Successfully.');
      this.toggleNewPlanForm();
      this.loadPlansData();
    });
  }

  async deactivatePlan(id: string) {
    const result = await Swal.fire({
      title: 'Deactivate Plan?',
      text: "It will be marked Inactive and hidden from new assignments, but existing policies are preserved.",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#f59e0b',
      cancelButtonColor: '#64748b',
      confirmButtonText: 'Yes, Deactivate'
    });

    if (result.isConfirmed) {
      this.adminService.deactivatePlan(id).subscribe(() => {
        this.toastService.success('Plan deactivated successfully.');
        this.loadPlansData();
      });
    }
  }

  async hardDeletePlan(id: string) {
    const result = await Swal.fire({
      title: '⚠️ PERMANENT DELETE',
      text: "This will permanently remove the plan from the database. This action CANNOT be undone. Are you absolutely sure?",
      icon: 'error',
      showCancelButton: true,
      confirmButtonColor: '#ef4444',
      cancelButtonColor: '#64748b',
      confirmButtonText: 'Yes, DELETE Permanently'
    });

    if (result.isConfirmed) {
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

  async approveAdminClaim(claimId: string, isApproved: boolean) {
    const claim = this.selectedClaimDetail();
    if (!claim) return;

    let reason = '';
    if (!isApproved) {
      const { value: text } = await Swal.fire({
        title: 'Rejection Reason',
        input: 'textarea',
        inputLabel: 'Please provide a rejection reason:',
        inputPlaceholder: 'Enter reason here...',
        showCancelButton: true,
        confirmButtonColor: '#ef4444',
        cancelButtonColor: '#64748b',
        confirmButtonText: 'Confirm Rejection'
      });

      if (text === undefined) return;
      reason = text || "";

      if (!reason.trim()) {
        this.toastService.warning("Rejection reason is mandatory for high-value claims.");
        return;
      }
    }

    const dto = {
      isApproved: isApproved,
      rejectionReason: reason
    };

    this.adminService.reviewClaim(claimId, dto).subscribe({
      next: () => {
        this.toastService.success(isApproved ? 'Claim approved and disbursed successfully.' : 'High-value claim rejected.');
        this.selectedClaimDetail.set(null);
        this.loadClaimsRegistry();
        this.loadDashboardData();
      },
      error: (err) => {
        this.toastService.error(err.error?.message || "Action failed.");
      }
    });
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

import { Component, inject, signal, OnInit, OnDestroy } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { AuthService } from '../../core/services/auth.service';
import { ThemeService } from '../../core/services/theme.service';
import { ToastService } from '../../core/services/toast.service';
import { NotificationService } from '../../core/services/notification.service';
import { CustomerService } from '../../data-access/customer.service';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './navbar.html'
})
export class AppLayoutComponent implements OnInit, OnDestroy {
  authService = inject(AuthService);
  themeService = inject(ThemeService);
  toastService = inject(ToastService);
  notificationService = inject(NotificationService);
  private customerService = inject(CustomerService, { optional: true });
  private sanitizer = inject(DomSanitizer);
  private router = inject(Router);

  showNotifications = signal(false);
  isSidebarOpen = signal(false);
  isProfileApproved = signal<boolean>(true);

  ngOnInit() {
    this.notificationService.startPolling();
    this.checkProfileStatus();

    // FORCE PASSWORD CHANGE: Immediate redirect if required
    if (this.authService.currentUser()?.requirePasswordChange) {
      this.toastService.warning('Security Alert: You must update your temporary password before accessing dashboard features.');
      this.router.navigateByUrl('/settings/change-password');
    }
  }

  private checkProfileStatus() {
    if (this.userRole.toLowerCase() === 'customer' && this.customerService) {
      this.customerService.getProfile().subscribe({
        next: (res: any) => {
          const status = res.data?.status || res.status;
          this.isProfileApproved.set(status === 'Approved');
        },
        error: () => this.isProfileApproved.set(false)
      });
    }
  }

  toggleNotifications() {
    const nextState = !this.showNotifications();
    this.showNotifications.set(nextState);
    if (nextState) {
      this.notificationService.fetchNotifications();
    }
  }

  ngOnDestroy() {
    this.notificationService.stopPolling();
  }

  get userRole() {
    return this.authService.currentUser()?.role || 'Guest';
  }

  get userName() {
    const user = this.authService.currentUser();
    return user?.name && user.name.trim() !== '' ? user.name : user?.email?.split('@')[0] || 'User';
  }

  get userInitials() {
    const user = this.authService.currentUser();
    if (user?.name && user.name.trim() !== '') {
      return user.name.substring(0, 2).toUpperCase();
    }
    const email = user?.email;
    return email ? email.substring(0, 2).toUpperCase() : 'U';
  }

  logout() {
    this.authService.logout();
  }

  getNavigationLinks(): Array<{ path: string, label: string, icon: SafeHtml }> {
    const role = this.authService.currentUser()?.role?.toLowerCase();

    // SVG icons safely bypassed
    const iconDashboard = this.sanitizer.bypassSecurityTrustHtml('<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect width="7" height="9" x="3" y="3" rx="1"/><rect width="7" height="5" x="14" y="3" rx="1"/><rect width="7" height="9" x="14" y="12" rx="1"/><rect width="7" height="5" x="3" y="16" rx="1"/></svg>');
    const iconShield = this.sanitizer.bypassSecurityTrustHtml('<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M20 13c0 5-3.5 7.5-7.66 8.95a1 1 0 0 1-.67-.01C7.5 20.5 4 18 4 13V6a1 1 0 0 1 1-1c2-1 4-2 7-2 2.5 0 4.5 1 7 2a1 1 0 0 1 1 1z"/></svg>');
    const iconBuilding = this.sanitizer.bypassSecurityTrustHtml('<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect width="16" height="20" x="4" y="2" rx="2" ry="2"/><path d="M9 22v-4h6v4"/><path d="M8 6h.01"/><path d="M16 6h.01"/><path d="M12 6h.01"/><path d="M12 10h.01"/><path d="M12 14h.01"/><path d="M16 10h.01"/><path d="M16 14h.01"/><path d="M8 10h.01"/><path d="M8 14h.01"/></svg>');
    const iconUsers = this.sanitizer.bypassSecurityTrustHtml('<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2"/><circle cx="9" cy="7" r="4"/><path d="M22 21v-2a4 4 0 0 0-3-3.87"/><path d="M16 3.13a4 4 0 0 1 0 7.75"/></svg>');
    const iconFileText = this.sanitizer.bypassSecurityTrustHtml('<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M14.5 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7.5L14.5 2z"/><polyline points="14 2 14 8 20 8"/><line x1="16" x2="8" y1="13" y2="13"/><line x1="16" x2="8" y1="17" y2="17"/><line x1="10" x2="8" y1="9" y2="9"/></svg>');
    const iconEdit3 = this.sanitizer.bypassSecurityTrustHtml('<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M12 20h9"/><path d="M16.5 3.5a2.121 2.121 0 0 1 3 3L7 19l-4 1 1-4L16.5 3.5z"/></svg>');
    const iconUser = this.sanitizer.bypassSecurityTrustHtml('<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M19 21v-2a4 4 0 0 0-4-4H9a4 4 0 0 0-4 4v2"/><circle cx="12" cy="7" r="4"/></svg>');
    const iconCreditCard = this.sanitizer.bypassSecurityTrustHtml('<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect width="20" height="14" x="2" y="5" rx="2"/><line x1="2" x2="22" y1="10" y2="10"/></svg>');
    const iconFilePlus = this.sanitizer.bypassSecurityTrustHtml('<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M14.5 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7.5L14.5 2z"/><polyline points="14 2 14 8 20 8"/><line x1="12" x2="12" y1="18" y2="12"/><line x1="9" x2="15" y1="15" y2="15"/></svg>');
    const iconList = this.sanitizer.bypassSecurityTrustHtml('<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><line x1="8" x2="21" y1="6" y2="6"/><line x1="8" x2="21" y1="12" y2="12"/><line x1="8" x2="21" y1="18" y2="18"/><line x1="3" x2="3.01" y1="6" y2="6"/><line x1="3" x2="3.01" y1="12" y2="12"/><line x1="3" x2="3.01" y1="18" y2="18"/></svg>');
    const iconBell = this.sanitizer.bypassSecurityTrustHtml('<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M6 8a6 6 0 0 1 12 0c0 7 3 9 3 9H3s3-2 3-9"/><path d="M10.3 21a1.94 1.94 0 0 0 3.4 0"/></svg>');

    // BLOCK ACCESS: If password change is required, show no navigation links
    if (this.authService.currentUser()?.requirePasswordChange) {
      return [
        { path: '/settings/change-password', label: 'Security - Change Password', icon: iconShield }
      ];
    }

    switch (role) {
      case 'admin':
        return [
          { path: '/admin/dashboard', label: 'Dashboard', icon: iconDashboard },
          { path: '/admin/plans', label: 'Insurance Plans', icon: iconShield },
          { path: '/admin/clients', label: 'Client Approvals', icon: iconBuilding },
          { path: '/admin/staff', label: 'Staff Management', icon: iconUsers },
          { path: '/admin/policies', label: 'Policies & Invoices', icon: iconFileText },
          { path: '/admin/claims', label: 'System Claims', icon: iconList },
          { path: '/admin/approvals', label: 'Executive Approvals', icon: iconShield },
          { path: '/admin/logs', label: 'Audit Logs', icon: iconEdit3 },
          { path: '/notifications/history', label: 'Activity History', icon: iconBell }
        ];
      case 'agent':
        return [
          { path: '/agent/dashboard', label: 'Dashboard', icon: iconDashboard },
          { path: '/agent/customers', label: 'Customers', icon: iconUsers },
          { path: '/agent/policies', label: 'Policies', icon: iconFileText },
          { path: '/agent/endorsements', label: 'Endorsements', icon: iconEdit3 },
          { path: '/agent/commissions', label: 'Commission Ledger', icon: iconCreditCard },
          { path: '/notifications/history', label: 'Activity History', icon: iconBell }
        ];
      case 'customer':
        return [
          { path: '/customer/dashboard', label: 'Dashboard', icon: iconDashboard },
          { path: '/customer/profile', label: 'Profile', icon: iconUser },
          { path: '/customer/policies', label: 'Policies & Members', icon: iconFileText },
          { path: '/customer/invoices', label: 'Invoices', icon: iconCreditCard },
          { path: '/customer/claims', label: 'Claims', icon: iconFilePlus },
          { path: '/customer/revisions', label: 'Revisions', icon: iconEdit3 },
          { path: '/notifications/history', label: 'Activity History', icon: iconBell }
        ];
      case 'claimsofficer':
        return [
          { path: '/claims-officer/dashboard', label: 'Dashboard', icon: iconDashboard },
          { path: '/claims-officer/queue', label: 'Actionable Queue', icon: iconList },
          { path: '/claims-officer/history', label: 'Review History', icon: iconFileText },
          { path: '/notifications/history', label: 'Activity History', icon: iconBell }
        ];
      default:
        return [];
    }
  }
}

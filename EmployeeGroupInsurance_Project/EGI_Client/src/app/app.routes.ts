import { Routes } from '@angular/router';
import { roleGuard } from './core/guards/role.guard';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    {
        path: 'error',
        loadComponent: () => import('./components/error/error').then(m => m.ErrorComponent)
    },
    {
        path: 'account-blocked',
        loadComponent: () => import('./components/auth/account-blocked/account-blocked').then(m => m.AccountBlockedComponent)
    },
    {
        path: 'home',
        loadComponent: () => import('./components/home/home').then(m => m.HomeComponent)
    },
    {
        path: 'auth/login',
        loadComponent: () => import('./components/auth/login/login').then(m => m.LoginComponent)
    },
    {
        path: 'auth/register',
        loadComponent: () => import('./components/auth/register/register').then(m => m.RegisterComponent)
    },
    {
        path: 'auth/forgot-password',
        loadComponent: () => import('./components/auth/forgot-password/forgot-password').then(m => m.ForgotPasswordComponent)
    },
    {
        path: 'auth/reset-password',
        loadComponent: () => import('./components/auth/reset-password/reset-password').then(m => m.ResetPasswordComponent)
    },

    {
        path: '',
        loadComponent: () => import('./layout/navbar/navbar').then(m => m.AppLayoutComponent),
        children: [
            {
                path: 'settings/change-password',
                loadComponent: () => import('./components/settings/change-password/change-password').then(m => m.ChangePasswordComponent)
            },
            {
                path: 'notifications/history',
                loadComponent: () => import('./components/notifications/history/history').then(m => m.NotificationHistoryComponent)
            },
            {
                path: 'admin',
                canActivate: [roleGuard],
                data: { roles: ['Admin'] },
                children: [
                    {
                        path: 'dashboard',
                        loadComponent: () => import('./components/admin/dashboard/dashboard').then(m => m.AdminDashboardComponent)
                    },
                    { path: 'plans', loadComponent: () => import('./components/admin/dashboard/dashboard').then(m => m.AdminDashboardComponent) },
                    { path: 'clients', loadComponent: () => import('./components/admin/dashboard/dashboard').then(m => m.AdminDashboardComponent) },
                    { path: 'staff', loadComponent: () => import('./components/admin/dashboard/dashboard').then(m => m.AdminDashboardComponent) },
                    { path: 'policies', loadComponent: () => import('./components/admin/dashboard/dashboard').then(m => m.AdminDashboardComponent) },
                    { path: 'claims', loadComponent: () => import('./components/admin/dashboard/dashboard').then(m => m.AdminDashboardComponent) },
                    { path: 'logs', loadComponent: () => import('./components/admin/dashboard/dashboard').then(m => m.AdminDashboardComponent) }
                ]
            },
            {
                path: 'agent',
                canActivate: [roleGuard],
                data: { roles: ['Agent'] },
                children: [
                    {
                        path: 'dashboard',
                        loadComponent: () => import('./components/agent/dashboard/dashboard').then(m => m.AgentDashboardComponent)
                    },
                    { path: 'customers', loadComponent: () => import('./components/agent/dashboard/dashboard').then(m => m.AgentDashboardComponent) },
                    { path: 'policies', loadComponent: () => import('./components/agent/dashboard/dashboard').then(m => m.AgentDashboardComponent) },
                    { path: 'endorsements', loadComponent: () => import('./components/agent/dashboard/dashboard').then(m => m.AgentDashboardComponent) },
                    { path: 'commissions', loadComponent: () => import('./components/agent/dashboard/dashboard').then(m => m.AgentDashboardComponent) }
                ]
            },
            {
                path: 'customer',
                canActivate: [roleGuard],
                data: { roles: ['Customer'] },
                children: [
                    {
                        path: 'dashboard',
                        loadComponent: () => import('./components/customer/dashboard/dashboard').then(m => m.CustomerDashboardComponent)
                    },
                    { path: 'profile', loadComponent: () => import('./components/customer/dashboard/dashboard').then(m => m.CustomerDashboardComponent) },
                    { path: 'policies', loadComponent: () => import('./components/customer/dashboard/dashboard').then(m => m.CustomerDashboardComponent) },
                    { path: 'invoices', loadComponent: () => import('./components/customer/dashboard/dashboard').then(m => m.CustomerDashboardComponent) },
                    { path: 'claims', loadComponent: () => import('./components/customer/dashboard/dashboard').then(m => m.CustomerDashboardComponent) },
                    { path: 'revisions', loadComponent: () => import('./components/customer/dashboard/dashboard').then(m => m.CustomerDashboardComponent) },
                    { path: 'premium-error', loadComponent: () => import('./components/customer/premium-error/premium-error').then(m => m.PremiumErrorComponent) }
                ]
            },
            {
                path: 'claims-officer',
                canActivate: [roleGuard],
                data: { roles: ['ClaimsOfficer'] },
                children: [
                    { path: 'dashboard', loadComponent: () => import('./components/claims-officer/dashboard/dashboard').then(m => m.ClaimsOfficerDashboardComponent) },
                    { path: 'queue', loadComponent: () => import('./components/claims-officer/dashboard/dashboard').then(m => m.ClaimsOfficerDashboardComponent) },
                    { path: 'history', loadComponent: () => import('./components/claims-officer/dashboard/dashboard').then(m => m.ClaimsOfficerDashboardComponent) }
                ]
            }
        ]
    },

    { path: '**', redirectTo: 'auth/login' }
];

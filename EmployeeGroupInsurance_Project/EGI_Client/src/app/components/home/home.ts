import { Component, inject, signal, HostListener, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ThemeService } from '../../core/services/theme.service';
import { AuthService } from '../../core/services/auth.service';
import { PublicService } from '../../data-access/public.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './home.html',
  styleUrls: ['./home.css']
})
export class HomeComponent implements OnInit {
  protected theme = inject(ThemeService);
  protected authService = inject(AuthService);
  private publicService = inject(PublicService);
  isScrolled = signal(false);
  isMenuOpen = signal(false);

  // Policy Plan Cards for Landing Page
  activePlans = signal<any[]>([]);

  ngOnInit() {
    // Auto-logout: Users must be logged out when returning to public landing
    this.authService.clearSession();

    const defaultPlans = [
      { planName: 'Bronze Shield', basePremium: 2500, coverages: [{ coverageName: 'Essential Medical Coverage' }, { coverageName: 'Standard Room Limits' }, { coverageName: 'Co-pay 20% on Clinics' }] },
      { planName: 'Silver Premium', basePremium: 5000, coverages: [{ coverageName: 'Comprehensive Medical' }, { coverageName: 'Dental & Vision Included' }, { coverageName: 'No Co-pay at Network' }, { coverageName: 'Priority Claims Handling' }] },
      { planName: 'Gold Pinnacle', basePremium: 12500, coverages: [{ coverageName: 'Global Health Access' }, { coverageName: 'Private Suites' }, { coverageName: '0% Deductible' }, { coverageName: 'Dedicated Officer' }] }
    ];

    const mapPlans = (plansToMap: any[]) => {
      // slice(0, 3) ensures we explicitly limit to 3 policy cards total on the UI
      return plansToMap.slice(0, 3).map((plan, index) => {
        let colorClass = 'text-amber-700 dark:text-amber-500';
        let bgClass = 'bg-amber-50 dark:bg-amber-500/10 border-amber-200 dark:border-amber-500/20';
        let badge = '';

        // Map real Coverages to string features
        const featureList = plan.coverages?.slice(0, 4).map((c: any) => {
          if (c.coverageName) return c.coverageName; // fallback support
          const amt = Number(c.coverageAmount).toLocaleString('en-IN') || '0';
          return `${c.type} Coverage up to ₹${amt} (${c.coveredGroup})`;
        }) || ['Standard Coverage', 'Basic Hospitalization'];

        if (index === 1 || plan.planName?.toLowerCase().includes('gold') || plan.planName?.toLowerCase().includes('silver')) {
          colorClass = 'text-slate-700 dark:text-slate-300';
          bgClass = 'bg-slate-50 dark:bg-slate-800 border-slate-300 dark:border-slate-600 shadow-xl shadow-blue-500/10 scale-105 z-10';
          badge = 'Most Popular';
        } else if (index === 2 || plan.planName?.toLowerCase().includes('enterprise') || plan.planName?.toLowerCase().includes('platinum')) {
          colorClass = 'text-emerald-600 dark:text-emerald-400';
          bgClass = 'bg-emerald-50 dark:bg-emerald-500/10 border-emerald-200 dark:border-emerald-500/20';
          badge = 'Enterprise';
        }

        return {
          name: plan.planName,
          price: plan.basePremium,
          period: 'yearly',
          features: featureList,
          colorClass,
          bgClass,
          badge
        };
      });
    };

    // Fetch dynamic backend plan data for public landing
    this.publicService.getInsurancePlans().subscribe({
      next: (response: any) => {
        // Handle common backend generic response wrappers in case it's not a direct Array
        let plansArray = Array.isArray(response) ? response : (response?.data || response?.items || response?.result || []);

        if (!plansArray || !Array.isArray(plansArray) || plansArray.length === 0) {
          this.activePlans.set(mapPlans(defaultPlans));
        } else {
          this.activePlans.set(mapPlans(plansArray));
        }
      },
      error: (err) => {
        console.warn("Backend offline or unreachable, falling back to cached public plan data.", err);
        this.activePlans.set(mapPlans(defaultPlans));
      }
    });
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    this.isScrolled.set(window.scrollY > 20);
  }

  scrollTo(id: string) {
    const el = document.getElementById(id);
    if (el) {
      el.scrollIntoView({ behavior: 'smooth', block: 'start' });
      this.isMenuOpen.set(false);
    }
  }
}

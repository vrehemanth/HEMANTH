import {
  Component, Input, OnDestroy, viewChild, ElementRef, effect, ChangeDetectionStrategy
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { Chart, registerables } from 'chart.js';

Chart.register(...registerables);

/**
 * Enterprise Interface for Dashboard State Management
 * Resolves Circular Dependency (Template <-> Tab <-> Dashboard)
 */
export interface IDashboard {
  profileData: () => any;
  summary: () => any;
  invoices: () => any[];
  claims: () => any[];
  policies: () => any[];
  hospitals: () => any[];
  members: () => any[];
  endorsements: () => any[];
  isLoading: () => boolean;
  isPremiumDueSoon: () => boolean;
  selectedHealthCheckHospital: any;
  healthCheckClaimedDate: () => any;
  healthCheckExpiryDate: () => any;
  showHealthCheckModal: any;
  isHealthCheckEligible: () => boolean;
  isHealthCheckCooldownOver: () => boolean;
  isHealthCheckActive: () => boolean;
  formatINR: (n: any) => string;
  claimHealthCheck: () => void;
  confirmHealthCheckExecution: () => void;
  generateSimpleVoucher: () => void;
  getEligiblePersonnelCount: () => any;
  viewClaimDetail: (claim: any) => void;
  setActiveTab: (tab: string) => void;
}

@Component({
  selector: 'app-overview-tab',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './overview.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class OverviewTabComponent implements OnDestroy {
  @Input() p!: IDashboard;

  premiumCanvas = viewChild<ElementRef<HTMLCanvasElement>>('premiumChart');
  claimsCanvas = viewChild<ElementRef<HTMLCanvasElement>>('claimsChart');

  private premiumChart?: Chart;
  private claimsChart?: Chart;
  private chartTimeout: any;

  private chartEffect = effect(() => {
    const pCanvas = this.premiumCanvas();
    const cCanvas = this.claimsCanvas();
    const summary = this.p?.summary();

    if (pCanvas && cCanvas && summary) {
      if (this.chartTimeout) clearTimeout(this.chartTimeout);
      this.chartTimeout = setTimeout(() => {
        this.destroyCharts();
        this.initPremiumChart(pCanvas.nativeElement);
        this.initClaimsChart(cCanvas.nativeElement);
      }, 400);
    }
  });

  private destroyCharts() {
    this.premiumChart?.destroy();
    this.claimsChart?.destroy();
    this.premiumChart = undefined;
    this.claimsChart = undefined;
  }

  private initPremiumChart(ctx: HTMLCanvasElement) {
    const now = new Date();
    const month = now.getMonth();
    const year = now.getFullYear();

    const monthlyInvoices = this.p.invoices().filter((inv: any) => {
      const d = new Date(inv.invoiceDate);
      return d.getMonth() === month && d.getFullYear() === year;
    });

    const totalPaid = monthlyInvoices
      .filter((inv: any) => inv.status === 'Paid')
      .reduce((sum: number, i: any) => sum + (i.amount || 0), 0);

    const totalDue = monthlyInvoices
      .filter((inv: any) => inv.status !== 'Paid')
      .reduce((sum: number, i: any) => sum + (i.amount || 0), 0);

    this.premiumChart = new Chart(ctx, {
      type: 'doughnut',
      data: {
        labels: ['Settled (Current Month)', 'Outstanding (Current Month)'],
        datasets: [{
          data: [totalPaid, totalDue],
          backgroundColor: ['#10b981', '#ef4444'],
          borderWidth: 0,
          hoverOffset: 15,
          borderRadius: 8
        } as any]
      },
      options: {
        cutout: '82%',
        responsive: true,
        maintainAspectRatio: false,
        animation: { animateRotate: true, animateScale: true },
        plugins: {
          legend: { display: false },
          tooltip: {
            backgroundColor: 'rgba(0,0,0,0.8)',
            padding: 12,
            callbacks: {
              label: (item: any) => ` ₹${item.raw.toLocaleString('en-IN')}`
            }
          }
        }
      }
    });
  }

  private initClaimsChart(ctx: HTMLCanvasElement) {
    const now = new Date();
    const months: string[] = [];
    const totalData: number[] = [];
    const approvedData: number[] = [];

    for (let i = 5; i >= 0; i--) {
      const d = new Date(now.getFullYear(), now.getMonth() - i, 1);
      months.push(d.toLocaleString('default', { month: 'short' }));
      const mClaims = this.p.claims().filter((c: any) => {
        const cd = new Date(c.claimDate);
        return cd.getMonth() === d.getMonth() && cd.getFullYear() === d.getFullYear();
      });
      totalData.push(mClaims.length);
      approvedData.push(mClaims.filter((c: any) => c.status === 'Approved').length);
    }

    this.claimsChart = new Chart(ctx, {
      type: 'line',
      data: {
        labels: months,
        datasets: [
          {
            label: 'Total Claims',
            data: totalData,
            fill: true,
            backgroundColor: 'rgba(99,102,241,0.08)',
            borderColor: 'rgb(99,102,241)',
            tension: 0.4,
            pointRadius: 4,
            pointHoverRadius: 7,
            pointBackgroundColor: 'rgb(99,102,241)',
            borderWidth: 2
          },
          {
            label: 'Approved',
            data: approvedData,
            fill: false,
            borderColor: '#10b981',
            tension: 0.4,
            pointRadius: 4,
            pointHoverRadius: 7,
            pointBackgroundColor: '#10b981',
            borderWidth: 2
          }
        ]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          legend: { display: false },
          tooltip: { mode: 'index', intersect: false, backgroundColor: 'rgba(0,0,0,0.8)', padding: 12 }
        },
        scales: {
          x: { display: true, grid: { display: false }, ticks: { font: { size: 10, weight: 600 } } },
          y: { display: true, grid: { color: 'rgba(0,0,0,0.05)' }, border: { dash: [5, 5] }, ticks: { stepSize: 1, font: { size: 10 } } }
        }
      }
    });
  }

  ngOnDestroy() {
    if (this.chartTimeout) clearTimeout(this.chartTimeout);
    this.destroyCharts();
  }
}

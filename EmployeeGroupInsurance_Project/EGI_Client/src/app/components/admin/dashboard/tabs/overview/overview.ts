import { Component, Input, viewChild, ElementRef, effect, OnDestroy, OnInit, inject, Injector } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-overview-tab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './overview.html'
})
export class OverviewTabComponent implements OnInit, OnDestroy {
  @Input() p!: AdminDashboardComponent;

  financialCanvas = viewChild<ElementRef<HTMLCanvasElement>>('financialChart');
  claimTrendCanvas = viewChild<ElementRef<HTMLCanvasElement>>('claimTrendChart');
  planMixCanvas = viewChild<ElementRef<HTMLCanvasElement>>('planMixChart');

  private chartTimeout: any;
  private injector = inject(Injector);

  ngOnInit() {
    effect(() => {
      // Access all required signals to track changes
      const fCanvas = this.financialCanvas();
      const cCanvas = this.claimTrendCanvas();
      const pCanvas = this.planMixCanvas();
      
      const summary = this.p.summary();
      const claims = this.p.allClaimsRegistry();
      const policies = this.p.allPolicyAssignments();
      const currentTab = this.p.activeTab();

      if (this.p && fCanvas && cCanvas && pCanvas && currentTab === 'dashboard') {
        // Only attempt to init if we have summary data
        if (summary) {
          if (this.chartTimeout) clearTimeout(this.chartTimeout);
          this.chartTimeout = setTimeout(() => {
            this.p.destroyCharts();
            this.p.initCharts(fCanvas.nativeElement, cCanvas.nativeElement, pCanvas.nativeElement);
          }, 500);
        }
      }
    }, { injector: this.injector });
  }

  ngOnDestroy() {
    if (this.chartTimeout) clearTimeout(this.chartTimeout);
    if (this.p) {
      this.p.destroyCharts();
    }
  }
}

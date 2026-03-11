import { Component, Input, OnInit, OnDestroy, viewChild, ElementRef, effect, Injector, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgentDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-overview-tab',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './overview.html'
})
export class OverviewTabComponent implements OnInit, OnDestroy {
  @Input() p!: AgentDashboardComponent;
  private injector = inject(Injector);

  commissionCanvas = viewChild<ElementRef<HTMLCanvasElement>>('commissionChart');
  outstandingCanvas = viewChild<ElementRef<HTMLCanvasElement>>('outstandingChart');
  salesMixCanvas = viewChild<ElementRef<HTMLCanvasElement>>('salesMixChart');

  private chartTimeout: any;

  ngOnInit() {
    effect(() => {
      const cCanvas = this.commissionCanvas();
      const oCanvas = this.outstandingCanvas();
      const sCanvas = this.salesMixCanvas();

      // Track signals for reactivity
      const summary = this.p.summary();
      const policiesList = this.p.policies();
      const tab = this.p.activeTab();

      if (cCanvas && oCanvas && sCanvas && tab === 'overview' && summary) {
        if (this.chartTimeout) clearTimeout(this.chartTimeout);
        this.chartTimeout = setTimeout(() => {
          this.p.destroyCharts();
          this.p.initCharts(cCanvas.nativeElement, oCanvas.nativeElement, sCanvas.nativeElement);
        }, 500);
      }
    }, { injector: this.injector });
  }

  ngOnDestroy() {
    if (this.chartTimeout) clearTimeout(this.chartTimeout);
    this.p.destroyCharts();
  }
}

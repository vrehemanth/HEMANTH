import { Component, Input, OnInit, OnDestroy, viewChild, ElementRef, effect, Injector, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClaimsOfficerDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-overview-tab',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './overview.html'
})
export class OverviewTabComponent implements OnInit, OnDestroy {
  @Input() p!: ClaimsOfficerDashboardComponent;
  private injector = inject(Injector);

  decisionCanvas = viewChild<ElementRef<HTMLCanvasElement>>('decisionChart');
  adjudicationCanvas = viewChild<ElementRef<HTMLCanvasElement>>('adjudicationChart');
  typeCanvas = viewChild<ElementRef<HTMLCanvasElement>>('typeChart');

  private chartTimeout: any;

  ngOnInit() {
    effect(() => {
      const dCanvas = this.decisionCanvas()?.nativeElement;
      const aCanvas = this.adjudicationCanvas()?.nativeElement;
      const tCanvas = this.typeCanvas()?.nativeElement;

      // Track signals for reactivity
      const summary = this.p.summary();
      const history = this.p.claimHistory();
      const tab = this.p.activeTab();

      if (dCanvas && aCanvas && tCanvas && tab === 'overview' && summary) {
        if (this.chartTimeout) clearTimeout(this.chartTimeout);
        this.chartTimeout = setTimeout(() => {
          this.p.destroyCharts();
          this.p.initCharts(dCanvas, aCanvas, tCanvas);
        }, 500);
      }
    }, { injector: this.injector });
  }

  ngOnDestroy() {
    if (this.chartTimeout) clearTimeout(this.chartTimeout);
    this.p.destroyCharts();
  }
}

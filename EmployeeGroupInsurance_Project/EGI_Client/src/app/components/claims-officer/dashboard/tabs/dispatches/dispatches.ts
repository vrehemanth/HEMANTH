import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClaimsOfficerDashboardComponent } from '../../dashboard';

@Component({
  selector: 'app-dispatches-tab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="space-y-6 animate-in slide-in-from-bottom-4 duration-500">
      <!-- Search & Status Stats -->
      <div class="grid grid-cols-1 md:grid-cols-4 gap-6 items-center">
        <div class="md:col-span-3 glass p-1.5 rounded-2xl flex items-center gap-2 border border-white/20 max-w-xl">
          <div class="w-8 h-8 rounded-full bg-primary-500/10 flex items-center justify-center text-primary-600 ml-1">
            <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" stroke-width="3"></path></svg>
          </div>
          <input type="text" 
                 [ngModel]="p.dispatchSearchTerm()" 
                 (ngModelChange)="p.dispatchSearchTerm.set($event)"
                 placeholder="Search Patients..." 
                 class="bg-transparent border-none outline-none w-full font-bold text-sm text-gray-700 dark:text-gray-200 px-2"/>
        </div>

        <div class="glass p-4 rounded-2xl bg-gradient-to-br from-emerald-500/10 to-primary-500/5 border border-emerald-500/20">
            <div class="flex items-center justify-between">
                <div>
                    <h4 class="text-[8px] font-black uppercase tracking-widest text-emerald-600 mb-0.5">Active Intakes</h4>
                    <p class="text-lg font-black dark:text-white">{{ p.liveDispatches().length }} <span class="text-[10px] uppercase font-normal opacity-40">Live</span></p>
                </div>
                <div class="w-8 h-8 rounded-xl bg-emerald-500 text-white flex items-center justify-center animate-pulse">
                    <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path d="M13 10V3L4 14h7v7l9-11h-7z" stroke-width="2.5"></path></svg>
                </div>
            </div>
        </div>
      </div>

      <!-- DISPATCH LIST -->
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        @for (d of p.filteredLiveDispatches(); track d.id) {
          <div class="group relative bg-white dark:bg-gray-800 rounded-[2.5rem] p-8 border border-gray-100 dark:border-gray-700 shadow-sm hover:shadow-2xl hover:border-primary-500/30 transition-all duration-500 overflow-hidden">
             <!-- Status Badge -->
             <div class="absolute top-6 right-8 px-3 py-1 bg-primary-50 text-primary-600 rounded-full text-[8px] font-black uppercase tracking-widest border border-primary-100">
                {{ d.status }}
             </div>

             <div class="flex items-start gap-4 mb-8">
                <div class="w-16 h-16 rounded-3xl bg-gray-50 dark:bg-gray-700 flex items-center justify-center text-gray-400 group-hover:bg-primary-500 group-hover:text-white transition-all duration-500 shadow-inner">
                    <svg class="w-8 h-8" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" stroke-width="2.5"></path></svg>
                </div>
                <div>
                    <h5 class="text-lg font-black dark:text-white leading-tight mb-1">{{ d.patientName }}</h5>
                    <p class="text-[10px] font-bold text-gray-400 uppercase tracking-widest">{{ d.employeeCode }}</p>
                </div>
             </div>

             <div class="space-y-4 mb-8">
                <div class="flex items-center gap-3">
                    <div class="w-8 h-8 rounded-xl bg-gray-50 dark:bg-gray-700 text-primary-600 flex items-center justify-center">
                        <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path d="M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5M9 7h1m-1 4h1m4-4h1m-1 4h1m-5 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4" stroke-width="2"></path></svg>
                    </div>
                    <div>
                        <p class="text-[8px] font-black text-gray-400 uppercase">Target Facility</p>
                        <p class="text-xs font-bold dark:text-gray-200">{{ d.hospitalName }}</p>
                    </div>
                </div>

                <div class="flex items-center gap-3">
                   <div class="w-8 h-8 rounded-xl bg-gray-50 dark:bg-gray-700 text-amber-600 flex items-center justify-center">
                        <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" stroke-width="2"></path></svg>
                   </div>
                   <div>
                       <p class="text-[8px] font-black text-gray-400 uppercase">Dispatch Time</p>
                       <p class="text-xs font-bold dark:text-gray-200">{{ d.dispatchDate | date:'mediumTime' }} <span class="opacity-40 font-normal">({{ d.dispatchDate | date:'shortDate' }})</span></p>
                   </div>
                </div>
             </div>

             <div class="bg-gray-50 dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-2xl p-4 mb-8">
                <p class="text-[8px] font-black text-primary-600 uppercase mb-2 tracking-widest italic">Digital Coverage Slip Summary</p>
                <div class="max-h-24 overflow-y-auto pr-2 custom-scrollbar">
                    <p class="text-[10px] font-bold text-gray-600 dark:text-gray-400 leading-relaxed whitespace-pre-line">{{ formatCoverage(d.coverageSummary) }}</p>
                </div>
             </div>

             <div class="flex gap-2">
                <button (click)="p.initiateIntakeFromDispatch(d)"
                        class="flex-1 py-4 bg-primary-600 hover:bg-primary-700 text-white rounded-2xl text-[10px] font-black uppercase tracking-wider shadow-lg shadow-primary-500/20 transition-all active:scale-95">
                    Process Bill Intake
                </button>
             </div>
          </div>
        } @empty {
          <div class="col-span-full py-20 flex flex-col items-center justify-center opacity-40">
             <div class="w-24 h-24 rounded-full border-4 border-dashed border-gray-300 flex items-center justify-center mb-6">
                <svg class="w-10 h-10 text-gray-300" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" stroke-width="2"></path></svg>
             </div>
             <h4 class="text-xl font-black uppercase tracking-tighter italic">Intake Stream Silent</h4>
             <p class="text-[10px] font-bold uppercase mt-2">All dispatched patients have been processed</p>
          </div>
        }
      </div>
    </div>
  `
})
export class DispatchesTabComponent {
  @Input({ required: true }) p!: ClaimsOfficerDashboardComponent;

  formatCoverage(json: string): string {
    try {
      const data = JSON.parse(json);
      if (Array.isArray(data)) {
        return data.map(c => `${c.ClaimType || c.claimType}: ₹${(c.Remaining || c.remaining || 0).toLocaleString('en-IN')}`).join('\n');
      }
      return json;
    } catch {
      return json;
    }
  }
}

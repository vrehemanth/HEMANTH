import { Component, Input, OnInit, effect, signal, OnDestroy, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomerDashboardComponent } from '../../dashboard';

declare var L: any;

@Component({
  selector: 'app-hospitals-tab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './hospitals.html'
})
export class HospitalsTabComponent implements OnInit, OnDestroy {
  @Input() p!: CustomerDashboardComponent;

  selectedHospital = signal<any>(null);
  
  // Searchable Select State
  memberSearchQuery = signal('');
  isDropdownOpen = signal(false);
  
  filteredPeople = computed(() => {
    const q = this.memberSearchQuery().toLowerCase().trim();
    const all = this.p.allEligiblePeople();
    if (!q) return all;
    return all.filter(person => 
      person.fullName.toLowerCase().includes(q) || 
      person.employeeCode?.toLowerCase().includes(q)
    );
  });
  private map: any;
  private marker: any;

  constructor() {
    effect(() => {
      const h = this.selectedHospital();
      if (h && this.map) {
        this.updateMapMarker(h.latitude, h.longitude, h.name);
      }
    });

    // Auto-center on user if they allow location
    effect(() => {
        const loc = this.p.userLocation();
        if (loc && this.map) {
            this.map.panTo([loc.lat, loc.lng]);
            if (this.map.getZoom() < 10) this.map.setZoom(12);
        }
    });
  }

  ngOnInit() {
    this.p.getUserLocation();
    setTimeout(() => this.initLeafletMap(), 300);

    // Select first hospital by default if available
    setTimeout(() => {
      const list = this.p.hospitals();
      if (list.length > 0 && !this.selectedHospital()) {
        this.selectedHospital.set(list[0]);
      }
    }, 1000);
  }

  ngOnDestroy() {
    if (this.map) {
      this.map.remove();
    }
  }

  private initLeafletMap() {
    if (typeof L === 'undefined') {
      console.warn("Retrying Leaflet initialization...");
      setTimeout(() => this.initLeafletMap(), 1000);
      return;
    }

    try {
      // Default center is India (or user location if available)
      const loc = this.p.userLocation() || { lat: 20.5937, lng: 78.9629 };
      
      this.map = L.map('customerHospitalMap').setView([loc.lat, loc.lng], 5);

      L.tileLayer('https://{s}.tile.host/tile/{z}/{x}/{y}.png', {
         // Using a cleaner tile provider if possible, otherwise standard OSM
      }).addTo(this.map);

      // standard OSM fallback
      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
      }).addTo(this.map);

      setTimeout(() => this.map.invalidateSize(), 500);

    } catch (e) {
      console.error("Map Load Error:", e);
    }
  }

  selectHospital(h: any) {
    this.selectedHospital.set(h);
  }

  selectPerson(person: any) {
    this.p.selectedMemberForHospitalId.set(person.id);
    this.memberSearchQuery.set('');
    this.isDropdownOpen.set(false);
  }

  private updateMapMarker(lat: number, lng: number, title: string) {
    if (!this.map) return;

    if (this.marker) {
      this.marker.setLatLng([lat, lng]);
    } else {
      this.marker = L.marker([lat, lng]).addTo(this.map);
    }

    this.map.panTo([lat, lng]);
    if (this.map.getZoom() < 13) this.map.setZoom(15);
  }
}

import { Component, Input, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminDashboardComponent } from '../../dashboard';

declare var L: any;

@Component({
  selector: 'app-hospitals-tab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './hospitals.html'
})
export class HospitalsTabComponent {
  @Input() p!: AdminDashboardComponent;

  private map: any;
  private marker: any;

  constructor() {
    effect(() => {
      if (this.p.showHospitalForm()) {
        setTimeout(() => this.initLeafletMap(), 300);
      }
    });

    // Watch for center changes (from Nominatim Search or Edit)
    effect(() => {
      const center = this.p.hospitalCenter();
      if (this.map && this.marker) {
        this.marker.setLatLng([center.lat, center.lng]);
        this.map.panTo([center.lat, center.lng]);
        if (this.map.getZoom() < 13) this.map.setZoom(15);
      }
    });
  }

  private initLeafletMap() {
    if (typeof L === 'undefined') {
      console.warn("Leaflet not loaded yet. Retrying...");
      setTimeout(() => this.initLeafletMap(), 1000);
      return;
    }

    const center = this.p.hospitalCenter();
    const isEdit = this.p.isEditingHospital();

    if (this.map) {
      this.map.remove();
    }

    try {
      this.map = L.map('hospitalMap').setView([center.lat, center.lng], isEdit ? 15 : 5);

      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors',
        className: 'map-tiles'
      }).addTo(this.map);

      this.marker = L.marker([center.lat, center.lng], {
        draggable: true
      }).addTo(this.map);

      // Marker drag listener
      this.marker.on('dragend', (event: any) => {
        const position = event.target.getLatLng();
        this.updateCoordinates(position.lat, position.lng);
      });

      // Map click listener
      this.map.on('click', (e: any) => {
        this.marker.setLatLng(e.latlng);
        this.updateCoordinates(e.latlng.lat, e.latlng.lng);
      });

      // Ensure proper render size in dynamic container
      setTimeout(() => this.map.invalidateSize(), 400);

    } catch (e) {
      console.error("Leaflet Error:", e);
    }
  }

  private updateCoordinates(lat: number, lng: number) {
    this.p.newHospital.latitude = Number(lat.toFixed(6));
    this.p.newHospital.longitude = Number(lng.toFixed(6));
  }
}

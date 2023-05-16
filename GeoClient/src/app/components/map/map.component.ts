import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataService } from '../../data.service';
import * as L from 'leaflet';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit, AfterViewInit {
  options = {
    position: 'left'
  };

  private buildingsLayer: L.LayerGroup;
  private parcelsLayer: L.LayerGroup;
  private map: L.Map | null;

  constructor(private dataService: DataService) {
    this.buildingsLayer = L.layerGroup();
    this.parcelsLayer = L.layerGroup();
    this.map = null;
  }

  ngOnInit() {
    // Do nothing here
  }

  ngAfterViewInit(): void {
    this.initMap();
    this.loadBuildingData();
    this.loadParcelData();

    // Add the layers to the map.
    if (this.map) {
      this.buildingsLayer.addTo(this.map);
      this.parcelsLayer.addTo(this.map);
    }
    else {
      console.error("Map is null, cannot add layers.");
    }
  }

  private initMap(): void {
    this.map = L.map('map', { zoomControl: false }).setView([41.432577, 31.728689], 17);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '© <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    }).addTo(this.map);

    L.control
      .zoom({
        position: 'topright',
      })
      .addTo(this.map);

    const allBuildingsCheckbox = document.getElementById('allBuildingsCheckbox');
    if (allBuildingsCheckbox) {
      allBuildingsCheckbox.addEventListener('change', (event: any) => {
        if (event.target.checked) {
          this.loadBuildingData();
          this.buildingsLayer.addTo(this.map!);
        } else {
          this.buildingsLayer.remove();
        }
      });
    }

    const allParcelsCheckbox = document.getElementById('allParcelsCheckbox');
    if (allParcelsCheckbox) {
      allParcelsCheckbox.addEventListener('change', (event: any) => {
        if (event.target.checked) {
          this.loadParcelData();
          this.parcelsLayer.addTo(this.map!);
        } else {
          this.parcelsLayer.remove();
        }
      });
    }
  }

  private addBuildingDataToMap(data: any): void {
    const buildings = L.geoJSON(data,
      {
        style: {
          color: '#ff7800',
          weight: 3,
          opacity: 0.7
        },
        onEachFeature: (feature, layer) => {
          if (feature.properties) {
            layer.bindPopup(`<strong>ID:</strong> ${feature.properties.Id}<br><strong>Blok:</strong> ${feature.properties.Blok}<br><strong>Nitelik:</strong> ${feature.properties.Nitelik}`);
          }
        }
          
        
       });
    console.log('Building layer:', buildings);
    this.buildingsLayer.addLayer(buildings);
  }

  private addParcelDataToMap(data: any): void {
    const parcels = L.geoJSON(data, {
      style: {
        color: '#3388ff',
        weight: 2,
        opacity: 0.8
      },
      onEachFeature: (feature, layer) => {
        if (feature.properties) {
          layer.bindPopup(`<strong>ID:</strong> ${feature.properties.Id}<br><strong>Parsel No:</strong> ${feature.properties.ParselNo}<br><strong>Pafta:</strong> ${feature.properties.Pafta}<br><strong>Ada:</strong> ${feature.properties.Ada}<br><strong>İl:</strong> ${feature.properties.il}<br><strong>İlçe:</strong> ${feature.properties.ilce}<br><strong>Mahalle:</strong> ${feature.properties.mahalle}<br><strong>Nitelik:</strong> ${feature.properties.nitelik}`);
        }
      }
    });
    console.log('Parcel layer:', parcels);
    this.parcelsLayer.addLayer(parcels);
  }

  private loadBuildingData(): void {
    this.dataService.getAllBuildings().subscribe((data) => {
      console.log('Building data:', data);
      this.addBuildingDataToMap(data);
    });
  }

  private loadParcelData(): void {
    this.dataService.getAllParcels().subscribe((data) => {
      console.log('Parcel data:', data);
      this.addParcelDataToMap(data);
    });
  }
}

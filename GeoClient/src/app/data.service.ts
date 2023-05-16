import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  getAllBuildings(): Observable<any> {
    return this.http.get('https://localhost:7211/api/Buildings');
  }

  getAllParcels(): Observable<any> {
    return this.http.get('https://localhost:7211/api/Parcels');
  }
}


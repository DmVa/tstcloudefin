import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Vendor } from '../models/vendor.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class VendorService {
  private readonly baseUrl = `${environment.apiUrl}/Vendor`;

  constructor(private readonly http: HttpClient) {}

  getVendors(skip = 0, take = 10): Observable<Vendor[]> {
    return this.http.get<Vendor[]>(this.baseUrl, { params: { skip, take } });
  }

  getVendor(id: string): Observable<Vendor> {
    return this.http.get<Vendor>(`${this.baseUrl}/${id}`);
  }

  addVendor(vendor: Vendor): Observable<void> {
    return this.http.post<void>(this.baseUrl, vendor);
  }

  updateVendor(vendor: Vendor): Observable<void> {
    return this.http.put<void>(this.baseUrl, vendor);
  }

  deleteVendor(id: string): Observable<void> {
    return this.http.delete<void>(this.baseUrl, { params: { id } });
  }
}

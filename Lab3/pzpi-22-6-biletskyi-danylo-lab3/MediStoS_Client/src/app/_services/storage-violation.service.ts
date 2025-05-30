import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { StorageViolation } from '../_models/storageViolation';

@Injectable({
  providedIn: 'root'
})
export class StorageViolationService {
  private baseUrl = environment.apiUrl;
  private http = inject(HttpClient);

  getViolations(warehouseId: number){
    return this.http.get<StorageViolation[]>(`${this.baseUrl}/storageViolation/${warehouseId}/Violations`);
  }

  constructor() { }
}

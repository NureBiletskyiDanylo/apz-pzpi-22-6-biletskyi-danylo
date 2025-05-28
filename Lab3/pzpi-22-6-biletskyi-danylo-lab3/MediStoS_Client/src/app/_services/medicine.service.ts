import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { MedicineModel } from '../_models/medicineModel';
import { MedicineCreateModel } from '../_models/medicineCreateModel';

@Injectable({
  providedIn: 'root'
})
export class MedicineService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  getMedicine(id: number){
    return this.http.get<MedicineModel>(`${this.baseUrl}/medicine/${id}`);
  }  

  createMedicine(medicine: MedicineCreateModel){
    return this.http.post(`${this.baseUrl}/medicine`, medicine);
  }

  deleteMedicine(id: number){
    return this.http.delete(`${this.baseUrl}/medicine/${id}`);
  }

  editMedicine(medicine: MedicineModel){
    return this.http.put(`${this.baseUrl}/medicine/edit`, medicine);
  }

  getMedicines(){
    return this.http.get<MedicineModel[]>(`${this.baseUrl}/medicine`);
  }

  getMedicineWithBatches(id: number){
    return this.http.get<MedicineModel>(`${this.baseUrl}/medicine/${id}-with`);  
  }

  getWarehouseLocation(id: number){
    return this.http.get<number>(`${this.baseUrl}/batch/locate-warehouse/${id}`);
  }
  constructor() { }
}

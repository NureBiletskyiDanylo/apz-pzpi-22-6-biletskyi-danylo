import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { WarehouseModel } from '../_models/warehouseModel';
import { WarehouseCreateModel } from '../_models/warehouseCreateModel';
import { BatchModel } from '../_models/batchModel';
import { catchError, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {

  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  getWarehouse(id: number){
    return this.http.get<WarehouseModel>(`${this.baseUrl}/warehouse/${id}`);
  }

  createWarehouse(warehouse: WarehouseCreateModel){
    return this.http.post(`${this.baseUrl}/warehouse`, warehouse);
  }

  deleteWarehouse(id: number){
    return this.http.delete(`${this.baseUrl}/warehouse/${id}`);
  }

  editWarehouse(warehouse: WarehouseModel){
    return this.http.put(`${this.baseUrl}/warehouse/edit`, warehouse);
  }

  getWarehouses(){
    return this.http.get<WarehouseModel[]>(`${this.baseUrl}/warehouse`);
  }

  getBatchesForWarehouse(id:number){
    return this.http.get<BatchModel[]>(`${this.baseUrl}/batch/${id}/batches-by-warehouse`);
  }

  getBatchesCountForWarehouse(id: number) {
    return this.http.get<number>(`${this.baseUrl}/batch/${id}/batch-count`);
  }

  deleteBatch(id: number){
    return this.http.delete(`${this.baseUrl}/batch/${id}`);
  }

getMedicineIdByBatchId(batchId: number): Observable<number> {
  return this.http.get<number>(`${this.baseUrl}/batch/${batchId}/medicine-id`).pipe(
    catchError(err => {
      // Convert HTTP error to application error
      if (err.status === 404) {
        throw new Error('Batch not found');
      }
      throw new Error('Server error');
    })
  );
}
}

import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BatchModel } from '../_models/batchModel';
import { BatchCreateModel } from '../_models/batchCreateModel';

@Injectable({
  providedIn: 'root'
})
export class BatchService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  getBatch(id: number) {
    return this.http.get<any>(`${this.baseUrl}/batch/${id}`);
  }

  updateBatch(batch: BatchModel) {
    return this.http.put(`${this.baseUrl}/batch/edit`, batch);
  }

  createBatch(batch: BatchCreateModel){
    return this.http.post(`${this.baseUrl}/batch`, batch);
  }
}

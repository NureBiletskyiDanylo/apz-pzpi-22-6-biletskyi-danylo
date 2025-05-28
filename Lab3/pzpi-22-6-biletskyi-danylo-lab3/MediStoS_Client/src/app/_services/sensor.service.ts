import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { SensorCreateModel } from '../_models/sensorCreateModel';
import { SensorModel } from '../_models/sensorModel';

@Injectable({
  providedIn: 'root'
})
export class SensorService {
  private baseUrl = environment.apiUrl;
  private http = inject(HttpClient);

  getSensor(id: number){
    return this.http.get(`${this.baseUrl}/sensor/${id}`);
  }

  createSensor(sensor: SensorCreateModel) {
    return this.http.post(`${this.baseUrl}/sensor`, sensor);
  }

  deleteSensor(id: number) {
    return this.http.delete(`${this.baseUrl}/sensor/${id}`);
  }

  updateSensor(sensor: SensorModel){
    return this.http.put(`${this.baseUrl}/sensor/${sensor.id}`, sensor);
  }

  getSensors(warehouseId: number) {
    return this.http.get<SensorModel[]>(`${this.baseUrl}/sensor/${warehouseId}/Sensors`);
  }

  getSensorsCountForWarehouse(warehouseId: number){
    return this.http.get<number>(`${this.baseUrl}/sensor/${warehouseId}/count`);
  }
  constructor() { }
}

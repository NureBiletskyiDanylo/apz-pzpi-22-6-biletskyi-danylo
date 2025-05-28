import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Log } from '../_models/log';

@Injectable({
  providedIn: 'root'
})
export class LoggingService {
  private baseUrl = environment.apiUrl;
  private http = inject(HttpClient);

  getLogs() {
    return this.http.get<Log[]>(`${this.baseUrl}/server/logs`);
  }
  constructor() { }
}

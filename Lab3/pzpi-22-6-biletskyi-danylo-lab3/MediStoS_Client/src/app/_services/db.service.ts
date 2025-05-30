import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DbService {
  private baseApiUrl = environment.apiUrl;
  private http = inject(HttpClient);

  restore(){
    return this.http.post(`${this.baseApiUrl}/database/Restore`, {});
  }

  backup(){
    return this.http.post(`${this.baseApiUrl}/database/Backup`, {});
  }
}

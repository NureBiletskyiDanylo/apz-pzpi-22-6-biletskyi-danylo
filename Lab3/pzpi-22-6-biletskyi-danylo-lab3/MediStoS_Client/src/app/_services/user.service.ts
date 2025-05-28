import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  constructor() { }
  getMembers() {
    return this.http.get<Member[]>(`${this.baseUrl}/user/users`);
  }

updateUserRole(id: number, role: string) {
  return this.http.post(
    `${this.baseUrl}/user/update-role/${id}`,
    { role: role }  // HttpClient auto-converts to JSON
  );
}

  updateUser(userModel: Member){
    return this.http.put(`${this.baseUrl}/user/edit`, userModel);
  }

  deleteUser(id: number) {
    return this.http.delete(`${this.baseUrl}/user/${id}`);
  }
}

import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { User } from '../_models/user';
import { map, of } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject (HttpClient);
  private router = inject(Router);
  baseUrl = environment.apiUrl;
  currentUser = signal<User | null>(null);

  login(model: any){
    return this.http.post<User>(this.baseUrl + '/auth/login', model).pipe(
      map(user => {
        console.log(user);
        if (user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
      })
    )
  }

  register(model: any){
    return this.http.post<User>(this.baseUrl + '/auth/register', model).pipe(
      map(user => {
        if (user){
          console.log(user);
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
      })
    )
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }

}

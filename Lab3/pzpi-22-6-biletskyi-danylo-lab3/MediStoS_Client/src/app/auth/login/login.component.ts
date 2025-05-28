import { Component, inject } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginModel } from '../../_models/loginModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  private router: Router = inject(Router);
  private accountService : AccountService = inject(AccountService);
  loginForm = new FormGroup({
    email: new FormControl('',
      [Validators.required, Validators.email]
    ),
    password: new FormControl('', [
      Validators.minLength(8),
      Validators.required
    ])
  })
  login(){
    const loginModel: LoginModel = {
      email: this.loginForm.value.email!,
      password: this.loginForm.value.password!
    };

    this.accountService.login(loginModel).subscribe({
      next: (() => {
        this.router.navigate(["/"]);
      })
    })
  }
}

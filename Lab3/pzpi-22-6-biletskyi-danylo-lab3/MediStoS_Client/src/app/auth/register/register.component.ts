import { Component, inject } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { RegisterModel } from '../../_models/registerModel';
import { AccountService } from '../../_services/account.service';
import { Router } from '@angular/router';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService);
  private router = inject(Router);
 registerForm = new FormGroup({
  firstname: new FormControl('', [
    Validators.required,
    Validators.minLength(2)
  ]),
  lastname: new FormControl('', [
    Validators.required,
    Validators.minLength(2)
  ]),
  email: new FormControl('', [
    Validators.required,
    Validators.email
  ]),
  password: new FormControl('', [
    Validators.required,
    Validators.minLength(8)
  ]),
  confirmPassword: new FormControl('', [
    Validators.required,
    Validators.minLength(8),
    this.matchValues('password')
  ])
 })

 matchValues(matchTo: string): ValidatorFn {
  return (control: AbstractControl) => {
    return control.value === control.parent?.get(matchTo)?.value ? null : {isMatching: true}
  }
 }

 register () {
  if (this.registerForm.value.password! !== this.registerForm.value.confirmPassword!){
    return;
  }
  const registerModel: RegisterModel = {
    firstname: this.registerForm.value.firstname!,
    lastname: this.registerForm.value.lastname!,
    email: this.registerForm.value.email!,
    password: this.registerForm.value.password!
  }
  console.log(registerModel);
  this.accountService.register(registerModel).subscribe({
    next: (() => {
      this.router.navigate(["/"]);
    })
  })
 }
}

import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MedicineService } from '../../_services/medicine.service';
import { ToastrService } from 'ngx-toastr';
import { MedicineCreateModel } from '../../_models/medicineCreateModel';

@Component({
  selector: 'app-medicine-create',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './medicine-create.component.html',
  styleUrl: './medicine-create.component.css'
})
export class MedicineCreateComponent {
  private fb = inject(FormBuilder);
  private medicineService = inject(MedicineService);
  router = inject(Router);
  private toastr = inject(ToastrService);

  medicineForm = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(100)]],
    manufacturer: ['', [Validators.required, Validators.maxLength(100)]],
    description: ['', Validators.maxLength(500)],
    min_temperature: [null, [Validators.required, Validators.min(-50), Validators.max(50)]],
    max_temperature: [null, [Validators.required, Validators.min(-50), Validators.max(50)]],
    min_humidity: [null, [Validators.required, Validators.min(0), Validators.max(100)]],
    max_humidity: [null, [Validators.required, Validators.min(0), Validators.max(100)]]
  });

  get f() { return this.medicineForm.controls; }

  onSubmit() {
    if (this.medicineForm.invalid) {
      this.medicineForm.markAllAsTouched();
      return;
    }

    const formValue = this.medicineForm.value;
    const medicine: MedicineCreateModel = {
      name: formValue.name ?? '',
      manufacturer: formValue.manufacturer ?? '',
      description: formValue.description ?? '',
      min_temperature: formValue.min_temperature ?? 0,
      max_temperature: formValue.max_temperature ?? 0,
      min_humidity: formValue.min_humidity ?? 0,
      max_humidity: formValue.max_humidity ?? 0
    };

    this.medicineService.createMedicine(medicine)
      .subscribe({
        next: () => {
          this.toastr.success('Medicine created successfully');
          this.router.navigate(['/medicine']);
        },
        error: (error) => {
          this.toastr.error('Failed to create medicine');
          console.error('Error creating medicine:', error);
        }
      });
  }
}

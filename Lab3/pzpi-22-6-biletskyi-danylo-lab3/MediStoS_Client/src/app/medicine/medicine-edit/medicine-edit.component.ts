import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MedicineService } from '../../_services/medicine.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { MedicineModel } from '../../_models/medicineModel';

@Component({
  selector: 'app-medicine-edit',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './medicine-edit.component.html',
  styleUrl: './medicine-edit.component.css'
})
export class MedicineEditComponent implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  router = inject(Router);
  private medicineService = inject(MedicineService);
  private toastr = inject(ToastrService);

  medicineForm = this.fb.group({
    id: [0, Validators.required],
    name: ['', [Validators.required, Validators.maxLength(100)]],
    manufacturer: ['', [Validators.required, Validators.maxLength(100)]],
    description: ['', Validators.maxLength(500)],
    min_temperature: [0, [Validators.required, Validators.min(-50), Validators.max(50)]],
    max_temperature: [0, [Validators.required, Validators.min(-50), Validators.max(50)]],
    min_humidity: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
    max_humidity: [0, [Validators.required, Validators.min(0), Validators.max(100)]]
  });

  get f() { return this.medicineForm.controls; }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.medicineService.getMedicine(+id).subscribe({
        next: (medicine: MedicineModel) => {
          this.medicineForm.patchValue({
            id: medicine.id,
            name: medicine.name,
            manufacturer: medicine.manufacturer,
            description: medicine.description,
            min_temperature: Number(medicine.min_temperature),
            max_temperature: Number(medicine.max_temperature),
            min_humidity: Number(medicine.min_humidity),
            max_humidity: Number(medicine.max_humidity)
          });
        },
        error: () => {
          this.toastr.error('Medicine not found');
          this.router.navigate(['/medicines']);
        }
      });
    }
  }

  onSubmit() {
    if (this.medicineForm.invalid) {
      this.medicineForm.markAllAsTouched();
      return;
    }

    const formValue = this.medicineForm.value;
    const medicine: MedicineModel = {
      id: Number(formValue.id),
      name: formValue.name ?? '',
      manufacturer: formValue.manufacturer ?? '',
      description: formValue.description ?? '',
      min_temperature: Number(formValue.min_temperature),
      max_temperature: Number(formValue.max_temperature),
      min_humidity: Number(formValue.min_humidity),
      max_humidity: Number(formValue.max_humidity),
      batches: []
    };

    this.medicineService.editMedicine(medicine).subscribe({
      next: () => {
        this.toastr.success('Medicine updated successfully');
        this.router.navigate(['/medicine']);
      },
      error: (error) => {
        this.toastr.error('Failed to update medicine');
        console.error('Error updating medicine:', error);
      }
    });
  }
}


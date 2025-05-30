import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { WarehouseService } from '../../_services/warehouse.service';
import { WarehouseCreateModel } from '../../_models/warehouseCreateModel';
import { ToastrService } from 'ngx-toastr';
import { CommonModule, NgIf } from '@angular/common';

@Component({
  selector: 'app-warehouse-create',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule, CommonModule],
  templateUrl: './warehouse-create.component.html',
  styleUrl: './warehouse-create.component.css'
})
export class WarehouseCreateComponent {
  private fb = inject(FormBuilder);
  private warehouseService = inject(WarehouseService);
  router = inject(Router);
  private toastService = inject(ToastrService);

  form = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(100)]],
    address: ['', [Validators.required, Validators.maxLength(200)]],
    minTemperature: [null, [Validators.required, Validators.min(-50), Validators.max(50)]],
    maxTemperature: [null, [Validators.required, Validators.min(-50), Validators.max(50)]],
    minHumidity: [null, [Validators.required, Validators.min(0), Validators.max(100)]],
    maxHumidity: [null, [Validators.required, Validators.min(0), Validators.max(100)]]
  });

  get f() { return this.form.controls; }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const formValue = this.form.value;
    console.log('Form submitted with value:', formValue);
    const warehouse: WarehouseCreateModel = {
      name: formValue.name ?? '',
      address: formValue.address ?? '',
      min_temperature: formValue.minTemperature ?? 0,
      max_temperature: formValue.maxTemperature ?? 0,
      min_humidity: formValue.minHumidity ?? 0,
      max_humidity: formValue.maxHumidity ?? 0
    };

    this.warehouseService.createWarehouse(warehouse)
      .subscribe({
        next: () => {
          this.toastService.success('Warehouse created successfully');
          this.router.navigate(['/warehouse']);
        },
        error: (error) => {
          this.toastService.error('Failed to create warehouse');
          console.error('Error creating warehouse:', error);
        }
      });
  }
}

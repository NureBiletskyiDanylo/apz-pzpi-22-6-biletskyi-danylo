import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MedicineModel } from '../../_models/medicineModel';
import { WarehouseModel } from '../../_models/warehouseModel';
import { AccountService } from '../../_services/account.service';
import { BatchService } from '../../_services/batch.service';
import { MedicineService } from '../../_services/medicine.service';
import { WarehouseService } from '../../_services/warehouse.service';

@Component({
  selector: 'app-batch-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './batch-create.component.html',
  styleUrl: './batch-create.component.css'
})
export class BatchCreateComponent implements OnInit {
  private fb = inject(FormBuilder);
  router = inject(Router);
  private batchService = inject(BatchService);
  private warehouseService = inject(WarehouseService);
  private medicineService = inject(MedicineService);
  private accountService = inject(AccountService);
  private toastr = inject(ToastrService);
  private route = inject(ActivatedRoute);

  batchForm!: FormGroup;
  warehouses: WarehouseModel[] = [];
  medicines: MedicineModel[] = [];
  isSubmitting = false;
  isLoading = false;

  medicineIdFromQuery: string | null = null;
  warehouseIdFromQuery: string | null = null;


  ngOnInit(): void {
    this.initializeForm();
    this.loadWarehouses();
    this.loadMedicines();

this.route.queryParams.subscribe(params => {
    this.medicineIdFromQuery = params['medicineId'] || null;
    this.warehouseIdFromQuery = params['warehouseId'] || null;

    if (this.medicineIdFromQuery) {
      this.batchForm.patchValue({ medicine_id: this.medicineIdFromQuery });
      this.batchForm.get('medicine_id')?.disable();
    }

    if (this.warehouseIdFromQuery) {
      this.batchForm.patchValue({ warehouse_id: this.warehouseIdFromQuery });
      this.batchForm.get('warehouse_id')?.disable();
    }
  });
  }

  initializeForm(): void {
    this.batchForm = this.fb.group({
      batch_number: ['', [
        Validators.required,
        Validators.maxLength(50)
      ]],
      quantity: [1, [
        Validators.required,
        Validators.min(1),
        Validators.max(1000000)
      ]],
      manufacture_date: ['', Validators.required],
      expiration_date: ['', Validators.required],
      warehouse_id: ['', Validators.required],
      medicine_id: ['', Validators.required]
    });

    // Add this to your form initialization
this.batchForm.setValidators([
  this.validateDateRange
]);
  }

  // Add this method to your component
validateDateRange(control: import('@angular/forms').AbstractControl): ValidationErrors | null {
  const group = control as FormGroup;
  const manufactureDate = new Date(group.get('manufacture_date')?.value);
  const expirationDate = new Date(group.get('expiration_date')?.value);

  if (manufactureDate && expirationDate && manufactureDate > expirationDate) {
    return { dateRangeInvalid: true };
  }
  return null;
}

  loadWarehouses(): void {
    this.isLoading = true;
    this.warehouseService.getWarehouses().subscribe({
      next: (warehouses) => {
        this.warehouses = warehouses;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
        this.toastr.error('Failed to load warehouses');
      }
    });
  }

  loadMedicines(): void {
    this.isLoading = true;
    this.medicineService.getMedicines().subscribe({
      next: (medicines) => {
        this.medicines = medicines;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
        this.toastr.error('Failed to load medicines');
      }
    });
  }

onSubmit(): void {
  if (this.batchForm.invalid) {
    this.batchForm.markAllAsTouched();
    return;
  }

  this.isSubmitting = true;
  const currentUser = this.accountService.currentUser();

  const batchData = {
    ...this.batchForm.getRawValue(),
    user_id: currentUser?.id
  };

  this.batchService.createBatch(batchData).subscribe({
    next: () => {
      this.toastr.success('Batch created successfully');

      // Navigate based on origin
      if (this.medicineIdFromQuery) {
        this.router.navigate(['/medicine/view', this.medicineIdFromQuery]); // adjust route if needed
      } else if (this.warehouseIdFromQuery) {
        this.router.navigate(['/warehouse/view', this.warehouseIdFromQuery]); // adjust route if needed
      } else {
        this.router.navigate(['/']); // fallback/default route
      }
    },
    error: () => {
      this.isSubmitting = false;
      this.toastr.error('Failed to create batch');
    }
  });
}

  formatDateForInput(date: Date): string {
    const d = new Date(date);
    return d.toISOString().split('T')[0];
  }
}

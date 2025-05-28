import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { WarehouseService } from '../../_services/warehouse.service';
import { WarehouseModel } from '../../_models/warehouseModel';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-warehouse-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './warehouse-edit.component.html',
  styleUrl: './warehouse-edit.component.css'
})
export class WarehouseEditComponent implements OnInit {
  warehouseForm = this.fb.group({
    id: [0, Validators.required],
    name: ['', [Validators.required, Validators.maxLength(100)]],
    address: ['', [Validators.required, Validators.maxLength(200)]],
    min_temperature: [0, [Validators.required, Validators.min(-50), Validators.max(50)]],
    max_temperature: [0, [Validators.required, Validators.min(-50), Validators.max(50)]],
    min_humidity: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
    max_humidity: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
    created_at: [new Date()]
  });

  router: Router = inject(Router)
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private warehouseService: WarehouseService
  ) {}

  ngOnInit(): void {
    const warehouse = history.state.warehouse;
    
    if (warehouse) {
      this.warehouseForm.patchValue({
        ...warehouse,
        // Ensure proper types for form controls
        min_temperature: +warehouse.min_temperature,
        max_temperature: +warehouse.max_temperature,
        min_humidity: +warehouse.min_humidity,
        max_humidity: +warehouse.max_humidity
      });
    } else {
      const id = this.route.snapshot.paramMap.get('id');
      if (id) {
        this.warehouseService.getWarehouse(+id).subscribe(w => {
          this.warehouseForm.patchValue({
            ...w,
            min_temperature: +w.min_temperature,
            max_temperature: +w.max_temperature,
            min_humidity: +w.min_humidity,
            max_humidity: +w.max_humidity
          });
        });
      }
    }
  }

  onSubmit() {
    if (this.warehouseForm.valid) {
      this.warehouseService.editWarehouse(this.warehouseForm.value as WarehouseModel)
        .subscribe({
          next: () => {
            this.router.navigate(['/warehouses']);
            // Optional: Add toast notification here
          },
          error: (error) => {
            console.error('Error updating warehouse:', error);
            // Optional: Show error message
          }
        });
    }
  }
}
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BatchModel } from '../../_models/batchModel';
import { MedicineModel } from '../../_models/medicineModel';
import { MedicineService } from '../../_services/medicine.service';
import { NgFor, NgIf } from '@angular/common';
import {NgbDropdownModule} from '@ng-bootstrap/ng-bootstrap'

@Component({
  selector: 'app-medicine-view',
  standalone: true,
  imports: [NgIf, NgFor, NgbDropdownModule],
  templateUrl: './medicine-view.component.html',
  styleUrl: './medicine-view.component.css'
})
export class MedicineViewComponent implements OnInit {
  private route = inject(ActivatedRoute);
  router = inject(Router);
  private medicineService = inject(MedicineService);
  private toastr = inject(ToastrService);

  medicine: MedicineModel | null = null;
  selectedBatch: BatchModel | null = null;
  isBatchDropdownOpen = false;

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadMedicine(+id);
    }
  }

  loadMedicine(id: number): void {
    this.medicineService.getMedicineWithBatches(id).subscribe({
      next: (medicine) => {
        this.medicine = medicine;
        if (medicine.batches && medicine.batches.length > 0) {
          this.selectedBatch = medicine.batches[0];
        }
      },
      error: () => {
        this.toastr.error('Medicine not found');
        this.router.navigate(['/medicine']);
      }
    });
  }

  toggleBatchDropdown(): void {
    this.isBatchDropdownOpen = !this.isBatchDropdownOpen;
  }

  selectBatch(batch: BatchModel): void {
    console.log(this.medicine?.batches);
    this.selectedBatch = batch;
    this.isBatchDropdownOpen = false;
  }

  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString();
  }

  getWarehouseLocation() {
    if (this.selectedBatch){
      this.medicineService.getWarehouseLocation(this.selectedBatch.id).subscribe({
        next: (location) => {
          this.router.navigate(['/warehouse/view', location]);
        },
        error: () => {
          this.toastr.error('Failed to retrieve warehouse location');
        }
      });
    }
  }

  createBatchForMedicine(): void {
  if (this.medicine) {
    this.router.navigate(['/batch/create'], {
      queryParams: { medicineId: this.medicine.id }
    });
  }
}
}

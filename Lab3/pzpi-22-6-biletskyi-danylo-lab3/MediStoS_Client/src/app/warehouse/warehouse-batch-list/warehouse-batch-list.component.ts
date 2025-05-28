import { trigger, state, style, transition, animate } from '@angular/animations';
import { CommonModule } from '@angular/common';
import { Component, inject, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { BatchModel } from '../../_models/batchModel';
import { WarehouseService } from '../../_services/warehouse.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-warehouse-batch-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './warehouse-batch-list.component.html',
  styleUrl: './warehouse-batch-list.component.css',
  animations: [
  trigger('expandCollapse', [
      state('collapsed', style({
        height: '0',
        opacity: '0',
        overflow: 'hidden'
      })),
      state('expanded', style({
        height: '*',
        opacity: '1'
      })),
      transition('collapsed <=> expanded', [
        animate('300ms ease-in-out')
      ])
    ])
  ]
})
export class WarehouseBatchListComponent implements OnInit, OnChanges {
  @Input() warehouseId!: number;
  batches: BatchModel[] = [];
  batchCount: number | null = null;
  isExpanded = false;
  isLoading = false;
  isCountLoading = false;
  router = inject(Router);
  private toastr = inject(ToastrService);
  isViewingMedicine: boolean = false;

  constructor(private warehouseService: WarehouseService) {}
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['warehouseId'] && changes['warehouseId'].currentValue) {
      this.loadBatchCount();
      if (this.isExpanded) {
        this.loadBatches();
      }
    }
  }

  ngOnInit(): void {
    this.loadBatchCount();
    if (this.isExpanded) {
      this.loadBatches();
    }
  }

  loadBatchCount(): void {
    if (!this.warehouseId) return;
    
    this.isCountLoading = true;
    this.warehouseService.getBatchesCountForWarehouse(this.warehouseId).subscribe({
      next: (count) => {
        this.batchCount = count;
        this.isCountLoading = false;
      },
      error: () => {
        this.isCountLoading = false;
      }
    });
  }

  toggleList(): void {
    this.isExpanded = !this.isExpanded;
    if (this.isExpanded && this.batches.length === 0) {
      this.loadBatches();
    }
  }

  loadBatches(): void {
    if (!this.warehouseId) return;
    
    this.isLoading = true;
    this.warehouseService.getBatchesForWarehouse(this.warehouseId).subscribe({
      next: (batches) => {
        this.batches = batches;
        this.batchCount = batches.length;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }

  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString();
  }

  editBatch(batch: BatchModel): void {
    this.router.navigate(['/batch/edit', batch.id]);
  }

viewMedicine(batch: BatchModel): void {
  this.isViewingMedicine = true;
  
  this.warehouseService.getMedicineIdByBatchId(batch.id).subscribe({
    next: (response) => {
      this.isViewingMedicine = false;
      
      // Handle different response types
      const medicineId = typeof response === 'object' 
        ? (response as any)?.medicineId || (response as any)?.id
        : response;

      if (typeof medicineId === 'number' && !isNaN(medicineId)) {
        this.router.navigate(['/medicine/view', medicineId]);
      } else {
        console.error('Invalid medicine ID format:', response);
        this.toastr.error('Received invalid medicine ID from server');
      }
    },
    error: (err) => {
      this.isViewingMedicine = false;
      console.error('Error fetching medicine ID:', err);
      
      const errorMessage = err.error?.message 
        || err.message 
        || 'Failed to retrieve medicine ID';
        
      this.toastr.error(errorMessage);
    }
  });
}

  deleteBatch(batch: BatchModel): void {
    if (confirm(`Are you sure you want to delete batch ${batch.batch_number}?`)) {
      this.warehouseService.deleteBatch(batch.id).subscribe({
        next: () => {
          this.toastr.success('Batch deleted successfully');
          // Refresh the list
          this.batches = this.batches.filter(b => b.id !== batch.id);
          this.batchCount = this.batches.length;
        },
        error: () => {
          this.toastr.error('Failed to delete batch');
        }
      });
    }
  }
}

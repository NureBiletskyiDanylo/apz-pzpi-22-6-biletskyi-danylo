import { Component, inject, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { StorageViolation } from '../../_models/storageViolation';
import { WarehouseService } from '../../_services/warehouse.service';
import { StorageViolationService } from '../../_services/storage-violation.service';
import { CommonModule, NgFor } from '@angular/common';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-violation-list',
  standalone: true,
  imports: [NgFor, CommonModule],
  templateUrl: './violation-list.component.html',
  styleUrl: './violation-list.component.css',
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
export class ViolationListComponent implements OnInit, OnChanges {
  @Input() warehouseId!: number;
  violations: StorageViolation[] = [];
  violationCount: number | null = null;
  isExpanded = false;
  isLoading = false;
  isCountLoading = false;
  private storageViolation = inject(StorageViolationService);
  private toastr = inject(ToastrService);

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['warehouseId'] && changes['warehouseId'].currentValue) {
      if (this.isExpanded) {
        this.loadViolations();
      }
    }
  }

  ngOnInit(): void {
    if (this.isExpanded) {
      this.loadViolations();
    }
  }


  toggleList(): void {
    this.isExpanded = !this.isExpanded;
    if (this.isExpanded && this.violations.length === 0) {
      this.loadViolations();
    }
  }

  loadViolations(): void {
    if (!this.warehouseId) return;
    
    this.isLoading = true;
    this.storageViolation.getViolations(this.warehouseId).subscribe({
      next: (violations) => {
        this.violations = violations;
        this.violationCount = violations.length;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
        this.toastr.error('Failed to load violations');
      }
    });
  }

  formatDateTime(date: Date): string {
    return new Date(date).toLocaleString();
  }
}

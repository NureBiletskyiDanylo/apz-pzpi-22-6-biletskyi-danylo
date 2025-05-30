import { CommonModule, NgIf } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BatchModel } from '../../_models/batchModel';
import { BatchService } from '../../_services/batch.service';

@Component({
  selector: 'app-batch-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgIf],
  templateUrl: './batch-edit.component.html',
  styleUrl: './batch-edit.component.css'
})
export class BatchEditComponent implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  router = inject(Router);
  private batchService = inject(BatchService);
  private toastr = inject(ToastrService);

  batchForm!: FormGroup;
  batchId!: number;
  originalBatch!: BatchModel;
  isSubmitting = false;

  ngOnInit(): void {
    this.batchId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadBatch();
  }

  loadBatch(): void {
    this.batchService.getBatch(this.batchId).subscribe({
      next: (batch) => {
        this.originalBatch = batch;
        this.initializeForm(batch);
      },
      error: () => {
        this.toastr.error('Failed to load batch');
        this.router.navigate(['/batches']);
      }
    });
  }

  initializeForm(batch: BatchModel): void {
    this.batchForm = this.fb.group({
      batch_number: [batch.batch_number, [
        Validators.required,
        Validators.maxLength(50)
      ]],
      quantity: [batch.quantity, [
        Validators.required,
        Validators.min(1),
        Validators.max(1000000)
      ]],
      manufacture_date: [this.formatDateForInput(batch.manufacture_date), [
        Validators.required
      ]],
      expiration_date: [this.formatDateForInput(batch.expiration_date), [
        Validators.required
      ]],
      email: [{ value: batch.email, disabled: true }]
    });
  }

  formatDateForInput(date: Date): string {
    const d = new Date(date);
    return d.toISOString().split('T')[0];
  }

  onSubmit(): void {
    if (this.batchForm.invalid) {
      this.batchForm.markAllAsTouched();
      return;
    }
    this.isSubmitting = true;
    const updatedBatch: BatchModel = {
      ...this.originalBatch,
      ...this.batchForm.value,
      email: this.originalBatch.email
    };

    this.batchService.updateBatch(updatedBatch).subscribe({
      next: () => {
        this.toastr.success('Batch updated successfully');
        this.router.navigate(['/']);
      },
      error: () => {
        this.isSubmitting = false;
        this.toastr.error('Failed to update batch');
      }
    });
  }
}

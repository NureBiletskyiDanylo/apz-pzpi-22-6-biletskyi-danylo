import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SensorCreateModel } from '../../_models/sensorCreateModel';
import { SensorService } from '../../_services/sensor.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-sensor-create',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sensor-create.component.html',
  styleUrl: './sensor-create.component.css'
})
export class SensorCreateComponent implements OnInit {
  sensor: SensorCreateModel = {
    type: '',
    serial_number: '',
    warehouse_id: 0
  };
  isSubmitting = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private sensorService: SensorService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    const warehouseId = this.route.snapshot.paramMap.get('warehouseId');
    if (!warehouseId) {
      this.toastr.error('No warehouse specified');
      this.router.navigate(['/warehouse']);
      return;
    }
    this.sensor.warehouse_id = +warehouseId;
  }

  onSubmit(): void {
    if (!this.sensor.type || !this.sensor.serial_number) {
      this.toastr.warning('Please fill all required fields');
      return;
    }

    this.isSubmitting = true;
    this.sensorService.createSensor(this.sensor).subscribe({
      next: () => {
        this.toastr.success('Sensor created successfully');
        this.router.navigate(['/warehouse/view', this.sensor.warehouse_id]);
      },
      error: (err) => {
        this.isSubmitting = false;
        const errorMessage = err.error?.message || 'Failed to create sensor';
        this.toastr.error(errorMessage);
      }
    });
  }

  onCancel(): void {
    this.router.navigate(['/warehouse/view', this.sensor.warehouse_id]);
  }
}



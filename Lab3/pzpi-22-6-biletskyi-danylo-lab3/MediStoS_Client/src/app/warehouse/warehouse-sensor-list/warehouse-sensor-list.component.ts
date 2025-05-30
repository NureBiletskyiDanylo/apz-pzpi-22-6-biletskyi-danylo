import { CommonModule } from '@angular/common';
import { Component, inject, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SensorModel } from '../../_models/sensorModel';
import { SensorService } from '../../_services/sensor.service';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { timer, switchMap, Subscription } from 'rxjs';

@Component({
  selector: 'app-warehouse-sensor-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './warehouse-sensor-list.component.html',
  styleUrl: './warehouse-sensor-list.component.css',
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
    ]),
    trigger('valueUpdate', [
      transition('* <=> *', [
        style({ transform: 'scale(1.1)' }),
        animate('500ms ease-out', style({ transform: 'scale(1)' }))
      ])
    ])
  ]
})
export class WarehouseSensorListComponent implements OnInit, OnChanges, OnDestroy {
  @Input() warehouseId!: number;
  sensors: SensorModel[] = [];
  sensorCount: number | null = null;
  isExpanded = false;
  isLoading = false;
  isCountLoading = false;
  router = inject(Router);
  private toastr = inject(ToastrService);
  private sensorService = inject(SensorService);
  private refreshSubscription?: Subscription;

  private temperatureThresholds = {
    criticalHigh: 30,
    warningHigh: 25,
    warningLow: 5,
    criticalLow: 0
  };

  private humidityThresholds = {
    criticalHigh: 85,
    warningHigh: 75,
    warningLow: 25,
    criticalLow: 15
  };

  ngOnInit(): void {
    this.loadSensorCount();
    if (this.isExpanded) {
      this.loadSensors();
      this.startAutoRefresh();
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['warehouseId'] && changes['warehouseId'].currentValue) {
      this.loadSensorCount();
      if (this.isExpanded) {
        this.loadSensors();
        this.startAutoRefresh();
      }
    }
  }

  ngOnDestroy(): void {
    this.stopAutoRefresh();
  }

  toggleList(): void {
    this.isExpanded = !this.isExpanded;
    if (this.isExpanded && this.sensors.length === 0) {
      this.loadSensors();
      this.startAutoRefresh();
    } else if (!this.isExpanded) {
      this.stopAutoRefresh();
    }
  }

  updateSensors(newSensors: SensorModel[]): void {
    this.sensors = this.sensors.map(existingSensor => {
      const updatedSensor = newSensors.find(s => s.id === existingSensor.id);
      return updatedSensor || existingSensor;
    });
    
    newSensors.forEach(newSensor => {
      if (!this.sensors.some(s => s.id === newSensor.id)) {
        this.sensors.push(newSensor);
      }
    });
    
    this.sensorCount = this.sensors.length;
  }

  startAutoRefresh(): void {
    this.stopAutoRefresh();
    
    const refreshInterval = 5000 + Math.random() * 5000;
    
    this.refreshSubscription = timer(0, refreshInterval)
      .pipe(
        switchMap(() => this.sensorService.getSensors(this.warehouseId))
      )
      .subscribe({
        next: (sensors) => {
          if (this.isExpanded) {
            this.updateSensors(sensors);
          }
        },
        error: (err) => {
          console.error('Error refreshing sensors:', err);
        }
      });
  }

  stopAutoRefresh(): void {
    if (this.refreshSubscription) {
      this.refreshSubscription.unsubscribe();
      this.refreshSubscription = undefined;
    }
  }

  loadSensorCount(): void {
    if (!this.warehouseId) return;
    
    this.isCountLoading = true;
    this.sensorService.getSensorsCountForWarehouse(this.warehouseId).subscribe({
      next: (count) => {
        this.sensorCount = count;
        this.isCountLoading = false;
      },
      error: () => {
        this.isCountLoading = false;
      }
    });
  }



  loadSensors(): void {
    if (!this.warehouseId) return;
    
    this.isLoading = true;
    this.sensorService.getSensors(this.warehouseId).subscribe({
      next: (sensors) => {
        this.sensors = sensors;
        this.sensorCount = sensors.length;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }

  isSensorCritical(sensor: SensorModel): boolean {
    if (sensor.type === 'Temperature') {
      return sensor.value >= this.temperatureThresholds.criticalHigh || 
             sensor.value <= this.temperatureThresholds.criticalLow;
    } else { // Humidity
      return sensor.value >= this.humidityThresholds.criticalHigh || 
             sensor.value <= this.humidityThresholds.criticalLow;
    }
  }

  isSensorWarning(sensor: SensorModel): boolean {
    if (this.isSensorCritical(sensor)) return false;
    
    if (sensor.type === 'Temperature') {
      return sensor.value >= this.temperatureThresholds.warningHigh || 
             sensor.value <= this.temperatureThresholds.warningLow;
    } else { // Humidity
      return sensor.value >= this.humidityThresholds.warningHigh || 
             sensor.value <= this.humidityThresholds.warningLow;
    }
  }

  getSensorStatus(sensor: SensorModel): string {
    if (this.isSensorCritical(sensor)) {
      return 'Critical';
    } else if (this.isSensorWarning(sensor)) {
      return 'Warning';
    } else {
      return 'Normal';
    }
  }


  deleteSensor(sensor: SensorModel): void {
    if (confirm(`Are you sure you want to delete sensor ${sensor.serial_number}?`)) {
      this.sensorService.deleteSensor(sensor.id).subscribe({
        next: () => {
          this.toastr.success('Sensor deleted successfully');
          this.sensors = this.sensors.filter(s => s.id !== sensor.id);
          this.sensorCount = this.sensors.length;
        },
        error: () => {
          this.toastr.error('Failed to delete sensor');
        }
      });
    }
  }
}


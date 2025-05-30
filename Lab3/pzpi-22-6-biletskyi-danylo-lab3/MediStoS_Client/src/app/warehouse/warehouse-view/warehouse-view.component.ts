import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { WarehouseModel } from '../../_models/warehouseModel';
import { WarehouseService } from '../../_services/warehouse.service';
import { CommonModule } from '@angular/common';
import { WarehouseBatchListComponent } from '../warehouse-batch-list/warehouse-batch-list.component';
import { WarehouseSensorListComponent } from "../warehouse-sensor-list/warehouse-sensor-list.component";
import { ViolationListComponent } from '../../violation/violation-list/violation-list.component';

@Component({
  selector: 'app-warehouse-view',
  standalone: true,
  imports: [CommonModule, WarehouseBatchListComponent, WarehouseSensorListComponent, ViolationListComponent],
  templateUrl: './warehouse-view.component.html',
  styleUrl: './warehouse-view.component.css'
})
export class WarehouseViewComponent implements OnInit {
  private route = inject(ActivatedRoute);
  router = inject(Router);
  private warehouseService = inject(WarehouseService);
  private toastr = inject(ToastrService);

  warehouse: WarehouseModel | null = null;
  isLoading = true;

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadWarehouse(+id);
    }
  }

  loadWarehouse(id: number): void {
    this.warehouseService.getWarehouse(id).subscribe({
      next: (warehouse) => {
        this.warehouse = warehouse;
        this.isLoading = false;
      },
      error: () => {
        this.toastr.error('Warehouse not found');
        this.router.navigate(['/warehouses']);
      }
    });
  }

  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString();
  }
// Add these methods to your component class
calculateRangePercentage(current: number, min: number, max: number): number {
  return ((current - min) / (max - min)) * 100;
}

getTemperatureStyle(warehouse: WarehouseModel): any {
  const min = -20; // Absolute minimum you expect (adjust as needed)
  const max = 50;  // Absolute maximum you expect (adjust as needed)
  const range = max - min;
  
  return {
    'left.%': ((warehouse.min_temperature - min) / range * 100),
    'width.%': ((warehouse.max_temperature - warehouse.min_temperature) / range * 100)
  };
}

getHumidityStyle(warehouse: WarehouseModel): any {
  return {
    'left.%': warehouse.min_humidity,
    'width.%': (warehouse.max_humidity - warehouse.min_humidity)
  };
}

createBatchForWarehouse(): void {
  if (this.warehouse) {
    this.router.navigate(['/batch/create'], {
      queryParams: { warehouseId: this.warehouse.id }
    });
  }
}



createSensorForWarehouse(): void {
  if (this.warehouse) {
    this.router.navigate(['/sensor/create', this.warehouse.id]);
  }
}

}
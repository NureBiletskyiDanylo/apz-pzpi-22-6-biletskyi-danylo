import { Component, inject, OnInit } from '@angular/core';
import { WarehouseService } from '../../_services/warehouse.service';
import { WarehouseModel } from '../../_models/warehouseModel';
import { NgFor } from '@angular/common';
import { WarehouseCardComponent } from "../warehouse-card/warehouse-card.component";
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-warehouse-list',
  standalone: true,
  imports: [NgFor, WarehouseCardComponent],
  templateUrl: './warehouse-list.component.html',
  styleUrl: './warehouse-list.component.css'
})
export class WarehouseListComponent implements OnInit {
  private warehouseService = inject(WarehouseService);
  private router = inject(Router);
  warehouses: WarehouseModel[] = [];

  ngOnInit(): void {
    this.loadWarehouses();
  }

  loadWarehouses() {
    this.warehouseService.getWarehouses().subscribe({
      next: (warehouses) => {
        this.warehouses = warehouses;
        console.log('Warehouses loaded:', this.warehouses);
      },
      error: (error) => {
        console.error('Error fetching warehouses:', error);
      }
    });
  }

  deleteWarehouse(id: number){
    this.warehouseService.deleteWarehouse(id).subscribe({
      next: () => {
        console.log(`Warehouse with id ${id} deleted successfully.`);
        this.warehouses = this.warehouses.filter(warehouse => warehouse.id !== id);
      },
      error: (error) => {
        console.error(`Error deleting warehouse with id ${id}:`, error);
      }
    });
  }

  editWarehouse(warehouse: WarehouseModel){
    this.router.navigate(['/warehouse/edit', warehouse.id], {
      state: { warehouse: warehouse }
    });
  }

  goToCreateWarehouse(){
    this.router.navigate(['/warehouse/create']);
  }

  viewWarehouse(id: number) {
    this.router.navigate(['/warehouse/view', id]);
  }
  
}

import { Component, EventEmitter, Input, input, Output } from '@angular/core';
import { WarehouseModel } from '../../_models/warehouseModel';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-warehouse-card',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './warehouse-card.component.html',
  styleUrl: './warehouse-card.component.css'
})
export class WarehouseCardComponent {
  @Input() warehouse!: WarehouseModel;
  @Output() deleteWarehouse = new EventEmitter<number>();
  @Output() editWarehouse = new EventEmitter<WarehouseModel>();
  @Output() viewWarehouse = new EventEmitter<number>();

  onDelete(){
    if (confirm ("Do you really want to delete this warehouse?")) {
      this.deleteWarehouse.emit(this.warehouse.id);
    }
  }

  onEdit(){
    this.editWarehouse.emit(this.warehouse);
  }

  onView(){
    this.viewWarehouse.emit(this.warehouse.id);
  }
}

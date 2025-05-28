import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MedicineModel } from '../../_models/medicineModel';

@Component({
  selector: 'app-medicine-card',
  standalone: true,
  imports: [],
  templateUrl: './medicine-card.component.html',
  styleUrl: './medicine-card.component.css'
})
export class MedicineCardComponent {
  @Input() medicine!: MedicineModel;
  @Output() deleteMedicine = new EventEmitter<number>();
  @Output() editMedicine = new EventEmitter<MedicineModel>();
  @Output() viewMedicine = new EventEmitter<number>();

  onDelete() {
    if (confirm("Do you really want to delete this medicine?")) {
      this.deleteMedicine.emit(this.medicine.id);
    }
  }

  onEdit(){
    this.editMedicine.emit(this.medicine);
  }

  onView(){
    this.viewMedicine.emit(this.medicine.id);
  }
}

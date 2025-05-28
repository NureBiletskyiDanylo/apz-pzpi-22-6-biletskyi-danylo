import { Component, inject, OnInit } from '@angular/core';
import { MedicineService } from '../../_services/medicine.service';
import { MedicineModel } from '../../_models/medicineModel';
import { ToastrService } from 'ngx-toastr';
import { NgFor } from '@angular/common';
import { MedicineCardComponent } from "../medicine-card/medicine-card.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-medicine-list',
  standalone: true,
  imports: [NgFor, MedicineCardComponent],
  templateUrl: './medicine-list.component.html',
  styleUrl: './medicine-list.component.css'
})
export class MedicineListComponent implements OnInit {
  private medicineService = inject(MedicineService);
  private toastrService = inject(ToastrService);
  private router = inject(Router);
  medicines: MedicineModel[] = []

  ngOnInit(): void {
    this.loadMedicines();
  }

  loadMedicines() {
    this.medicineService.getMedicines().subscribe({
      next: (medicines) => {
        this.medicines = medicines;
      },
      error: (error) => {
        this.toastrService.error('Error fetching medicines', 'Error');
      }
    });
  }

  deleteMedicine(id: number){
    this.medicineService.deleteMedicine(id).subscribe({
      next: () => {
        this.toastrService.success('Medicine deleted successfully', 'Success');
        this.medicines = this.medicines.filter(medicine => medicine.id !== id);
      },
      error: (error) => {
        this.toastrService.error('Error deleting medicine', 'Error');
      }
    });
  }

  editMedicine(medicine: MedicineModel){
    this.router.navigate(['/medicine/edit', medicine.id], {
      state: { medicine: medicine }
    })
  }

  goToCreateMedicine() {
    this.router.navigate(['/medicine/create']);
  }

  viewMedicine(id: number){
    this.router.navigate(['/medicine/view', id]);
  }

}

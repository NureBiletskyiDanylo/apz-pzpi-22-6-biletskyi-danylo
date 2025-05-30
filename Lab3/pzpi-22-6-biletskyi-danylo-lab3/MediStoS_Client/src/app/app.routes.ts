import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { WarehouseListComponent } from './warehouse/warehouse-list/warehouse-list.component';
import { WarehouseCreateComponent } from './warehouse/warehouse-create/warehouse-create.component';
import { WarehouseEditComponent } from './warehouse/warehouse-edit/warehouse-edit.component';
import { MedicineListComponent } from './medicine/medicine-list/medicine-list.component';
import { MedicineCreateComponent } from './medicine/medicine-create/medicine-create.component';
import { MedicineEditComponent } from './medicine/medicine-edit/medicine-edit.component';
import { MedicineViewComponent } from './medicine/medicine-view/medicine-view.component';
import { WarehouseViewComponent } from './warehouse/warehouse-view/warehouse-view.component';
import { BatchEditComponent } from './batch/batch-edit/batch-edit.component';
import { BatchCreateComponent } from './batch/batch-create/batch-create.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { SensorCreateComponent } from './sensor/sensor-create/sensor-create.component';
import { ViolationListComponent } from './violation/violation-list/violation-list.component';

export const routes: Routes = [
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'warehouse', component: WarehouseListComponent},
    {path: 'warehouse/view/:id', component: WarehouseViewComponent},
    {path: 'warehouse/create', component:WarehouseCreateComponent},
    {path: 'warehouse/edit/:id', component: WarehouseEditComponent},
    {path: 'medicine', component: MedicineListComponent},
    {path: 'medicine/create', component: MedicineCreateComponent},
    {path: 'medicine/edit/:id', component: MedicineEditComponent},
    {path: 'medicine/view/:id', component: MedicineViewComponent},
    {path: 'batch/edit/:id', component: BatchEditComponent},
    {path: 'batch/create', component: BatchCreateComponent},
    {path: 'admin/admin-panel', component: AdminPanelComponent},
    {path: 'sensor/create/:warehouseId', component: SensorCreateComponent},
];

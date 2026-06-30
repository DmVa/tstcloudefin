import { Routes } from '@angular/router';
import { VendorListComponent } from './vendor-list/vendor-list.component';
import { VendorFormComponent } from './vendor-form/vendor-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'vendors', pathMatch: 'full' },
  { path: 'vendors', component: VendorListComponent },
  { path: 'vendors/new', component: VendorFormComponent },
  { path: 'vendors/:id', component: VendorFormComponent }
];

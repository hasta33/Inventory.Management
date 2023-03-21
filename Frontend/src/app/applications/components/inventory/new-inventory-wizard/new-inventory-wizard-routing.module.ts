import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {NewInventoryWizardComponent} from "./new-inventory-wizard.component";

const routes: Routes = [
  { path: '', component: NewInventoryWizardComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NewInventoryWizardRoutingModule { }

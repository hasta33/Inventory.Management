import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'inventory-list', loadChildren: () => import('./inventory-list/inventory-list.module').then(m => m.InventoryListModule)},
  { path: 'new-inventory-wizard', loadChildren: () => import('./new-inventory-wizard/new-inventory-wizard.module').then(m => m.NewInventoryWizardModule)},
  { path: '**', redirectTo: '/notfound'}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InventoryRoutingModule { }

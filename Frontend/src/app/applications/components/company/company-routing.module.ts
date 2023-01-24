import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'company-list', loadChildren: () => import('./company-list/company-list.module').then(m => m.CompanyListModule)},
  { path: '**', redirectTo: '/notfound'}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompanyRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'brand-list', loadChildren: () => import('./brand-list/brand-list.module').then(m => m.BrandListModule) },
  { path: '**', redirectTo: '/notfound'}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BrandRoutingModule { }

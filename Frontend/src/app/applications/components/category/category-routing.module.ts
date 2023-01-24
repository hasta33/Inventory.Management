import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'category-list', loadChildren: () => import('./category-list/category-list.module').then(x => x.CategoryListModule)},
  { path: '**', redirectTo: '/notfound'}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoryRoutingModule { }

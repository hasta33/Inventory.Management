import {NgModule} from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AppLayoutComponent} from "./layout/app-layout/app-layout.component";
import {NotfoundComponent} from "./applications/components/notfound/notfound.component";
import {RoleGuard} from "./applications/shared/role.guard";


const routes: Routes = [
  {
    path: '', component: AppLayoutComponent,
    children: [
      { path: '', loadChildren: () => import('./applications/components/dashboard/dashboard.module').then(m => m.DashboardModule) },
      { path: 'company', loadChildren: () => import('./applications/components/company/company.module').then(m => m.CompanyModule), canActivate: [RoleGuard], data: {roles: ['CompanyRole'] }},
      { path: 'inventory', loadChildren: () => import('./applications/components/inventory/inventory.module').then(m => m.InventoryModule), canActivate: [RoleGuard], data: {roles: ['InventoryRole'] }},
      { path: 'category', loadChildren: () => import('./applications/components/category/category.module').then(m => m.CategoryModule), canActivate: [RoleGuard], data: {roles: ['CategoryRole'] }},
      { path: 'brand', loadChildren: () => import('./applications/components/brand/brand.module').then(m => m.BrandModule), canActivate: [RoleGuard], data: {roles: ['BrandRole'] }},
    ], canActivate: [RoleGuard],  //AuthGuard
  },

  { path: 'auth', loadChildren: () => import('./applications/components/auth/auth.module').then(m => m.AuthModule)},

  { path: 'notfound', component: NotfoundComponent, canActivate: [RoleGuard]}, //AuthGuard
  { path: '**', redirectTo: '/notfound', canActivate: [RoleGuard]} //AuthGuard
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled', onSameUrlNavigation: 'reload'})],
  exports: [RouterModule]
})
export class AppRoutingModule { }

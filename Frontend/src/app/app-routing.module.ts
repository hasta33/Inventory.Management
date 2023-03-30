import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AppLayoutComponent} from "./layout/app-layout/app-layout.component";
import {NotfoundComponent} from "./applications/components/notfound/notfound.component";
import {AuthGuard} from "./applications/shared/auth.guard";
import {RoleGuard} from "./applications/shared/role.guard";

const routes: Routes = [
  {
    path: '', component: AppLayoutComponent,
    children: [
      { path: '', loadChildren: () => import('./applications/components/dashboard/dashboard.module').then(m => m.DashboardModule) },
      { path: 'company', loadChildren: () => import('./applications/components/company/company.module').then(m => m.CompanyModule) },
      { path: 'inventory', loadChildren: () => import('./applications/components/inventory/inventory.module').then(m => m.InventoryModule) },
      { path: 'category', loadChildren: () => import('./applications/components/category/category.module').then(m => m.CategoryModule) },
      { path: 'brand', loadChildren: () => import('./applications/components/brand/brand.module').then(m => m.BrandModule) }
    ], canActivate: [AuthGuard] //AuthGuard
  },

  { path: 'auth', loadChildren: () => import('./applications/components/auth/auth.module').then(m => m.AuthModule)},

  { path: 'notfound', component: NotfoundComponent, canActivate: [AuthGuard]}, //AuthGuard
  { path: '**', redirectTo: '/notfound', canActivate: [AuthGuard]} //AuthGuard
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled', onSameUrlNavigation: 'reload'})],
  exports: [RouterModule]
})
export class AppRoutingModule { }

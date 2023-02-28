import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppLayoutComponent } from './app-layout/app-layout.component';
import { AppFooterComponent } from './app-footer/app-footer.component';
import { AppTopbarComponent } from './app-topbar/app-topbar.component';
import { AppSidebarComponent } from './app-sidebar/app-sidebar.component';
import {RouterOutlet} from "@angular/router";
import {NotfoundComponent} from "../applications/components/notfound/notfound.component";
import {MenubarModule} from "primeng/menubar";
import {InputTextModule} from "primeng/inputtext";



@NgModule({
  declarations: [
    AppLayoutComponent,
    AppFooterComponent,
    AppTopbarComponent,
    AppSidebarComponent,


    NotfoundComponent
  ],
  imports: [
    CommonModule,
    RouterOutlet,
    MenubarModule,

    InputTextModule
  ]
})
export class AppLayoutModule { }

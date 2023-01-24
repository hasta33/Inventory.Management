import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppLayoutComponent } from './app-layout/app-layout.component';
import { AppFooterComponent } from './app-footer/app-footer.component';
import { AppMenuComponent } from './app-menu/app-menu.component';
import { AppSidebarComponent } from './app-sidebar/app-sidebar.component';
import {RouterOutlet} from "@angular/router";
import {NotfoundComponent} from "../applications/components/notfound/notfound.component";
import {MenubarModule} from "primeng/menubar";
import {InputTextModule} from "primeng/inputtext";



@NgModule({
  declarations: [
    AppLayoutComponent,
    AppFooterComponent,
    AppMenuComponent,
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

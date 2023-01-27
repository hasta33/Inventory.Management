import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CompanyListRoutingModule } from './company-list-routing.module';
import { CompanyListComponent } from './company-list.component';
import {TableModule} from "primeng/table";
import {InputTextModule} from "primeng/inputtext";
import {ButtonModule} from "primeng/button";
import {RippleModule} from "primeng/ripple";
import {FormsModule} from "@angular/forms";
import {ToastModule} from "primeng/toast";
import {PaginatorModule} from "primeng/paginator";
import {ContextMenuModule} from "primeng/contextmenu";


@NgModule({
  declarations: [
    CompanyListComponent
  ],
  imports: [
    CommonModule,
    CompanyListRoutingModule,
    TableModule,
    InputTextModule,
    ButtonModule,
    FormsModule,
    ToastModule,
    RippleModule,
    PaginatorModule,
    ContextMenuModule
  ]
})
export class CompanyListModule { }

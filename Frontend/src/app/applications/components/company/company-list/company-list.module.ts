import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CompanyListRoutingModule } from './company-list-routing.module';
import { CompanyListComponent } from './company-list.component';
import {TableModule} from "primeng/table";
import {InputTextModule} from "primeng/inputtext";
import {ButtonModule} from "primeng/button";
import {FormsModule} from "@angular/forms";
import {ToastModule} from "primeng/toast";
import {PaginatorModule} from "primeng/paginator";
import {ContextMenuModule} from "primeng/contextmenu";
import {RippleModule} from "primeng/ripple";
import { AddRowDirective } from '../../../directive/add-row.directive';
import {AutoFocusModule} from 'primeng/autofocus';

@NgModule({
  declarations: [
    CompanyListComponent,
    AddRowDirective
  ],
  imports: [
    CommonModule,
    CompanyListRoutingModule,
    TableModule,
    InputTextModule,
    ButtonModule,
    FormsModule,
    ToastModule,
    PaginatorModule,
    ContextMenuModule,
    RippleModule,
    AutoFocusModule
  ]
})
export class CompanyListModule { }

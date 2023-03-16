import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BrandListRoutingModule } from './brand-list-routing.module';
import { BrandListComponent } from './brand-list.component';
import {ToastModule} from "primeng/toast";
import {TableModule} from "primeng/table";
import {ContextMenuModule} from "primeng/contextmenu";
import {ButtonModule} from "primeng/button";
import {OverlayPanelModule} from "primeng/overlaypanel";
import {InputTextModule} from "primeng/inputtext";
import {RippleModule} from "primeng/ripple";
import {AutoFocusModule} from "primeng/autofocus";
import {FormsModule} from "@angular/forms";
import {PaginatorModule} from "primeng/paginator";
import {SharedModule} from "../../../directive/shared.module";


@NgModule({
  declarations: [
    BrandListComponent
  ],
  imports: [
    CommonModule,
    BrandListRoutingModule,
    ToastModule,
    TableModule,
    ContextMenuModule,
    ButtonModule,
    OverlayPanelModule,
    InputTextModule,
    RippleModule,
    AutoFocusModule,
    FormsModule,
    PaginatorModule,
    SharedModule
  ]
})
export class BrandListModule { }

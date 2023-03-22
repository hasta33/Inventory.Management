import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InventoryListRoutingModule } from './inventory-list-routing.module';
import { InventoryListComponent } from './inventory-list.component';
import {TableModule} from "primeng/table";
import {ButtonModule} from "primeng/button";
import {InputTextModule} from "primeng/inputtext";
import {CardModule} from "primeng/card";
import {DropdownModule} from "primeng/dropdown";
import {InputNumberModule} from "primeng/inputnumber";
import {FormsModule} from "@angular/forms";
import {RippleModule} from "primeng/ripple";
import {OverlayPanelModule} from "primeng/overlaypanel";
import {MessageModule} from "primeng/message";
import {PaginatorModule} from "primeng/paginator";


@NgModule({
  declarations: [
    InventoryListComponent
  ],
  imports: [
    CommonModule,
    InventoryListRoutingModule,
    TableModule,
    ButtonModule,
    InputTextModule,
    CardModule,
    DropdownModule,
    InputNumberModule,
    FormsModule,
    RippleModule,
    OverlayPanelModule,
    MessageModule,
    PaginatorModule
  ]
})
export class InventoryListModule { }

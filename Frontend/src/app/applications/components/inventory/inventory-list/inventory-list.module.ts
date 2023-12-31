import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
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
import {ToastModule} from "primeng/toast";
import {ContextMenuModule} from "primeng/contextmenu";
import {TabViewModule} from "primeng/tabview";
import {DialogModule} from "primeng/dialog";
import {EmbezzledComponent} from "../embezzled/embezzled.component";
import {ToolbarModule} from "primeng/toolbar";


@NgModule({
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  declarations: [
    InventoryListComponent,
    EmbezzledComponent
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
    PaginatorModule,
    ToastModule,
    ContextMenuModule,
    TabViewModule,
    DialogModule,
    ToolbarModule,
  ]
})
export class InventoryListModule { }

import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {CategoryListRoutingModule} from './category-list-routing.module';
import {CategoryListComponent} from './category-list.component';
import {TableModule} from "primeng/table";
import {ToastModule} from "primeng/toast";
import {ContextMenuModule} from "primeng/contextmenu";
import {InputTextModule} from "primeng/inputtext";
import {ButtonModule} from "primeng/button";
import {RippleModule} from "primeng/ripple";
//import { AddRowDirective } from '../../../directive/add-row.directive';
import {AutoFocusModule} from "primeng/autofocus";
import {FormsModule} from "@angular/forms";
import {PaginatorModule} from "primeng/paginator";
import {SharedModule} from "../../../directive/shared.module";
import {OverlayPanelModule} from "primeng/overlaypanel";
import {TreeTableModule} from "primeng/treetable";

@NgModule({
  declarations: [
    CategoryListComponent,
    //AddRowDirective
  ],
  /*exports: [
    AddRowDirective
  ],*/
    imports: [
        CommonModule,
        CategoryListRoutingModule,
        TableModule,
        ToastModule,
        ContextMenuModule,
        InputTextModule,
        ButtonModule,
        RippleModule,
        AutoFocusModule,
        FormsModule,
        PaginatorModule,
        SharedModule,
        OverlayPanelModule,
        TreeTableModule
    ]
})
export class CategoryListModule {
}

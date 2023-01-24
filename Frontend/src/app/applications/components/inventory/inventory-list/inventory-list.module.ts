import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InventoryListRoutingModule } from './inventory-list-routing.module';
import { InventoryListComponent } from './inventory-list.component';


@NgModule({
  declarations: [
    InventoryListComponent
  ],
  imports: [
    CommonModule,
    InventoryListRoutingModule
  ]
})
export class InventoryListModule { }

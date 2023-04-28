import {NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmbezzledRoutingModule } from './embezzled-routing.module';
//import { EmbezzledComponent } from './embezzled.component';
import {InputTextModule} from "primeng/inputtext";
import {ButtonModule} from "primeng/button";
import {TableModule} from "primeng/table";


@NgModule({

  declarations: [
    //EmbezzledComponent
  ],
  imports: [
    CommonModule,
    EmbezzledRoutingModule,
    InputTextModule,
    ButtonModule,
      TableModule
  ]
})
export class EmbezzledModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccessRoutingModule } from './access-routing.module';
import { AccessComponent } from './access.component';
import {ButtonModule} from "primeng/button";
import {RippleModule} from "primeng/ripple";


@NgModule({
  declarations: [
    AccessComponent
  ],
  imports: [
    CommonModule,
    AccessRoutingModule,
    ButtonModule,
    RippleModule
  ]
})
export class AccessModule { }

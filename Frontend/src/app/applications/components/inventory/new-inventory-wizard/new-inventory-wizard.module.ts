import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NewInventoryWizardRoutingModule } from './new-inventory-wizard-routing.module';
import { NewInventoryWizardComponent } from './new-inventory-wizard.component';
import {StepsModule} from "primeng/steps";
import {ToastModule} from "primeng/toast";
import {ButtonModule} from "primeng/button";
import {TableModule} from "primeng/table";
import {InputTextModule} from "primeng/inputtext";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RippleModule} from "primeng/ripple";
import {PaginatorModule} from "primeng/paginator";
import {CardModule} from "primeng/card";
import {MessageModule} from "primeng/message";
import {CalendarModule} from "primeng/calendar";
import {DialogModule} from "primeng/dialog";


@NgModule({
  declarations: [
    NewInventoryWizardComponent
  ],
    imports: [
        CommonModule,
        NewInventoryWizardRoutingModule,
        StepsModule,
        ToastModule,
        ButtonModule,
        TableModule,
        InputTextModule,
        FormsModule,
        RippleModule,
        PaginatorModule,
        CardModule,
        ReactiveFormsModule,
        MessageModule,
        CalendarModule,
        DialogModule
    ]
})
export class NewInventoryWizardModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import {PasswordModule} from "primeng/password"; //sonradan eklendi
import {CheckboxModule} from "primeng/checkbox"; //sonradan eklendi
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RippleModule} from "primeng/ripple";
import {ToastModule} from "primeng/toast";


@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    LoginRoutingModule,
    ButtonModule,
    CheckboxModule,
    InputTextModule,
    FormsModule,
    PasswordModule,
    ReactiveFormsModule,
    RippleModule,
    ToastModule
  ]
})
export class LoginModule { }

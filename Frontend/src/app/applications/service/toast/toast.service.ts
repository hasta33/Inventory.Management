import {Component, Injectable} from '@angular/core';
import {MessageService} from "primeng/api";

@Injectable({
  providedIn: 'root'
})

export class ToastService {

  constructor(
    private messageService: MessageService
  ) { }

  public showSuccess() {
    this.messageService.add({severity:'success', summary: 'Success', detail: 'Message Content'});
  }
}

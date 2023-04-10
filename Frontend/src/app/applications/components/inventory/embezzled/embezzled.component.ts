import {Component, OnInit} from '@angular/core';
import {InventoryService} from "../../../service/inventory/inventory.service";
import {constants} from "../../../constants/constants";
import {MessageService} from "primeng/api";
import {PersonalListModel} from "../../../models/personal-list/personal-list";

@Component({
  selector: 'app-embezzled',
  templateUrl: './embezzled.component.html',
  styleUrls: ['./embezzled.component.scss'],
  providers: [MessageService]
})

export class EmbezzledComponent implements OnInit {
  personalList: any;
  searchPersonal: any;

  constructor(private messageService: MessageService,
              private inventoryService: InventoryService) {
  }

  ngOnInit() {
    //this.getPersonalList();
  }

  getPersonalList() {
    this.inventoryService.getPersonalList(this.searchPersonal).subscribe({
      next: (data) => {
        //this.categoryList = data?.data;
        console.log(data)
        this.personalList = data;
      },
      complete: () => {
        //this.loading = false;
      },
      error: (e) => {
        this.messageService.add({ severity: 'error', summary: 'Hata', detail: `Kullanıcı listesi alınamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME });
        this.messageService.clear('c');
        //this.loading = false;
      },
    });
  }
  keyDownFunction(event: any) {
    //console.log(event)
    if (event.keyCode === 13) {
      this.getPersonalList();
    }
  }


}

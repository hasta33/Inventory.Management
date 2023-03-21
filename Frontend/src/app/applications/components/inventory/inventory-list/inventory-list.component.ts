import {Component, OnInit} from '@angular/core';
import {InventoryService} from "../../../service/inventory.service";
import {InventoryModel} from "../../../models/inventory/inventory";
import {PrimeNGConfig} from "primeng/api";

@Component({
  selector: 'app-inventory-list',
  templateUrl: './inventory-list.component.html',
  styleUrls: ['./inventory-list.component.scss']
})
export class InventoryListComponent implements OnInit {
  inventoryList: InventoryModel[] = [];
  page: number = 1;
  pageSize: number = 100;
  loading: boolean = true;
  totalRecords: number = 0;


  constructor(
    private inventoryService: InventoryService,
    private primengConfig: PrimeNGConfig ) {  }

  ngOnInit() {
    this.primengConfig.ripple = true;
  }

  getInventoryLis() {

  }
}

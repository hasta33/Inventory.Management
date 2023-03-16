import { Component } from '@angular/core';
import {MenuItem} from "primeng/api";

@Component({
  selector: 'app-app-topbar',
  templateUrl: './app-topbar.component.html',
  styleUrls: ['./app-topbar.component.scss']
})
export class AppTopbarComponent {

  menuItems: MenuItem[] | any ;

  ngOnInit() {
    this.menuItems = [
      {
        label:'Envanter',
        icon:'pi pi-fw pi-box',
        items:[
          {
            label:'Envanter Listesi',
            icon:'pi pi-fw pi-book',
            routerLink: ['/inventory/inventory-list']
          },
          {
            label:'Delete',
            icon:'pi pi-fw pi-user-minus',

          },
          {
            label:'Search',
            icon:'pi pi-fw pi-users',
            items:[
              {
                label:'Filter',
                icon:'pi pi-fw pi-filter',
                items:[
                  {
                    label:'Print',
                    icon:'pi pi-fw pi-print'
                  }
                ]
              },
              {
                icon:'pi pi-fw pi-bars',
                label:'List'
              }
            ]
          }
        ]
      },

      {
        label:'Tanımlamalar',
        icon:'pi pi-fw pi-bookmark',
        items:[
          {
            label:'Şirket Bilgisi',
            icon:'pi pi-fw pi-building',
            items:[
              {
                label:'Şirket Listesi',
                icon:'pi pi-fw pi-building',
                routerLink: ['/company/company-list']
              }
            ]
          },
          {
            label:'Kategori Tanımla',
            icon:'pi pi-fw pi-calendar-times',
            items:[
              {
                label:'Kategori Listesi',
                icon:'pi pi-fw pi-calendar-minus',
                routerLink: ['/category/category-list']
              }
            ]
          },
          {
            label:'Marka Tanımla',
            icon:'pi pi-fw pi-calendar-times',
            items:[
              {
                label:'Marka Listesi',
                icon:'pi pi-fw pi-calendar-minus',
                routerLink: ['/brand/brand-list']
              }
            ]
          }
        ]
      },
      {
        label:'Quit',
        icon:'pi pi-fw pi-power-off'
      }
    ];
  }
}

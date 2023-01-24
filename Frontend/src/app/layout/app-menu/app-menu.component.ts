import { Component } from '@angular/core';
import {MenuItem} from "primeng/api";

@Component({
  selector: 'app-app-menu',
  templateUrl: './app-menu.component.html',
  styleUrls: ['./app-menu.component.scss']
})
export class AppMenuComponent {

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
              },
              {
                label:'Şirket Tanımlama',
                icon:'pi pi-fw pi-calendar-minus'
              },

            ]
          },
          {
            label:'Kategori Tanımla',
            icon:'pi pi-fw pi-calendar-times',
            items:[
              {
                label:'Remove',
                icon:'pi pi-fw pi-calendar-minus'
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

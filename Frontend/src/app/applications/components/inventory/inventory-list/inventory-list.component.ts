import {Component, OnInit} from '@angular/core';
import {InventoryService} from "../../../service/inventory.service";
import {InventoryModel} from "../../../models/inventory/inventory";
import {MenuItem, MessageService, PrimeNGConfig} from "primeng/api";
import {CompanyService} from "../../../service/company/company.service";
import {CompanyModel} from "../../../models/company/company";
import {constants} from "../../../constants/constants";
import {CategoryService} from "../../../service/category/category.service";
import {CategoryModel} from "../../../models/category/category";
import {BrandService} from "../../../service/brand/brand.service";
import {BrandModel} from "../../../models/brand/brand";

@Component({
  selector: 'app-inventory-list',
  templateUrl: './inventory-list.component.html',
  styleUrls: ['./inventory-list.component.scss'],
  providers: [MessageService]
})
export class InventoryListComponent implements OnInit {
  inventoryList: InventoryModel[] = [];
  page: number = 1;
  pageSize: number = 100;
  loading: boolean = true;
  totalRecords: number = 0;

  //Company
  companies: CompanyModel[] = [];
  selectedCompany: any;

  //Category
  categoryList: CategoryModel[] = [];
  selectedCategory: CategoryModel = this.categoryList[0];
  selectedCategorySub: any;
  selectedCategorySubs: any;
  categorySubsList: any;

  //Brand
  brandList: BrandModel[] = [];
  selectedBrand: BrandModel = this.brandList[0];
  modelsList: any;
  selectedModel: any;

  //#contextMenu
  contextMenu: MenuItem[] = [];

  //query params
  selectedBarcode: any;
  selectedSerialNumber: any;
  selectedName: any;
  selectedMac: any;
  selectedImei: any;
  selectedResponsible: any;
  selectedStatus: any;

  constructor(
    private inventoryService: InventoryService,
    private primengConfig: PrimeNGConfig,
    private messageService: MessageService,
    private companyService: CompanyService,
    private categoryService: CategoryService,
    private brandService: BrandService) {  }

  ngOnInit() {
    this.primengConfig.ripple = true;

    this.contextMenu = [
      { label: 'Yenile', icon: 'pi pi-fw pi-refresh', command: () => this.getCompanyAllList() },
      { label: 'Şirketi sil', icon: 'pi pi-fw pi-times', command: () => this.getCompanyAllList() }
    ];

    this.getCompanyAllList();
    this.getCategoryAllList();
    this.getBrandAllList();

    this.searchInventory();
  }

  getCompanyAllList() {
    this.companyService.getCompanyAllList(this.page, this.pageSize).subscribe({
      next: (data) => {
        this.companies = data?.data;
        //this.totalRecords = data?.data[0].totalCount;
      },
      error: (e) => {
        this.messageService.add({ severity: 'error', summary: 'Hata', detail: `Şirket listesi alınamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME });
        this.messageService.clear('c');
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }
  onRowCompanySelect(event: any) {
    this.selectedCompany = event?.value;
  }

  //Category
  getCategoryAllList() {
    this.categoryService.getCategoryAllList().subscribe({
      next: (data) => {
        this.categoryList = data?.data;
      },
      complete: () => {
        this.loading = false;
      },
      error: (e) => {
        this.messageService.add({ severity: 'error', summary: 'Hata', detail: `Kategori listesi alınamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME });
        this.messageService.clear('c');
        this.loading = false;
      },
    });
  }
  setDataCategory(event: any) {
    this.categorySubsList = event.value?.categorySubs;
  }
  setDataCategorySub(event: any) {
    this.selectedCategorySub = event?.value;
  }



  //Brand
  getBrandAllList() {
    this.brandService.getBrandAllList().subscribe({
      next: (data) => {
        this.brandList = data?.data;
      },
      complete: () => {
        this.loading = false;
      },
      error: (e) => {
        this.messageService.add({ severity: 'error', summary: 'Hata', detail: `Marka listesi alınamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME });
        this.messageService.clear('c');
        this.loading = false;
      }
    })
  }
  setDataBrand(event:any) {
    this.modelsList = event.value?.models;
  }
  setDataModel(event: any) {
    this.selectedModel = event?.value;
  }

  searchInventory(){
    this.inventoryService
      .getInventoryAllList(this.page, this.pageSize, {
        companyId: this.selectedCompany?.id,
        categoryId: this.selectedCategory?.id,
        categorySubId:this.selectedCategorySub?.id,
        brandId: this.selectedBrand?.id,
        modelId: this.selectedModel?.id,
        name: this?.selectedName,
        barcode: this?.selectedBarcode,
        serialNumber: this?.selectedSerialNumber,
        mac: this?.selectedMac,
        imei: this?.selectedImei,
        responsibleUser: this?.selectedResponsible,
        status: this?.selectedStatus
      })
      .subscribe({
        next: (data) => {
          this.inventoryList = data?.data;
          this.totalRecords = data.data[0]?.totalCount;
        },
        error: (error) => {
          this.messageService.add({severity:'error', summary: 'Hata', detail: `Envanter listesi alınamadı \n${error}`, life: constants.TOAST_ERROR_LIFETIME});
          this.messageService.clear('c');
          this.loading = false;
        },
        complete: () => {
          this.loading = false;
        }
      })
  }

  //InventoryDetail
  onDetailInventory(event: any) {
    console.log(event)
  }
}



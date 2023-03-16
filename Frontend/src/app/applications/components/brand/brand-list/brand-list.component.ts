import {Component, OnInit} from '@angular/core';
import {CompanyModel} from "../../../models/company/company";
import {BrandService} from "../../../service/brand/brand.service";
import {CompanyService} from "../../../service/company/company.service";
import {MenuItem, MessageService, PrimeNGConfig} from "primeng/api";
import {constants} from "../../../constants/constants";
import {ModelService} from "../../../service/model/model.service";
import {BrandModel} from "../../../models/brand/brand";
import {BrandSubModel} from "../../../models/brand/brandsubmodel";

@Component({
  selector: 'app-brand-list',
  templateUrl: './brand-list.component.html',
  styleUrls: ['./brand-list.component.scss'],
  providers: [ MessageService ]
})
export class BrandListComponent implements OnInit {

  //Brand
  brandList: BrandModel[] = [];
  page: number = 1;
  pageSize: number = 100;
  loading: boolean = true;
  totalRecords: number = 0;
  selectedBrandRowIndex: number = -1;
  selectedBrand: BrandModel | any;
  clonedBrandList: { [s: string]: BrandModel } = {};

  //Model
  selectedModel: any;
  selectedModelRowIndex: number = -1;
  clonedModelList: { [s: string]: BrandModel } = {};

  //Company
  selectedCompany: any;

  //Overlay - Companies
  companies: CompanyModel[] = [];

  //context menu
  brandContextMenu: MenuItem[] = [];
  modelContextMenu: MenuItem[] = [];

  rowCounter: number = 0;

  constructor(
    private brandService: BrandService,
    private companyService: CompanyService,
    private modelService: ModelService,
    private messageService: MessageService,
    private primengConfig: PrimeNGConfig ) { }

  ngOnInit() {
    this.getCompanyAllList();

    this.primengConfig.ripple = true;

    this.brandContextMenu = [
      { label: 'Yenile', icon: 'pi pi-fw pi-refresh', command: () => this.getBrandList() },
      { label: 'Marka sil', icon: 'pi pi-fw pi-times', command: () => this.removeConfirm('brand') }
    ];
    this.modelContextMenu = [
      { label: 'Yenile', icon: 'pi pi-fw pi-refresh', command: () => this.getBrandList() },
      { label: 'Model sil', icon: 'pi pi-fw pi-times', command: () => this.removeConfirm('model') }
    ];
  }


  // --------------------------- Company ---------------------------
  getCompanyAllList() {
    this.companyService.getCompanyAllList(this.page, this.pageSize).subscribe({
      next: (data) => {
        this.companies = data?.data;
        this.totalRecords = data?.data[0].totalCount;
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
  // --------------------------- Company ---------------------------


  // --------------------------- Brand ---------------------------
  getBrandList() {
    this.brandService.getBrandList(this.selectedCompany.id, this.page, this.pageSize).subscribe({
      next: (data) => {
        this.brandList = data?.data;
        this.totalRecords = data.data[0]?.totalCount;
      },
      complete: () => {
        this.loading = false;
      },
      error: (e) => {
        this.messageService.add({ severity: 'error', summary: 'Hata', detail: `Marka listesi alınamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME });
        this.messageService.clear('c');
        this.loading = false;
      }
    });
  }
  addBrand(brand: BrandModel, index: number) {
    brand.id = brand.id.toString().trim();

    this.brandService.postBrand(brand)
      .subscribe({
        next: (response: any) => {
          this.brandList.push((response.data));
          this.brandList = [...this.brandList];
        },
        complete: () => {
          this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: `${brand.name} isimli marka kaydedildi.`, life: constants.TOAST_SUCCESS_LIFETIME });
          this.messageService.clear('c');

          //Remove temp item table
          const item = this.brandList.find(item => item.id === brand.id);
          if (item) {
            const itemIndex = this.brandList.indexOf(item);
            this.brandList.splice(itemIndex, 1);
          }
        },
        error: (e) => {
          this.messageService.add({ severity: 'error', summary: 'Hata', detail: `kayıt yapılamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME });
          this.messageService.clear('c');
        }
      });
  }
  putBrand(brand: BrandModel) {
    this.brandService.putBrand(brand)
      .subscribe({
        next: (response) => {

        },
        complete: () => {
          this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: 'Kayıt güncellendi', life: constants.TOAST_SUCCESS_LIFETIME });
          this.messageService.clear('c');
        },
        error: (e) => {
          this.messageService.add({ severity: 'error', summary: 'Hata', detail: `Başarısız kayıt \n${e}`, life: constants.TOAST_ERROR_LIFETIME });
          this.messageService.clear('c');
        }
      });
  }
  deleteBrand() {
    this.brandService.deleteBrand(this.selectedBrand.id)
      .subscribe({
        next: (response) => {

        },
        complete: () => {
          //Brand delete array
          const brandIndex = this.brandList.findIndex(subCategory => subCategory.id === this.selectedBrand.id);
          if (brandIndex > -1) {
            this.brandList.splice(brandIndex, 1);
          }
          this.brandList = Object.assign([], this.brandList);

          //Popup remove
          this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: `${this.selectedBrand.name} isimli marka silindi`, life: constants.TOAST_SUCCESS_LIFETIME });
          this.messageService.clear('c');
          this.selectedBrand = null;
        },
        error: (e) => {
          this.messageService.add({ severity: 'error', summary: 'Hata', detail: `${this.selectedBrand.name} marka silinemedi ${e}`, life: constants.TOAST_ERROR_LIFETIME });
          this.messageService.clear('c');
        }
      });
  }

  // --------------------------- Table Row Brand ---------------------------
  onRowBrandEditInit(brand: BrandModel) {
    this.clonedBrandList[brand?.id] = { ...brand };
  }
  onRowBrandEditSave(brand: BrandModel, index: number) {
    brand.companyId = this.selectedCompany.id;

    if(!brand.id.toString().indexOf(' ')) {
      this.addBrand(brand, index);
    } else if (brand.id.toString().indexOf(' ')) {
      this.putBrand(brand);
    }
  }
  onRowBrandEditCancel(brand: BrandModel, index: number) {
    this.brandList[index] = this.clonedBrandList[brand?.id];
    delete this.clonedBrandList[brand?.id];
  }
  selectedBrandEvent(event: any, data: any, index: any) {
    this.selectedBrand = data;
    this.selectedBrandRowIndex = index;
  }
  // --------------------------- Table Row Brand ---------------------------

  // --------------------------- Brand ---------------------------





  // --------------------------- Model ---------------------------
  addModel(model: BrandSubModel, index: number){
    model.id = model.id.toString().trim();
    this.modelService.postModel(model)
      .subscribe({
        next: (response: any) => { //response any değilde direkt response diyerek deneyeceğim ilerde
          this.brandList[this.selectedBrandRowIndex].models.push(response.data);
          this.brandList = [...this.brandList]
        },
        complete: () => {
          this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: `${model.name} isimli model kaydedildi.`,life: constants.TOAST_SUCCESS_LIFETIME });
          this.messageService.clear('c');

          //Remove temp item table
          const item = this.brandList[this.selectedBrandRowIndex].models.find(item => item.id === model.id);
          if (item) {
            const itemIndex = this.brandList[this.selectedBrandRowIndex].models.indexOf(item);
            this.brandList[this.selectedBrandRowIndex].models.splice(itemIndex, 1);
          }
        },
        error: (e) => {
          this.messageService.add({ severity: 'error', summary: 'Hata', detail: `kayıt yapılamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME });
          this.messageService.clear('c');
        }
      })
  }
  putModel(model: BrandSubModel) {
    this.modelService.putModel(model)
      .subscribe({
        next: (response) => {

        },
        complete: () => {
          this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: 'Kayıt güncellendi', life: constants.TOAST_SUCCESS_LIFETIME });
          this.messageService.clear('c');
        },
        error: (e) => {
          this.messageService.add({ severity: 'error', summary: 'Hata', detail: `Başarısız kayıt \n${e}`, life: constants.TOAST_ERROR_LIFETIME });
          this.messageService.clear('c');
        }
      });
  }
  deleteModel() {
    this.modelService.deleteModel(this.selectedModel.id)
      .subscribe({
        next: (response) => {

        },
        complete: () => {
          //Popup remove
          this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: `${this.selectedModel.name} isimli model silindi`, life: constants.TOAST_SUCCESS_LIFETIME });
          this.messageService.clear('c');

          //Remove temp item table
          const item = this.brandList[this.selectedBrandRowIndex].models.find(item => item.id === this.selectedModel.id);
          if (item) {
            const itemIndex = this.brandList[this.selectedBrandRowIndex].models.indexOf(item);
            this.brandList[this.selectedBrandRowIndex].models.splice(itemIndex, 1);
          }
          this.selectedModel = null;
        },
        error: (e) => {
          this.messageService.add({ severity: 'error', summary: 'Hata', detail: `${this.selectedModel.name} alt kategori silinemedi ${e}`, life: constants.TOAST_ERROR_LIFETIME });
          this.messageService.clear('c');
        }
      });
  }


  // --------------------------- Table Row Model ---------------------------
  onRowModelEditInit(model: any) {
    this.clonedModelList[model?.id] = { ...model };
  }
  onRowModelEditSave(model: any, index: number) {
    model.brandId = this.selectedBrand.id;

    if(!model.id.toString().indexOf(' ')) {
      this.addModel(model, index);
    } else if (model.id.toString().indexOf(' ')) {
      this.putModel(model);
    }
  }
  onRowModelEditCancel(model: any, index: number) {
    this.brandList[index] = this.clonedBrandList[model?.id];
    delete this.clonedBrandList[model?.id];
  }
  selectedModelEvent(event: any, data: any, rowIndex: any) {
    this.selectedModel = data;
    this.selectedModelRowIndex = rowIndex;
  }
  // --------------------------- Table Row Model ---------------------------
  // --------------------------- Model ---------------------------


  // --------------------------- General ---------------------------
  onRowSelect(event: any) {
    this.messageService.add({ severity: 'info', summary: 'Şirket Seç', detail: event.data.name });
    this.selectedCompany = event.data;
    //this.cloneSelectedCompany = event.data;

    this.getBrandList();
  }
  public handlePagination(paginationData: any): void {
    this.page = paginationData.page + 1;
    this.pageSize = paginationData.rows;
    this.getBrandList();
  }
  newRow() {
    return {
      id: ' ' + this.rowCounter++,
      name: ''
    };
  }
  removeConfirm(selectedType: string) {
    this.messageService.clear();
    this.messageService.add({key: 'c', sticky: true, severity: 'warn', summary: 'Silmek istediğinizden emin misiniz ?', data: `${selectedType}`, detail: 'Devam etmek için onaylayın' });
  }
  onReject() {
    this.messageService.clear('c');
  }
  // --------------------------- General ---------------------------
}


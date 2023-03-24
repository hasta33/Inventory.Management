import {Component, OnInit} from '@angular/core';
import {MenuItem, MessageService, PrimeNGConfig} from "primeng/api";
import {CompanyService} from "../../../service/company/company.service";
import {CompanyModel} from "../../../models/company/company";
import {constants} from "../../../constants/constants";
import {CategoryService} from "../../../service/category/category.service";
import {CategoryModel} from "../../../models/category/category";
import {BrandModel} from "../../../models/brand/brand";
import {BrandService} from "../../../service/brand/brand.service";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {InventoryModel} from "../../../models/inventory/inventory";
import {InventoryService} from "../../../service/inventory/inventory.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-new-inventory-wizard',
  templateUrl: './new-inventory-wizard.component.html',
  styleUrls: ['./new-inventory-wizard.component.scss'],
  providers: [MessageService]
})
export class NewInventoryWizardComponent implements OnInit {
  //steps
  stepItems: MenuItem[] = [];
  activeIndex: number = 0;

  //Company
  companyList: CompanyModel[] = [];
  selectedCompany: any;
  loading: boolean = true;
  page: number = 1;
  pageSize: number = 250;
  totalRecordsCompany: number = 0;

  //Category
  categoryList: CategoryModel[] = [];
  totalRecordsCategory: number = 0;
  selectedCategory: CategoryModel = this.categoryList[0];

  //categorySub
  categorySubsList: any;
  selectedCategorySubs: any;
  selectedCategorySub: any;

  //Brand
  brandList: BrandModel[] = [];
  totalRecordsBrand: number = 0;
  selectedBrand: BrandModel = this.brandList[0];

  //model
  modelsList: any;
  selectedModels: any;
  selectedModel: any;

  //categoryValidations
  categoryForm: FormGroup | any;
  categorySubmitted: boolean = false;

  //category-model validations
  brandForm: FormGroup | any;
  modelSubmitted: boolean = false;

  //detail
  detailForm: FormGroup | any;
  detailSubmitted: boolean = false;
  inventoryChangeValues: InventoryModel[] = [];
  defaultDate: Date = new Date();

  //approve Inventory
  displayModal: boolean = false;


  constructor(
    private router: Router,
    private fb: FormBuilder,
    private primengConfig: PrimeNGConfig,
    private messageService: MessageService,
    private inventoryService: InventoryService,
    private companyService: CompanyService,
    private categoryService: CategoryService,
    private brandService: BrandService) {}

  ngOnInit() {
    this.stepItems = [
      {
        label: 'Şirket',
        command: (event: any) => {
        this.activeIndex = 0;
        this.messageService.add({severity:'info', summary:'Şirket', detail: event.item.label});
      }
      },
      {
        label: 'Kategori',
        command: (event: any) => {
          this.activeIndex = 1;
          this.messageService.add({severity:'info', summary:'Seat Selection', detail: event.item.label});
        }
      },
      {
        label: 'Marka Model',
        command: (event: any) => {
          this.activeIndex = 2;
          this.messageService.add({severity:'info', summary:'Pay with CC', detail: event.item.label});
        }
      },
      {
        label: 'Ürün Detayları',
        command: (event: any) => {
          this.activeIndex = 3;
          this.messageService.add({severity:'info', summary:'Pay with CC', detail: event.item.label});
        }
      },
      {
        label: 'Onay',
        command: (event: any) => {
          this.activeIndex = 4;
          this.messageService.add({severity:'info', summary:'Last Step', detail: event.item.label});
        }
      }
    ];

    //ripple
    this.primengConfig.ripple = true;

    this.getCompanyAllList();

    this.categoryForm = this.fb.group({
      'categoryName': new FormControl('', Validators.required),
      'categoryModel': new FormControl('', Validators.required)
    });
    this.brandForm = this.fb.group({
      'brandName': new FormControl('', Validators.required),
      'modelName': new FormControl('', Validators.required)
    });
    this.detailForm = this.fb.group({
      'deviceName': new FormControl('', Validators.required),
      'barcodeName': new FormControl(null, Validators.required),
      'serialNumber': new FormControl('', Validators.required),
      'imei': new FormControl(null),
      'mac': new FormControl(''),
      'inventoryDate': new FormControl(''),
      'invoiceDate': new FormControl('')
    });
  }



  //#GetCompanyAllList {page}/{pageSize}
  getCompanyAllList() {
    this.companyService.getCompanyAllList(this.page, this.pageSize).subscribe({
      next: (data) => {
        this.companyList = data?.data;
        this.totalRecordsCompany = data.data[0]?.totalCount;
      },
      error: (e) => {
        this.messageService.add({severity:'error', summary: 'Hata', detail: `Şirket listesi alınamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME});
        this.messageService.clear('c');
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }
  companyOnRowSelected(data: any) {
    this.selectedCompany = data;

    this.getCategoryList();
  }



  // ---------------- Kategori - Alt kategori
  getCategoryList() {
    this.categoryService.getCategoryList(this.selectedCompany.id, this.page, this.pageSize).subscribe({
      next: (data) => {
        this.categoryList = data?.data;
        this.totalRecordsCategory = data.data[0]?.totalCount;
        },
      complete: () => {
        this.loading = false;

        this.getBrandList();
      },
      error: (e) => {
        this.messageService.add({ severity: 'error', summary: 'Hata', detail: `Kategori listesi alınamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME });
        this.messageService.clear('c');
        this.loading = false;
      },
    });
  }
  setDataCategory(event: any) {
    this.categorySubsList = event.value.categorySubs;
  }
  setDataCategorySub(event: any) {
    this.selectedCategorySub = event.value;
  }
  onCategorySubmit(value: string) {
    this.categorySubmitted = true;
    this.messageService.add({severity:'info', summary:'Success', detail:'Kategori Submitted', sticky: true});
  }
  // ---------------- Kategori - Alt kategori


  // ---------------- Brand - Model
  getBrandList() {
    this.brandService.getBrandList(this.selectedCompany.id, this.page, this.pageSize).subscribe({
      next: (data) => {
        this.brandList = data?.data;
        this.totalRecordsBrand = data.data[0]?.totalCount;
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
    this.modelsList = event.value.models;
  }
  setDataModel(event: any) {
    this.selectedModel = event.value;
  }
  onBrandSubmit(value: string) {
    this.modelSubmitted = true;
    this.messageService.add({severity:'info', summary:'Success', detail:'Brand Submitted', sticky: true});
  }
  // ---------------- Brand - Model

  //detail
  onDetailSubmit(value: string) {
    this.detailSubmitted = true;
    this.messageService.add({severity:'info', summary:'Success', detail:'Detail Submitted', sticky: true});
  }
  postInventory(data: InventoryModel) {
    data.companyId = this.selectedCompany.id;
    data.categoryId = this.selectedCategory.id;
    data.categorySubId = this.selectedCategorySub.id;
    data.brandId = this.selectedBrand.id;
    data.modelId = this.selectedModel.id;
    data.name = this.detailForm.controls['deviceName'].value;
    data.inventoryDate = this.detailForm.controls['inventoryDate'].value;
    data.invoiceDate = this.detailForm.controls['invoiceDate'].value;
    data.barcode = this.detailForm.controls['barcodeName'].value;
    data.serialNumber = this.detailForm.controls['serialNumber'].value;
    data.imei = this.detailForm.controls['imei'].value;
    data.mac = this.detailForm.controls['mac'].value;
    data.status = 'Depoda';

    this.inventoryService.postInventory(data)
      .subscribe({
        next: (response: any) => {
        },
        complete: () => {
          this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: `${data.name} isimli envanter kaydedildi.`, life: constants.TOAST_SUCCESS_LIFETIME });
          this.messageService.clear('c');

          this.showModalDialog();
        },
        error: (e) => {
          this.messageService.add({ severity: 'error', summary: 'Hata', detail: `kayıt yapılamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME });
          this.messageService.clear('c');
        }
      });
  }
  showModalDialog() {
    this.displayModal = true;
  }
  newInventoryReset() {
    this.displayModal = false;
    this.selectedModel = null;
    this.selectedBrand = new BrandModel();
    this.selectedCategorySub = null;
    this.selectedCategory = new CategoryModel();
    this.selectedCompany = null;
    this.detailForm.reset({
      'deviceName': new FormControl('', Validators.required),
      'barcodeName': new FormControl(null, Validators.required),
      'serialNumber': new FormControl('', Validators.required),
      'imei': new FormControl(null),
      'mac': new FormControl(''),
      'inventoryDate': new FormControl(''),
      'invoiceDate': new FormControl('')
    });
    this.brandForm.reset({
      'brandName': new FormControl('', Validators.required),
      'modelName': new FormControl('', Validators.required)
    });
    this.categoryForm.reset({
      'categoryName': new FormControl('', Validators.required),
      'categoryModel': new FormControl('', Validators.required)
    });
    this.activeIndex = 0
  }


  public handlePagination(paginationData: any): void {
    this.page = paginationData.page +1;
    this.pageSize = paginationData.rows;
    this.getCompanyAllList();
  }
  nextPage() {
    this.activeIndex++;
  }
  prevPage() {
    this.activeIndex--;
  }
}

/*onDetailChange(event: any) {
  //console.log(event);
  this.detailForm.patchValue([
    { barcodeName: event.value},
    //{ empId: "112", empName: "Krishna", skill: "Angular"}
  ]);

  //console.log(this.detailForm.get('barcodeName'));
  //this.detailForm.at(0).get('barcodeName').setValue()
  //this.detailForm.get('barcodeName').setValue(event.value);
}*/
//
/*go_next(){
  setTimeout(() => {
      this.router.navigate(['../inventory/inventory-list'], {queryParams: {'part': 1 + 1}})
    }
    , 2500);
}*/

import {Component, OnInit, ViewChild} from '@angular/core';
import {CategoryModel} from "../../../models/category/category";
import {CategoryService} from "../../../service/category/category.service";
import {MenuItem, MessageService, PrimeNGConfig, SelectItem} from "primeng/api";
import {constants} from "../../../constants/constants";
import {Table} from "primeng/table";
import {CompanyService} from "../../../service/company/company.service";
import {CompanyModel} from "../../../models/company/company";

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss'],
  providers: [MessageService]
})
export class CategoryListComponent implements OnInit {
  constructor(
    private categoryService: CategoryService,
    private companyService: CompanyService,
    private messageService: MessageService,
    private primengConfig: PrimeNGConfig) { }

  @ViewChild(Table, { read: Table }) pTable: Table | any;

  //#table
  categoryList: CategoryModel[] = [];

//dropdown
  cmpDropDown = null;
  deptDropDown = null;
  feedDropDown = null;
  public cmpResp: any;

  columns: [] | any;
  page: number = 1;
  pageSize: number = 10;
  loading: boolean = true;
  totalRecords: number = 0;

  //#contextMenu
  itemsMenu: MenuItem[] = [];
  selectedCategory: CategoryModel | any;

  clonedCategoryList: { [s: string]: CategoryModel } = {};
  rowCounter: number = 0;

  public prodResp: any = [
    {"id": "1", "name": "ereğli", "businessCode": "120"},
    {"id": "2", "name": "konya", "businessCode": "130"},
    {"id": "3", "name": "istanbul", "businessCode": "140"},
  ];

  ngOnInit() {

    this.primengConfig.ripple = true;

    this.columns = [
      {field: 'id', header: 'id'},
      {field: 'name', header: 'name'}
    ]

    this.getCategories();

    this.itemsMenu = [
      {label: 'Yenile', icon: 'pi pi-fw pi-refresh', command: () => this.getCategories()},
      {label: 'Kategori sil', icon: 'pi pi-fw pi-times', command: () => this.showConfirm(this.selectedCategory)}
    ];
  }


  onChange(): void {
    console.log("Compnay Id :- ", this.feedDropDown)
    console.log("Department Id :- ", this.deptDropDown)
    console.log("Product Id :- ", this.feedDropDown)
  }

  getCompanyOnlyNameAndBusinessCode(){
    this.companyService.getCompanyOnlyNameAndBusinessCode().subscribe({
      next: (data) => {
        /*this.companies = data.data.map((name) => {
          return { label: name.name, value: name.businessCode}
        });*/
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

  //#Get categories list
  getCategories() {
    this.categoryService.getCategoryList(this.page, this.pageSize).subscribe({
      next: (data) => {
        this.categoryList = data.data;
        this.totalRecords = data.data[0].totalCount;
      },
      error: (e) => {
        this.messageService.add({severity:'error', summary: 'Hata', detail: `Kategori listesi alınamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME});
        this.messageService.clear('c');
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
        this.getCompanyOnlyNameAndBusinessCode();
      }
    });
  }

  //#putCompany
  putCategory(category: CategoryModel) {
    this.categoryService.putCategory(category).subscribe({
      next: (response) => {
      },
      error: (e) => {
        this.messageService.add({severity:'error', summary: 'Hata', detail: `Başarısız kayıt \n${e}`, life: constants.TOAST_ERROR_LIFETIME});
        this.messageService.clear('c');
      },
      complete: () => {
        this.messageService.add({severity:'success', summary: 'Başarılı', detail: 'Kayıt güncellendi', life: constants.TOAST_SUCCESS_LIFETIME});
        this.messageService.clear('c');
      },
    });
  }

  //#Delete company
  deleteCategory() {
    this.categoryService.deleteCategory(this.selectedCategory.id)
      .subscribe({
        next: (response) => {

        },
        error: (e) => {
          this.messageService.add({severity: 'error', summary: 'Hata', detail: `${this.selectedCategory.name}  kategori silinemedi ${e}`, life: constants.TOAST_ERROR_LIFETIME});
          this.messageService.clear('c');
        },
        complete: () => {
          this.categoryList = this.categoryList.filter((p) => p.id !== this.selectedCategory.id);//table uzerinden sil
          this.messageService.add({severity: 'success', summary: 'Başarılı', detail: `${this.selectedCategory.name} isimli şirket silindi`, life: constants.TOAST_SUCCESS_LIFETIME});
          this.messageService.clear('c');
          this.selectedCategory = null;  //null yap secileni
        }
      })
  }

  addCategory(category: CategoryModel, index: number) {
    this.categoryService.postCategory(category)
      .subscribe({
        next: (response: any) => {
          this.categoryList.push(response.data);
          this.categoryList = [...this.categoryList];
        },
        error: (e) => {
          this.messageService.add({severity: 'error', summary: 'Hata', detail: `kayıt yapılamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME});
          this.messageService.clear('c');
        },
        complete: () => {
          this.messageService.add({severity: 'success', summary: 'Başarılı', detail: `${category.name} isimli kategori kaydedildi.`, life: constants.TOAST_SUCCESS_LIFETIME});
          this.messageService.clear('c');
          this.categoryList[index] = this.clonedCategoryList[category.id];
          delete this.clonedCategoryList[category.id];
        }
      });
  }

  //#Edit
  onRowEditInit(category: CategoryModel) {
    this.clonedCategoryList[category.id] = { ...category };
  }

  //#Save
  onRowEditSave(category: CategoryModel, index: number) {
    if (!category.id.toString().indexOf(' ')){
      this.addCategory(category, index);
    } else if (category.id.toString().indexOf(' ')) {
      this.putCategory(category);
    }
  }


  //#Cancel
  onRowEditCancel(category: CategoryModel, index: number) {
    this.categoryList[index] = this.clonedCategoryList[category.id];
    delete this.clonedCategoryList[category.id];
  }


  // #Pagination
  public handlePagination(paginationData: any): void {
    this.page = paginationData.page +1;
    this.pageSize = paginationData.rows;
    this.getCategories();
  }


  onReject() {
    this.messageService.clear('c');
  }

  showConfirm(selectedCompany: CategoryModel) {
    this.messageService.clear();
    this.messageService.add({key: 'c', sticky: true, severity:'warn', summary:'Silmek istediğinizden emin misiniz ?', detail:'Devam etmek için onaylayın'});
  }



  newRow() {
    return {
      id: ' '+this.rowCounter++,
      name: ''
    };
  }
}

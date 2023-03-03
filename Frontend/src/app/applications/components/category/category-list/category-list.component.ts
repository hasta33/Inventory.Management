import {Component, OnInit, ViewChild} from '@angular/core';
import {CategoryModel} from "../../../models/category/category";
import {CategoryService} from "../../../service/category/category.service";
import {MenuItem, MessageService, PrimeNGConfig, SelectItem} from "primeng/api";
import {constants} from "../../../constants/constants";
import {Table} from "primeng/table";
import {CompanyService} from "../../../service/company/company.service";
import {CompanyModel} from "../../../models/company/company";
import {CategorySubModel} from "../../../models/category/categorysub";

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

  //@ViewChild(Table, { read: Table }) pTable: Table | any;

  //#table
  categoryList: CategoryModel[] = [];
  columns: any; //[] | any; //burası sonra silinecek
  page: number = 1;
  pageSize: number = 100;
  loading: boolean = true;
  totalRecords: number = 0;

  //overlay menu
  companies: CompanyModel[] = [];
  selectedCompany: any;
  selectedCompanyId: any;


  //#contextMenu
  contextMenu: MenuItem[] = [];
  selectedCategory: CategoryModel | any;

  clonedCategoryList: { [s: string]: CategoryModel } = {};
  clonedCategorySubList: { [s: string]: CategorySubModel } = {};
  rowCounter: number = 0;


  ngOnInit() {
    this.getCompanyAllList();

    //ripple
    this.primengConfig.ripple = true;


    //#contextMenu
    this.contextMenu = [
      {label: 'Yenile', icon: 'pi pi-fw pi-refresh', command: () => this.getCategoryList() },
      {label: 'Kategori sil', icon: 'pi pi-fw pi-times', command: () => this.showConfirm(this.selectedCategory)}
    ];
  }

  //overlay
  onRowSelect(event: any) {
    this.messageService.add({severity: 'info', summary: 'Seçilen şirket', detail: event.data.name});
    this.selectedCompanyId = event.data.id;

    this.getCategoryList();
  }

  //#GetCompanyAllList {page}/{pageSize}
  getCompanyAllList() {
    this.companyService.getCompanyAllList(this.page, this.pageSize).subscribe({
      next: (data) => {
        this.companies = data?.data;
        this.totalRecords = data?.data[0].totalCount;
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

  //#GetCategoryList {companyId}/{page}/{pageSize}
  getCategoryList() {
    this.categoryService.getCategoryList(this.selectedCompanyId, this.page, this.pageSize).subscribe({
      next: (data) => {
        this.categoryList = data?.data;
        this.totalRecords = data.data[0]?.totalCount;
      },
      error: (e) => {
        this.messageService.add({severity:'error', summary: 'Hata', detail: `Kategori listesi alınamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME});
        this.messageService.clear('c');
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
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
      });
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
  //Edit categorySub
  onRowEditInitCategorySub(categorySub: CategorySubModel) {
    this.clonedCategorySubList[categorySub.id] = { ...categorySub };
  }

  //#Save
  onRowEditSave(category: CategoryModel, index: number) {
    category.companyId = this.selectedCompany;

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
    this.getCategoryList();
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

/*
Bugs
1. deleteCategory() içinde, şirket seçilip, ardından bir tane kayıt yapılıp ve aynı kaydı tekrar sildiğimizde console'da
hata cıkıyor daha sonra kontrol edilmesi gerekiyor.

 */

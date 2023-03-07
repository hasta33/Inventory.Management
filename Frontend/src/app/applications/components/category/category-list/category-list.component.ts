import {Component, OnInit} from '@angular/core';
import {CategoryModel} from "../../../models/category/category";
import {CategoryService} from "../../../service/category/category.service";
import {MenuItem, MessageService, PrimeNGConfig} from "primeng/api";
import {constants} from "../../../constants/constants";
import {CompanyService} from "../../../service/company/company.service";
import {CompanyModel} from "../../../models/company/company";
import {CategorySubService} from "../../../service/category-sub/category-sub.service";
import {OverlayPanel} from "primeng/overlaypanel";
import {CategorySubModel} from "../../../models/category/categorysub";
import {elementAt} from "rxjs";

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss'],
  providers: [MessageService]
})
export class CategoryListComponent implements OnInit {
  constructor(
    private categoryService: CategoryService,
    private categorySubService: CategorySubService,
    private companyService: CompanyService,
    private messageService: MessageService,
    private primengConfig: PrimeNGConfig) { }

  //@ViewChild(Table, { read: Table }) pTable: Table | any;

  //#table
  categoryList: CategoryModel[] = [];
  columns: [] | any; //burası sonra silinecek
  cols: any;
  page: number = 1;
  pageSize: number = 100;
  loading: boolean = true;
  totalRecords: number = 0;

  //overlay menu
  companies: CompanyModel[] = [];
  selectedCompany: any;
  cloneSelectedCompany: any;

  //#contextMenu
  contextMenu: MenuItem[] = [];
  selectedCategory: CategoryModel | any;

  clonedCategoryList: { [s: string]: CategoryModel } = {};
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
    this.messageService.add({severity: 'info', summary: 'Şirket Seç', detail: event.data.name});
    this.selectedCompany = event.data;
    this.cloneSelectedCompany = event.data;

    this.getCategoryList();
  }

  // --------------------------- Company ---------------------------
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
  // --------------------------- Company ---------------------------



  // --------------------------- Category ---------------------------
  //#GetCategoryList {companyId}/{page}/{pageSize}
  getCategoryList() {
    this.categoryService.getCategoryList(this.selectedCompany.id, this.page, this.pageSize).subscribe({
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
  //addCategory
  addCategory(category: CategoryModel, index: number) {
    this.categoryService.postCategory(category)
      .subscribe({
        next: (response: any) => {
          this.categoryList.push(response.data);
          this.categoryList = [...this.categoryList];
        },
        complete: () => {
          this.messageService.add({severity: 'success', summary: 'Başarılı', detail: `${category.name} isimli kategori kaydedildi.`, life: constants.TOAST_SUCCESS_LIFETIME});
          this.messageService.clear('c');

          this.categoryList[index] = this.clonedCategoryList[category.id];
          delete this.clonedCategoryList[category.id];
        },
        error: (e) => {
          this.messageService.add({severity: 'error', summary: 'Hata', detail: `kayıt yapılamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME});
          this.messageService.clear('c');
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
  // --------------------------- Category ---------------------------




  // --------------------------- Table Row ---------------------------
  //#Edit
  onCategoryRowEditInit(category: CategoryModel) {
    this.clonedCategoryList[category.id] = { ...category };
  }
  //#Save
  onCategoryRowEditSave(category: CategoryModel, index: number) {
    category.companyId = this.selectedCompany.id;

    if (!category.id.toString().indexOf(' ')){
      this.addCategory(category, index);
    } else if (category.id.toString().indexOf(' ')) {
      this.putCategory(category);
    }
  }
  //#Cancel
  onCategoryRowEditCancel(category: CategoryModel, index: number) {
    this.categoryList[index] = this.clonedCategoryList[category.id];
    delete this.clonedCategoryList[category.id];
  }
  //#Pagination
  public handlePagination(paginationData: any): void {
    this.page = paginationData.page +1;
    this.pageSize = paginationData.rows;
    this.getCategoryList();
  }
// --------------------------- Table Row ---------------------------


  // --------------------------- Table Row - CategorySub ---------------------------
  onCategorySubRowEditInit(categorySub: CategorySubModel) {
    //this.clonedCategorySubList[categorySub.id] = { ...categorySub };
  }
  onCategorySubRowEditSave(categorySub: CategorySubModel, index: number) {
    console.log(categorySub)
    /*categorySub.companyId = this.selectedCompany.id;

    if (!categorySub.id.toString().indexOf(' ')){
      this.addCategory(categorySub, index);
    } else if (categorySub.id.toString().indexOf(' ')) {
      this.putCategory(categorySub);
    }*/
  }
  onCategorySubRowEditCancel(categorySub: CategorySubModel, index: number) {
    //this.categorySubList[index] = this.clonedCategorySubList[categorySub.id];
    //delete this.clonedCategorySubList[categorySub.id];
  }
  // --------------------------- Table Row - CategorySub ---------------------------


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

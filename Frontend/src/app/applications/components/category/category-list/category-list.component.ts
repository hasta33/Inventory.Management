import {Component, OnInit} from '@angular/core';
import {CategoryModel} from "../../../models/category/category";
import {CategoryService} from "../../../service/category/category.service";
import {MenuItem, MessageService, PrimeNGConfig} from "primeng/api";
import {constants} from "../../../constants/constants";
import {CompanyService} from "../../../service/company/company.service";
import {CompanyModel} from "../../../models/company/company";
import {CategorySubService} from "../../../service/category-sub/category-sub.service";
import {CategorySubModel} from "../../../models/category/categorysub";

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss'],
  providers: [MessageService]
})
export class CategoryListComponent implements OnInit {
  //#table
  categoryList: CategoryModel[] = [];
  page: number = 1;
  pageSize: number = 100;
  loading: boolean = true;
  totalRecords: number = 0;

  //overlay menu
  companies: CompanyModel[] = [];
  selectedCompany: any;
  cloneSelectedCompany: any;

  //#contextMenu
  categoryContextMenu: MenuItem[] = [];
  selectedCategory: CategoryModel | any;
  selectedCategoryRowIndex: number = -1;
  clonedCategoryList: { [s: string]: CategoryModel } = {};
  rowCounter: number = 0;


  //categorySub
  clonedCategorySubList: { [s: string]: CategorySubModel } = {};
  categorySubContextMenu: MenuItem[] = [];
  selectedCategorySub: any; //CategorySubModel | any;
  selectedCategorySubRowIndex: number = -1;



  constructor(
    private categoryService: CategoryService,
    private categorySubService: CategorySubService,
    private companyService: CompanyService,
    private messageService: MessageService,
    private primengConfig: PrimeNGConfig) {
  }

  ngOnInit() {
    this.getCompanyAllList();

    //ripple
    this.primengConfig.ripple = true;

    //#contextMenu
    this.categoryContextMenu = [
      { label: 'Yenile', icon: 'pi pi-fw pi-refresh', command: () => this.getCategoryList() },
      { label: 'Kategori sil', icon: 'pi pi-fw pi-times', command: () => this.removeConfirm('category') }
    ];
    this.categorySubContextMenu = [
      { label: 'Yenile', icon: 'pi pi-fw pi-refresh', command: () => this.getCategoryList() },
      { label: 'Alt Kategori  sil', icon: 'pi pi-fw pi-times', command: () => this.removeConfirm( 'categorySub') }
    ];
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
        this.messageService.add({
          severity: 'error',
          summary: 'Hata',
          detail: `Şirket listesi alınamadı \n${e}`,
          life: constants.TOAST_ERROR_LIFETIME
        });
        this.messageService.clear('c');
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }
  // --------------------------- Company ---------------------------


  // --------------------------- Category - Table ---------------------------
  //#GetCategoryList {companyId}/{page}/{pageSize}
  getCategoryList() {
    this.categoryService.getCategoryList(this.selectedCompany.id, this.page, this.pageSize).subscribe({
      next: (data) => {
        this.categoryList = data?.data;
        this.totalRecords = data.data[0]?.totalCount;
      },
      error: (e) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Hata',
          detail: `Kategori listesi alınamadı \n${e}`,
          life: constants.TOAST_ERROR_LIFETIME
        });
        this.messageService.clear('c');
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }
  addCategory(category: CategoryModel, index: number) {
    category.id = category.id.toString().trim();

    this.categoryService.postCategory(category)
      .subscribe({
        next: (response: any) => {
          this.categoryList.push(response.data);
          this.categoryList = [...this.categoryList];
        },
        complete: () => {
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: `${category.name} isimli kategori kaydedildi.`,
            life: constants.TOAST_SUCCESS_LIFETIME
          });
          this.messageService.clear('c');

          //Remove temp item table
          const item = this.categoryList.find(item => item.id === category.id);
          if (item) {
            const itemIndex = this.categoryList.indexOf(item);
            this.categoryList.splice(itemIndex, 1);
          }
        },
        error: (e) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Hata',
            detail: `kayıt yapılamadı \n${e}`,
            life: constants.TOAST_ERROR_LIFETIME
          });
          this.messageService.clear('c');
        }
      });
  }
  putCategory(category: CategoryModel) {
    this.categoryService.putCategory(category).subscribe({
      next: (response) => {
      },
      error: (e) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Hata',
          detail: `Başarısız kayıt \n${e}`,
          life: constants.TOAST_ERROR_LIFETIME
        });
        this.messageService.clear('c');
      },
      complete: () => {
        this.messageService.add({
          severity: 'success',
          summary: 'Başarılı',
          detail: 'Kayıt güncellendi',
          life: constants.TOAST_SUCCESS_LIFETIME
        });
        this.messageService.clear('c');
      },
    });
  }
  deleteCategory() {
    this.categoryService.deleteCategory(this.selectedCategory.id)
      .subscribe({
        next: (response) => {
        },
        error: (e) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Hata',
            detail: `${this.selectedCategory.name} kategori silinemedi ${e}`,
            life: constants.TOAST_ERROR_LIFETIME
          });
          this.messageService.clear('c');
        },
        complete: () => {
          /*//this.categoryList = this.categoryList.filter((p) => p.id !== this.selectedCategory.id);//table uzerinden sil
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: `${this.selectedCategory.name} isimli şirket silindi`,
            life: constants.TOAST_SUCCESS_LIFETIME
          });
          this.messageService.clear('c');
          this.selectedCategory = null;  //null yap secileni*/

          //CategorySub delete array
          const categoryIndex = this.categoryList.findIndex(subCategory => subCategory.id === this.selectedCategory.id);
          if (categoryIndex > -1) {
            this.categoryList.splice(categoryIndex, 1);
          }
          this.categoryList = Object.assign([], this.categoryList);

          //Popup remove
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: `${this.selectedCategory.name} isimli alt kategori silindi`,
            life: constants.TOAST_SUCCESS_LIFETIME
          });
          this.messageService.clear('c');
          this.selectedCategory = null;
        }
      });
  }

  selectedCategoryEvent(event: any, data: any, index: any) {
    this.selectedCategory = data;
    this.selectedCategoryRowIndex = index;
  }

  onRowCategoryEditInit(category: any) {
    this.clonedCategoryList[category?.id] = {...category};
  }
  onRowCategoryEditSave(category: any, index: number) {
    category.companyId = this.selectedCompany.id;
    if (!category.id.toString().indexOf(' ')) {
      this.addCategory(category, index);
    } else if (category.id.toString().indexOf(' ')) {
      this.putCategory(category);
    }
  }
  onRowCategoryEditCancel(category: any, index: number) {
    this.categoryList[index] = this.clonedCategoryList[category?.id];
    delete this.clonedCategoryList[category?.id];
  }
  // --------------------------- Category ---------------------------






  // --------------------------- CategorySub - Table ---------------------------
  addCategorySub(categorySub: CategorySubModel, index: number) {
    categorySub.id = categorySub.id.toString().trim();
    this.categorySubService.postCategorySub(categorySub)
      .subscribe({
        next: (response: any) => {
          this.categoryList[this.selectedCategoryRowIndex].categorySubs.push(response.data);
          this.categoryList = [...this.categoryList];
        },
        complete: () => {
          this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: `${categorySub.name} isimli alt kategori kaydedildi.`,life: constants.TOAST_SUCCESS_LIFETIME });
          this.messageService.clear('c');

          //Remove temp item table
          const item = this.categoryList[this.selectedCategoryRowIndex].categorySubs.find(item => item.id === categorySub.id);
          if (item) {
            const itemIndex = this.categoryList[this.selectedCategoryRowIndex].categorySubs.indexOf(item);
            this.categoryList[this.selectedCategoryRowIndex].categorySubs.splice(itemIndex, 1);
          }
        },
        error: (e) => {
          this.messageService.add({ severity: 'error', summary: 'Hata', detail: `kayıt yapılamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME });
          this.messageService.clear('c');
        }
      })
  }
  putCategorySub(categorySub: CategorySubModel) {
    this.categorySubService.putCategorySub(categorySub)
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
      })
  }
  deleteCategorySub() {
    this.categorySubService.deleteCategorySub(this.selectedCategorySub.id)
      .subscribe({
        next: (response) => {

        },
        complete: () => {
          //CategorySub delete array
          /*const subCategoryIndex = this.categoryList[0].categorySubs.findIndex(subCategory => subCategory.id === this.selectedCategorySub.id);
          if (subCategoryIndex > -1) {
            this.categoryList[0].categorySubs.splice(subCategoryIndex, 1);
          }
          this.categoryList = Object.assign([], this.categoryList);*/

          //Remove temp item table
          const item = this.categoryList[this.selectedCategoryRowIndex].categorySubs.find(item => item.id === this.selectedCategorySub.id);
          if (item) {
            const itemIndex = this.categoryList[this.selectedCategoryRowIndex].categorySubs.indexOf(item);
            this.categoryList[this.selectedCategoryRowIndex].categorySubs.splice(itemIndex, 1);
          }

          //Popup remove
          this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: `${this.selectedCategory.name} isimli alt kategori silindi`, life: constants.TOAST_SUCCESS_LIFETIME });
          this.messageService.clear('c');
          this.selectedCategorySub = null; //null yap secileni*/
        },
        error: (e) => {
          this.messageService.add({ severity: 'error', summary: 'Hata', detail: `${this.selectedCategory.name} alt kategori silinemedi ${e}`, life: constants.TOAST_ERROR_LIFETIME });
          this.messageService.clear('c');
        }
      })
  }

  selectedCategorySubEvent(event: any, data: any, rowIndex: number) {
    this.selectedCategorySub = data;
    this.selectedCategorySubRowIndex = rowIndex;
  }

  onRowCategorySubEditInit(category: any) {
    this.clonedCategorySubList[category?.id] = {...category};
  }
  onRowCategorySubEditSave(category: any, index: any) {
    category.companyId = this.selectedCompany.id;
    category.categoryId = this.selectedCategory.id;

    if (!category.id.toString().indexOf(' ')) {
      this.addCategorySub(category, index);
    } else if (category.id.toString().indexOf(' ')) {
      this.putCategorySub(category);
    }
  }
  onRowCategorySubEditCancel(category: any, index: number) {
    this.categoryList[index] = this.clonedCategoryList[category?.id];
    delete this.clonedCategoryList[category?.id];
  }
  // --------------------------- CategorySub ---------------------------



  // --------------------------- General Definition ---------------------------
  onRowSelect(event: any) {
    this.messageService.add({severity: 'info', summary: 'Şirket Seç', detail: event.data.name});
    this.selectedCompany = event.data;
    this.cloneSelectedCompany = event.data;

    this.getCategoryList();
  }
  public handlePagination(paginationData: any): void {
    this.page = paginationData.page + 1;
    this.pageSize = paginationData.rows;
    this.getCategoryList();
  }
  onReject() {
    this.messageService.clear('c');
  }
  removeConfirm(selectedType: string) {
    this.messageService.clear();
    this.messageService.add({
      key: 'c',
      sticky: true,
      severity: 'warn',
      summary: 'Silmek istediğinizden emin misiniz ?',
      data: `${selectedType}`,
      detail: 'Devam etmek için onaylayın'
    });
  }
  newRow() {
    return {
      id: ' ' + this.rowCounter++,
      name: ''
    };
  }
  // --------------------------- General Definition ---------------------------
}

/*
public deleteSubCategory(subCategoryId: number) {

  const subCategoryIndex = this.categoryList[0].categorySubs.findIndex(subCategory => subCategory.id === subCategoryId);

  // if a matching id item is available then above code will return an index 0 or greater

  if (subCategoryIndex > -1) {
    this.categoryList[0].categorySubs.splice(subCategoryIndex, 1);
  }

  // some UI component libraries require you to reconstruct the array,
  // to pick up the new changes in the array
  // in that case you have to do one more step

  this.categoryList = Object.assign([], this.categoryList);
}

-----
table remove row
          const subCategoryIndex = this.categoryList[index].categorySubs.findIndex(subCategory => subCategory.id === categorySub.id);
          if (subCategoryIndex > -1) {
            this.categoryList[index].categorySubs.splice(subCategoryIndex, 1);
          }
          this.categoryList = Object.assign([], this.categoryList);

//Remove temp item table
const item = this.categoryList[index].categorySubs.find(category => category.id === categorySub.id);
if (item) {
  const itemIndex = this.categoryList[index].categorySubs.indexOf(item);
  this.categoryList[index].categorySubs.splice(itemIndex, 1);
}


------------------------------------------------------------------------------------------------------------------------
Bugs
1. deleteCategory() içinde, şirket seçilip, ardından bir tane kayıt yapılıp ve aynı kaydı tekrar sildiğimizde console'da
hata cıkıyor daha sonra kontrol edilmesi gerekiyor.

2. Yeni ana kategori satırı eklendi ve hemen ardından "Cancel - onCategoryRowEditCancel" butonuna bastıktan sonra chrome consol'da hata ortaya çkıyor
düzeltilecek
 */

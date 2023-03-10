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
  constructor(
    private categoryService: CategoryService,
    private categorySubService: CategorySubService,
    private companyService: CompanyService,
    private messageService: MessageService,
    private primengConfig: PrimeNGConfig) { }


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

  //categorySub
  categorySubList: CategorySubModel[] = [];
  clonedCategorySubList: { [s: string]: CategorySubModel } = {};
  categorySubContextMenu: MenuItem[] = [];
  selectedCategorySub: any;




  ngOnInit() {
    this.getCompanyAllList();

    //ripple
    this.primengConfig.ripple = true;

    //#contextMenu
    this.contextMenu = [
      {label: 'Yenile', icon: 'pi pi-fw pi-refresh', command: () => this.getCategoryList() },
      {label: 'Kategori sil', icon: 'pi pi-fw pi-times', command: () => this.showConfirm(this.selectedCategory)}
    ];

    this.categorySubContextMenu = [
      { label: 'Yenile', icon: 'pi pi-fw pi-refresh', command: () => this.getCategoryList() },
      {label: 'Alt Kategori  sil', icon: 'pi pi-fw pi-times', command: () => this.showConfirmCategorySubDelete()}
    ]
  }


  //overlay
  onRowSelect(event: any) {
    this.messageService.add({severity: 'info', summary: 'Şirket Seç', detail: event.data.name});
    this.selectedCompany = event.data;
    this.cloneSelectedCompany = event.data;

    this.getCategoryList();
  }

  selectedCategoryEvent(event: any) {
    this.selectedCategory = event;
  }
  selectedCategorySubEvent(event: any, data: any) {
    this.selectedCategorySub = data;
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
    category.id = category.id.toString().trim();

    this.categoryService.postCategory(category)
      .subscribe({
        next: (response: any) => {
          this.categoryList.push(response.data);
          this.categoryList = [...this.categoryList];
          console.log(this.categoryList)
          },
        complete: () => {
          this.messageService.add({severity: 'success', summary: 'Başarılı', detail: `${category.name} isimli kategori kaydedildi.`, life: constants.TOAST_SUCCESS_LIFETIME});
          this.messageService.clear('c');

          this.categoryList = this.categoryList.filter((p) => p.id !== category.id);

          //Tablo üzerinden bu şekilde silinmesi gerekiyor, yukarıdaki geçici çözümdür
          //this.categoryList[index] = this.clonedCategoryList[category.id];
          //delete this.clonedCategoryList[category.id];
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




  // --------------------------- CategorySub ---------------------------
  addCategorySub(categorySub: CategorySubModel, index: number) {
    categorySub.id = categorySub.id.toString().trim();

    this.categorySubService.postCategorySub(categorySub)
      .subscribe({
        next: (response: any) => {
          this.categorySubList.push(response.data);
          this.categorySubList = [...this.categorySubList];
        },
        complete: () => {
          this.messageService.add({severity: 'success', summary: 'Başarılı', detail: `${categorySub.name} isimli alt kategori kaydedildi.`, life: constants.TOAST_SUCCESS_LIFETIME});
          this.messageService.clear('c');

          this.categorySubList[index] = this.clonedCategorySubList[categorySub.id];
          delete this.clonedCategorySubList[categorySub.id];
        },
        error: (e) => {
          this.messageService.add({severity: 'error', summary: 'Hata', detail: `kayıt yapılamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME});
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
          this.messageService.add({severity:'success', summary: 'Başarılı', detail: 'Kayıt güncellendi', life: constants.TOAST_SUCCESS_LIFETIME});
          this.messageService.clear('c');
        },
        error: (e) => {
          this.messageService.add({severity:'error', summary: 'Hata', detail: `Başarısız kayıt \n${e}`, life: constants.TOAST_ERROR_LIFETIME});
          this.messageService.clear('c');
        }
      })
  }
  deleteCategorySub() {

    //console.log(this.selectedCategorySub)
    this.categoryList = this.categoryList.filter((x) => x.id !== this.selectedCategorySub.id);
    //console.log(this.categoryList)
    console.log(this.categoryList.filter((x) => x.id !== this.selectedCategorySub.id))

    //delete this.categoryList[2];
    //console.log(this.categoryList)

    //this.companyList[index] = this.clonedCompanyList[company.id];
    //delete this.clonedCompanyList[company.id];

    /*this.categorySubService.deleteCategorySub(this.selectedCategorySub.id)
      .subscribe({
        next: (response) => {

        },
        complete: () => {
          //this.categorySubList = this.categorySubList.filter((p) => p.id !== this.selectedCategorySub.id);//table uzerinden sil
          //this.messageService.add({severity: 'success', summary: 'Başarılı', detail: `${this.selectedCategory.name} isimli şirket silindi`, life: constants.TOAST_SUCCESS_LIFETIME});
          //this.messageService.clear('c');
          //this.selectedCategorySub = null; //null yap secileni

          this.categoryList = this.categoryList.filter((p) => p.id !== this.selectedCategorySub.id);//table uzerinden sil
          this.messageService.add({severity: 'success', summary: 'Başarılı', detail: `${this.selectedCategory.name} isimli şirket silindi`, life: constants.TOAST_SUCCESS_LIFETIME});
          this.messageService.clear('c');
          //this.selectedCategorySub = null; //null yap secileni

          console.log(this.selectedCategorySub)
          console.log(this.selectedCategorySub.id)
          console.log(this.categoryList)
          console.log(this.categorySubList)
        },
        error: (e) => {
          this.messageService.add({severity: 'error', summary: 'Hata', detail: `${this.selectedCategory.name} alt kategori silinemedi ${e}`, life: constants.TOAST_ERROR_LIFETIME});
          this.messageService.clear('c');
        }
      })*/
  }
  // --------------------------- CategorySub ---------------------------



  // --------------------------- Table Row ---------------------------
  //#Edit
  onCategoryRowEditInit(category: CategoryModel, selectedType: string) {
    if (selectedType === 'category') {
      this.clonedCategoryList[category?.id] = { ...category };
    } else if (selectedType === 'categorySub') {
      console.log("category sub edit ekranı")
    }
  }
  //#Save
  onCategoryRowEditSave(category: any, index: number, selectedType: string) {
    category.companyId = this.selectedCompany.id;
    category.categoryId = this.selectedCategory.id;


    if (selectedType === 'category') {
      if (!category.id.toString().indexOf(' ')){
        this.addCategory(category, index);
      } else if (category.id.toString().indexOf(' ')) {
        this.putCategory(category);
      }
    } else if (selectedType === 'categorySub') {
      if (!category.id.toString().indexOf(' ')) {
        this.addCategorySub(category, index);
      } else if (category.id.toString().indexOf(' ')) {
        this.putCategorySub(category);
      }
    }
  }
  //#Cancel
  onCategoryRowEditCancel(category: CategoryModel, index: number, selectedType: string) {
    if (selectedType === 'category') {
      this.categoryList[index] = this.clonedCategoryList[category?.id];
      delete this.clonedCategoryList[category?.id];
    } else if (selectedType === 'categorySub') {
      console.log("category sub cancel edildi")
    }
  }

  //#Pagination
  public handlePagination(paginationData: any): void {
    this.page = paginationData.page +1;
    this.pageSize = paginationData.rows;
    this.getCategoryList();
  }
// --------------------------- Table Row ---------------------------



  onReject() {
    this.messageService.clear('c');
  }

  showConfirm(selectedCompany: CategoryModel) {
    this.messageService.clear();
    this.messageService.add({key: 'c', sticky: true, severity:'warn', summary:'Silmek istediğinizden emin misiniz ?', detail:'Devam etmek için onaylayın'});
  }

  showConfirmCategorySubDelete() {
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
------------------------------------------------------------------------------------------------------------------------
Bugs
1. deleteCategory() içinde, şirket seçilip, ardından bir tane kayıt yapılıp ve aynı kaydı tekrar sildiğimizde console'da
hata cıkıyor daha sonra kontrol edilmesi gerekiyor.

2. Yeni ana kategori satırı eklendi ve hemen ardından "Cancel - onCategoryRowEditCancel" butonuna bastıktan sonra chrome consol'da hata ortaya çkıyor
düzeltilecek
 */

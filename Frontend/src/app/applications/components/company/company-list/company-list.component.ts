import {Component, OnInit, ViewChild} from '@angular/core';
import {CompanyModel} from "../../../models/company/company";
import {CompanyService} from "../../../service/company/company.service";
import {MenuItem, MessageService, PrimeNGConfig} from "primeng/api";
import {constants} from "../../../constants/constants";
import {Table} from "primeng/table";

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.scss'],
  providers: [MessageService]
})
export class CompanyListComponent implements OnInit {
  constructor(
    private companyService: CompanyService,
    private messageService: MessageService,
    private primengConfig: PrimeNGConfig) { }

  @ViewChild(Table, { read: Table }) pTable: Table | any;

  //#table
  companyList: CompanyModel[] = [];
  columns: [] | any;
  page: number = 1;
  pageSize: number = 10;
  loading: boolean = true;
  totalRecords: number = 0;

  //#contextMenu
  itemsMenu: MenuItem[] = [];
  selectedCompany: CompanyModel | any;

  clonedCompanyList: { [s: string]: CompanyModel } = {};
  rowCounter: number = 0;


  ngOnInit() {
    this.primengConfig.ripple = true;

    this.columns = [
      {field: 'id', header: 'id'},
      {field: 'name', header: 'name'},
      {field: 'businessCode', header: 'businessCode'},
      {field: 'description', header: 'description'},
      {field: 'createdDate', header: 'createdDate'},
      {field: 'updatedDate', header: 'updatedDate'},
    ]

    this.getCompanies();

    this.itemsMenu = [
      {label: 'Yenile', icon: 'pi pi-fw pi-refresh', command: () => this.getCompanies()},
      {label: 'Şirketi sil', icon: 'pi pi-fw pi-times', command: () => this.showConfirm(this.selectedCompany)}
    ];
  }


  //#Get companies list {page}/{pageSize}
  getCompanies() {
    this.companyService.getCompanyList(this.page, this.pageSize).subscribe({
      next: (data) => {
        this.companyList = data.data;
        this.totalRecords = data.data[0].totalCount;
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

  //#putCompany
  putCompany(company: CompanyModel) {
    this.companyService.putCompany(company).subscribe({
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
  deleteCompany() {
    this.companyService.deleteCompany(this.selectedCompany.id)
      .subscribe({
        next: (response) => {

        },
        error: (e) => {
          this.messageService.add({severity: 'error', summary: 'Hata', detail: `${this.selectedCompany.name}  şirket'i silinemedi ${e}`, life: constants.TOAST_ERROR_LIFETIME});
          this.messageService.clear('c');
        },
        complete: () => {
          this.companyList = this.companyList.filter((p) => p.id !== this.selectedCompany.id);//table uzerinden sil
          this.messageService.add({severity: 'success', summary: 'Başarılı', detail: `${this.selectedCompany.name} isimli şirket silindi`, life: constants.TOAST_SUCCESS_LIFETIME});
          this.messageService.clear('c');
          this.selectedCompany = null;  //null yap secileni
        }
        })
    }

  addCompany(company: CompanyModel, index: number) {
    this.companyService.postCompany(company)
      .subscribe({
        next: (response: any) => {
          this.companyList.push(response.data);
          this.companyList = [...this.companyList];
        },
        error: (e) => {
          this.messageService.add({severity: 'error', summary: 'Hata', detail: `kayıt yapılamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME});
          this.messageService.clear('c');
        },
        complete: () => {
          this.messageService.add({severity: 'success', summary: 'Başarılı', detail: `${company.name} isimli şirket kaydedildi.`, life: constants.TOAST_SUCCESS_LIFETIME});
          this.messageService.clear('c');
          this.companyList[index] = this.clonedCompanyList[company.id];
          delete this.clonedCompanyList[company.id];
        }
      });
  }

  //#Edit
  onRowEditInit(company: CompanyModel) {
    this.clonedCompanyList[company.id] = { ...company };
  }

  //#Save
  onRowEditSave(company: CompanyModel, index: number) {
    if (!company.id.toString().indexOf(' ')){
      this.addCompany(company, index);
    } else if (company.id.toString().indexOf(' ')) {
      this.putCompany(company);
    }
  }


  //#Cancel
  onRowEditCancel(company: CompanyModel, index: number) {
    this.companyList[index] = this.clonedCompanyList[company.id];
    delete this.clonedCompanyList[company.id];
  }


  // #Pagination
  public handlePagination(paginationData: any): void {
    this.page = paginationData.page +1;
    this.pageSize = paginationData.rows;
    this.getCompanies();
  }


  onReject() {
    this.messageService.clear('c');
  }

  showConfirm(selectedCompany: CompanyModel) {
    this.messageService.clear();
    this.messageService.add({key: 'c', sticky: true, severity:'warn', summary:'Silmek istediğinizden emin misiniz ?', detail:'Devam etmek için onaylayın'});
  }



  newRow() {
    return {
      id: ' '+this.rowCounter++,
      name: '',
      description: '',
      businessCode: ''
    };
  }
}

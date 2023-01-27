import {Component, OnInit} from '@angular/core';
import {CompanyModel} from "../../../models/company/company";
import {CompanyService} from "../../../service/company/company.service";
import {MenuItem, MessageService} from "primeng/api";
import {constants} from "../../../constants/constants";
import {debounceTime} from "rxjs";

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
  ) {
  }
  //#table
  companyList: CompanyModel[] = [];
  columns: [] | any;
  page: number = 1;
  pageSize: number = 2;
  loading: boolean = true;
  totalRecords: number =0;

  //#contextMenu
  itemsMenu: MenuItem[] = [];
  selectedCompany: CompanyModel | any;


  ngOnInit() {
    this.columns = [
      {field: 'id', header: 'id'},
      {field: 'name', header: 'name'},
      {field: 'description', header: 'description'},
      {field: 'businessCode', header: 'businessCode'},
      {field: 'createdDate', header: 'createdDate'},
      {field: 'updatedDate', header: 'updatedDate'},
    ]

    this.getCompanies();

    this.itemsMenu = [
      {label: 'Şirketi sil', icon: 'pi pi-fw pi-times', command: () => this.deleteCompany(this.selectedCompany)}
    ];
  }

  //#Get companies list
  getCompanies() {
    this.companyService.getCompanyList(this.page, this.pageSize).subscribe({
      next: (data) => {
        this.companyList = data.data;
        this.totalRecords = data.data[0].totalCount;
      },
      error: (e) => {
        this.messageService.add({severity:'error', summary: 'Hata', detail: `Şirket listesi alınamadı ${e}`, life: constants.TOAST_ERROR_LIFETIME});
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }

  //#Delete company
  deleteCompany(event: any) {
    this.companyService.deleteCompany(event.id).subscribe({
      next: (data) => {
        console.log("next" + data);
      },
      error: (e) => {
        this.messageService.add({severity: 'error', summary: 'Hata', detail: `Şirket silinemedi ${e}`, life: constants.TOAST_ERROR_LIFETIME})
      },
      complete: () => {
        this.messageService.add({severity: 'success', summary: 'Başarılı', detail: `${event.name} isimli şirket silindi`, life: constants.TOAST_SUCCESS_LIFETIME});
      }
    })
  }


  //#Edit
  onRowEditInit(company: CompanyModel) {
    //console.log("edit tıklandı");
    //console.log(company);
  }

  //#Save
  onRowEditSave(company: CompanyModel) {
    this.companyService.putCompany(company).subscribe({
      next: (res) => {

      },
      error: (e) => {
        this.messageService.add({severity:'error', summary: 'Hata', detail: `Başarısız kayıt ${e}`, life: constants.TOAST_ERROR_LIFETIME});
        },
      complete: () => {
        this.messageService.add({severity:'success', summary: 'Başarılı', detail: 'Kayıt güncellendi', life: constants.TOAST_SUCCESS_LIFETIME});
      },
    });
  }

  //#Cancel
  onRowEditCancel(company: CompanyModel, index: number) {
    //console.log(company, index);
  }


  // #Pagination
  public handlePagination(paginationData: any): void {
    this.page = paginationData.page +1;
    this.pageSize = paginationData.rows;

    this.getCompanies();
  }
}

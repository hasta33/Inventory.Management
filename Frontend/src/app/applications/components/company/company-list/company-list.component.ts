import {Component, OnInit} from '@angular/core';
import {CompanyModel} from "../../../models/company/company";
import {CompanyService} from "../../../service/company/company.service";

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.scss']
})
export class CompanyListComponent implements OnInit {
  constructor(private companyService: CompanyService) {
  }
  companies: CompanyModel[] = [];
  cols: [] | any;

  ngOnInit() {
    this.companyService.getCompanyList().subscribe(data => {
      console.log(data.data);
      this.companies = data.data;
    });

    this.cols = [
      {field: 'id', header: 'id'},
      {field: 'name', header: 'name'},
      {field: 'description', header: 'description'},
      {field: 'businessCode', header: 'businessCode'},
      {field: 'createdDate', header: 'createdDate'},
      {field: 'updatedDate', header: 'updatedDate'},
    ]
  }

  onRowEditInit(company: CompanyModel) {
    console.log("edit tıklandı");
    console.log(company);
  }

  onRowEditSave(company: CompanyModel) {
    console.log("edit save tıklandı");
    console.log(company);
    this.companyService.putCompany(company).subscribe(data => {
      console.log(data)
    });
  }

  onRowEditCancel(company: CompanyModel, index: number) {
    console.log("edit cancel tıklandı");
    console.log(company, index);
  }


}

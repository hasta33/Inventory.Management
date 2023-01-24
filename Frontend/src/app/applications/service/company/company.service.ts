import { Injectable } from '@angular/core';
import {environment} from "../../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {CompanyModel} from "../../models/company/company";
import {catchError, Observable, retry, tap, throwError} from "rxjs";
import {constants} from "../../constants/constants";

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private gatewayUrl: string = `${environment.gatewayUrl}`;

  constructor(private httpClient: HttpClient) { }


  getCompanyList() {
    return this.httpClient.get<{data: CompanyModel[]}>(constants.GET_COMPANY_LIST).pipe(tap(x => {
      console.log("dsfdsfdsf");
    }));
  }

  putCompany(data: any): Observable<CompanyModel> {
    console.log("put alanına girdi");

    return this.httpClient
      .put<any>(constants.PUT_COMPANY, data)
      .pipe(retry(2), catchError(this.handleError));
  }


  private handleError(error: any) {
    let errorMessage = '';
    if (error.errorMessage instanceof ErrorEvent) {
      errorMessage = error.error.errorMessage;
    } else {
      errorMessage = `\nDurum kodu: ${error.status}}\nCevap: ${error.message}`;
    }
    //window.alert(errorMessage);
    return throwError(() => {
      return errorMessage;
    });
  }

}

import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CompanyModel} from "../../models/company/company";
import {catchError, Observable, retry, throwError} from "rxjs";
import {constants} from "../../constants/constants";

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private httpClient: HttpClient) { }


  getCompanyList(page: number, pageSize: number, businessCode: number) {
    return this.httpClient
      .get<{data: CompanyModel[]}>(constants.GET_COMPANY_LIST_URL+`/${page}/${pageSize}/${businessCode}`)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }



  postCompany(data: any): Observable<CompanyModel> {
    return this.httpClient
      .post<any>(constants.POST_COMPANY_URL, data)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  putCompany(data: any): Observable<CompanyModel> {
    return this.httpClient
      .put<any>(constants.PUT_COMPANY_URL, data)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  deleteCompany(id: Number) {
    return this.httpClient
      .delete<any>(constants.DELETE_COMPANY_URL+`/${id}`)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  private handleError(error: any) {
    let errorMessage = '';
    if (error.errorMessage instanceof ErrorEvent) {
      errorMessage = error.error.errorMessage;
    } else {
      errorMessage = `\nStatusCode: ${error.status}}\nResponse: ${error.message}`;
    }
    //window.alert(errorMessage);
    return throwError(() => {
      return errorMessage;
    });
  }

}

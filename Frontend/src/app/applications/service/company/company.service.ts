import {Injectable, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {CompanyModel} from "../../models/company/company";
import {catchError, Observable, retry, throwError} from "rxjs";
import {constants} from "../../constants/constants";
import {environment} from "../../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private httpClient: HttpClient) { }


  //#GetCompanyAllList {page}/{pageSize}
  getCompanyAllList(page: number, pageSize: number) {
    //let header = new HttpHeaders().set('Authorization', 'bearer '+ window.sessionStorage.getItem('access_permission_token'));

    return this.httpClient
      //.get<{data: CompanyModel[]}>(constants.GET_COMPANY_LIST_URL+`/${page}/${pageSize}`, { headers: header } )
      .get<{data: CompanyModel[]}>(constants.GET_COMPANY_LIST_URL+`/${page}/${pageSize}` )
      //, {headers: HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')}
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  //#GetCompanyListWithSubTables {companyId}/{page}/{pageSize}
  /*getCompanyListWithSubTables(companyId: number, page: number, pageSize:number){
    return this.httpClient
      .get<{data: CompanyModel[]}>(constants.GET_COMPANY_ONLY_NAME_AND_BUSINESS_CODE+`/${companyId}/${page}/${pageSize}/`)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }*/



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
    console.log(error.message)
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

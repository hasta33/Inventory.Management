import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BrandModel} from "../../models/brand/brand";
import {constants} from "../../constants/constants";
import {catchError, Observable, retry, throwError} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  constructor(private httpClient: HttpClient) { }


  //GetBrandList {companyId}/{page}/{pageSize}
  getBrandList(companyId: number, page: number, pageSize: number) {
    return this.httpClient
      .get<{data: BrandModel[]}>(constants.GET_BRAND_LIST_URL+`/${companyId}/${page}/${pageSize}`)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError))
  }

  postBrand(data: any): Observable<BrandModel> {
    return this.httpClient
      .post<any>(constants.POST_BRAND_URL, data)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  putBrand(data: any): Observable<BrandModel>{
    return this.httpClient
      .put<any>(constants.PUT_BRAND_URL, data)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  deleteBrand(id: Number) {
    return this.httpClient
      .delete<any>(constants.DELETE_BRAND_URL+`/${id}`)
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

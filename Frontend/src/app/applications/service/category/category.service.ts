import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CategoryModel} from "../../models/category/category";
import {catchError, Observable, retry, throwError} from "rxjs";
import {constants} from "../../constants/constants";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private httpClient: HttpClient) { }


  //GetCategoryList {companyId}/{page}/{pageSize}
  getCategoryList(companyId: number, page: number, pageSize: number) {
    return this.httpClient
      .get<{data: CategoryModel[]}>(constants.GET_CATEGORY_LIST_URL+`/${companyId}/${page}/${pageSize}/`)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }



  postCategory(data: any): Observable<CategoryModel> {
    return this.httpClient
      .post<any>(constants.POST_CATEGORY_URL, data)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  putCategory(data: any): Observable<CategoryModel> {
    return this.httpClient
      .put<any>(constants.PUT_CATEGORY_URL, data)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  deleteCategory(id: Number) {
    return this.httpClient
      .delete<any>(constants.DELETE_CATEGORY_URL+`/${id}`)
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

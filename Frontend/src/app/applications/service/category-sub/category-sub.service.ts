import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, Observable, retry, throwError} from "rxjs";
import {CategorySubModel} from "../../models/category/categorysub";
import {constants} from "../../constants/constants";

@Injectable({
  providedIn: 'root'
})
export class CategorySubService {

  constructor(private httpClient: HttpClient) { }

  postCategorySub(data: any): Observable<CategorySubModel> {
    return this.httpClient
      .post<any>(constants.POST_CATEGORY_SUB, data)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  putCategorySub(data: any): Observable<CategorySubModel>{
    return this.httpClient
      .put<any>(constants.PUT_CATEGORY_SUB_URL, data)
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

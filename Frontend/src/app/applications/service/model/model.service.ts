import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, Observable, retry, throwError} from "rxjs";
import {constants} from "../../constants/constants";
import {BrandSubModel} from "../../models/brand/brandsubmodel";

@Injectable({
  providedIn: 'root'
})
export class ModelService {

  constructor(private httpClient: HttpClient) { }

  postModel(data: any): Observable<BrandSubModel> {
    return this.httpClient
      .post<any>(constants.POST_MODEL, data)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  putModel(data: any): Observable<BrandSubModel>{
    return this.httpClient
      .put<any>(constants.PUT_MODEL_URL, data)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  deleteModel(id: number) {
    return this.httpClient
      .delete<any>(constants.DELETE_MODEL_URL+`/${id}`)
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

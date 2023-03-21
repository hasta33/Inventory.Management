import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, Observable, retry, throwError} from "rxjs";
import {InventoryModel} from "../models/inventory/inventory";
import {constants} from "../constants/constants";

@Injectable({
  providedIn: 'root'
})
export class InventoryService {

  constructor(private httpClient: HttpClient) { }

  getInventoryLis(companyId: number, page: number, pageSize: number) {
    return this.httpClient
      .get<{data: InventoryModel[]}>(constants.GET_INVENTORY_LIST_URL+`/${companyId}/${page}/${pageSize}`)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  postInventory(data: any): Observable<InventoryModel> {
    return this.httpClient
      .post<any>(constants.POST_INVENTORY_URL, data)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  putInventory(data: any): Observable<InventoryModel> {
    return this.httpClient
      .put<any>(constants.PUT_INVENTORY_URL, data)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  deleteInventory(id: Number) {
    return this.httpClient
      .delete<any>(constants.DELETE_INVENTORY_URL+`/${id}`)
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

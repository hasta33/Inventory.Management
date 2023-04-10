import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, Observable, retry, throwError} from "rxjs";
import {InventoryListParameters, InventoryModel} from "../../models/inventory/inventory";
import {constants} from "../../constants/constants";
import {PersonalListModel} from "../../models/personal-list/personal-list";

@Injectable({
  providedIn: 'root'
})
export class InventoryService {

  constructor(private httpClient: HttpClient) { }

  getInventoryList(companyId: number, page: number, pageSize: number) {
    return this.httpClient
      .get<{data: InventoryModel[]}>(constants.GET_INVENTORY_LIST_URL+`/${companyId}/${page}/${pageSize}`)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }

  /*getInventoryAllList(page: number, pageSize: number, params: any) {
    return this.httpClient
      .get<{data: InventoryModel[]}>(constants.GET_INVENTORY_LIST_URL+`/${page}/${pageSize}?${params}` )
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }*/
  /*getInventoryAllList(page: number, pageSize: number, parameters?: any) {
    //const resourceUrl = `/${page}/${pageSize}${buildQueryParameters(parameters)}`
    const resourceUrl = `/${page}/${pageSize}?${parameters}`
    console.log(parameters)
    console.log(resourceUrl)

    return this.httpClient
      .get<{data: InventoryModel[]}>(constants.GET_INVENTORY_LIST_URL + resourceUrl)
      .pipe(retry(constants.HTTP_SERVICE_RETRY), catchError(this.handleError));
  }*/
  getInventoryAllList(page: number, pageSize: number, parameters?: InventoryListParameters) {
    const resourceUrl = `/${page}/${pageSize}${buildQueryParameters(parameters)}`
    return this.httpClient
      .get<{data: InventoryModel[]}>(constants.GET_INVENTORY_LIST_URL + resourceUrl)
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

  //`${environment.gatewayUrl}/api/services/inventory/keycloak/user/`,
  getPersonalList(searchData: string) { //</users?username=test&first=5&max=10
    return this.httpClient
      .get<{data: PersonalListModel}>(constants.GET_PERSONAL_LIST + `users?search=${searchData}`)
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

function buildQueryParameters<T extends Record<string, string | boolean | number | (string | boolean | number)[]>>(queryParameters?: T): string {
  if (!queryParameters) return '';
  const params = Object
    .entries(queryParameters)
    .filter(([, value]) => value != null)
    .flatMap(([param, value]) => {
      if (Array.isArray(value)) {
        return value
          .filter(v => v != null || v!= undefined || v!= '')
          .map(v => `${encodeURIComponent(param)}=${encodeURIComponent(String(v))}`)
      }
      return [`${encodeURIComponent(param)}=${encodeURIComponent(String(value))}`];
    });
  return params.length ? `?${params.join('&')}` : '';
}

/*
function buildQueryParameters<T extends Record<string, string | boolean | number>>(queryParameters?: T): string {
  if (!queryParameters) return '';

  const params = Object
    .entries(queryParameters)
    .filter(([, value]) => value != undefined)
    .map(([param, value]) => {
      return `${encodeURIComponent(param)}=${encodeURIComponent(String(value))}`;
    });

  return params.length ? `?${params.join('&')}` : '';
}
*/

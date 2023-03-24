import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {InventoryMovementModel} from "../../models/inventory-movement/inventory-movement";
import {constants} from "../../constants/constants";
import {catchError, retry, throwError} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class InventoryMovementService {

  constructor(private httpClient: HttpClient) { }

  getInventoryMovement(inventoryId: number) {
    return this.httpClient
      .get<{data: InventoryMovementModel[]}>(constants.GET_INVENTORY_MOVEMENT_URL)
      .pipe(retry(constants.HTTP_SERVICE_RETRY),  catchError(this.handleError));
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

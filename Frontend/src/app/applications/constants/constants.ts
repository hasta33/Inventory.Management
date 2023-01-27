import {environment} from "../../../environments/environment";

export const constants = {
  GET_COMPANY_LIST_URL : `${environment.gatewayUrl}/api/services/inventory/company`,
  PUT_COMPANY_URL: `${environment.gatewayUrl}/api/services/inventory/company`,
  DELETE_COMPANY_URL: `${environment.gatewayUrl}/api/services/inventory/company`,

  TOAST_ERROR_LIFETIME:5000, //Toast timeout
  TOAST_SUCCESS_LIFETIME:3000,
  HTTP_SERVICE_RETRY:3, //Http servisine gonderilen istegin tekrarlanma istegi
}

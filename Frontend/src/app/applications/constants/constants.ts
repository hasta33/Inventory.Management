import {environment} from "../../../environments/environment";

export const constants = {
  //Company
  //GET_COMPANY_ONLY_NAME_AND_BUSINESS_CODE : `${environment.gatewayUrl}/api/services/inventory/company`, //GetCompanyOnlyNameAndBusinessCode
  GET_COMPANY_LIST_URL : `${environment.gatewayUrl}/api/services/inventory/company`, //{page}/{pageSize}
  POST_COMPANY_URL: `${environment.gatewayUrl}/api/services/inventory/company`,
  PUT_COMPANY_URL: `${environment.gatewayUrl}/api/services/inventory/company`,
  DELETE_COMPANY_URL: `${environment.gatewayUrl}/api/services/inventory/company`,

  //Category
  GET_CATEGORY_LIST_URL : `${environment.gatewayUrl}/api/services/inventory/category`,
  POST_CATEGORY_URL: `${environment.gatewayUrl}/api/services/inventory/category`,
  PUT_CATEGORY_URL: `${environment.gatewayUrl}/api/services/inventory/category`,
  DELETE_CATEGORY_URL: `${environment.gatewayUrl}/api/services/inventory/category`,


  //CategorySub
  POST_CATEGORY_SUB: `${environment.gatewayUrl}/api/services/inventory/categorySub`,
  PUT_CATEGORY_SUB_URL: `${environment.gatewayUrl}/api/services/inventory/categorySub`,
  DELETE_CATEGORY_SUB_URL: `${environment.gatewayUrl}/api/services/inventory/categorySub`,


  TOAST_ERROR_LIFETIME:5000, //Toast timeout
  TOAST_SUCCESS_LIFETIME:3000,
  HTTP_SERVICE_RETRY:3, //Http servisine gonderilen istegin tekrarlanma istegi
}

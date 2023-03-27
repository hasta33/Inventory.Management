import {environment} from "../../../environments/environment";

export const constants = {
  //Keycloak Address
  GET_PERSONEL_LIST : `${environment.gatewayUrl}/admin/realms/EneryaInventoryApiRealm`,



  //Company
  //GET_COMPANY_ONLY_NAME_AND_BUSINESS_CODE : `${environment.gatewayUrl}/api/services/inventory/company`, //GetCompanyOnlyNameAndBusinessCode
  GET_COMPANY_LIST_URL : `${environment.gatewayUrl}/api/services/inventory/company`, //{page}/{pageSize}
  POST_COMPANY_URL: `${environment.gatewayUrl}/api/services/inventory/company`,
  PUT_COMPANY_URL: `${environment.gatewayUrl}/api/services/inventory/company`,
  DELETE_COMPANY_URL: `${environment.gatewayUrl}/api/services/inventory/company`,

  //Category
  GET_CATEGORY_ALL_LIST_URL : `${environment.gatewayUrl}/api/services/inventory/category`,
  GET_CATEGORY_LIST_URL : `${environment.gatewayUrl}/api/services/inventory/category`,
  POST_CATEGORY_URL: `${environment.gatewayUrl}/api/services/inventory/category`,
  PUT_CATEGORY_URL: `${environment.gatewayUrl}/api/services/inventory/category`,
  DELETE_CATEGORY_URL: `${environment.gatewayUrl}/api/services/inventory/category`,


  //CategorySub
  POST_CATEGORY_SUB: `${environment.gatewayUrl}/api/services/inventory/categorySub`,
  PUT_CATEGORY_SUB_URL: `${environment.gatewayUrl}/api/services/inventory/categorySub`,
  DELETE_CATEGORY_SUB_URL: `${environment.gatewayUrl}/api/services/inventory/categorySub`,

  //Brand
  GET_BRAND_ALL_LIST_URL : `${environment.gatewayUrl}/api/services/inventory/brand`,
  GET_BRAND_LIST_URL : `${environment.gatewayUrl}/api/services/inventory/brand`,
  POST_BRAND_URL: `${environment.gatewayUrl}/api/services/inventory/brand`,
  PUT_BRAND_URL: `${environment.gatewayUrl}/api/services/inventory/brand`,
  DELETE_BRAND_URL: `${environment.gatewayUrl}/api/services/inventory/brand`,

  //Model
  POST_MODEL: `${environment.gatewayUrl}/api/services/inventory/model`,
  PUT_MODEL_URL: `${environment.gatewayUrl}/api/services/inventory/model`,
  DELETE_MODEL_URL: `${environment.gatewayUrl}/api/services/inventory/model`,



  GET_INVENTORY_LIST_URL : `${environment.gatewayUrl}/api/services/inventory/inventory`,
  POST_INVENTORY_URL: `${environment.gatewayUrl}/api/services/inventory/inventory`,
  PUT_INVENTORY_URL: `${environment.gatewayUrl}/api/services/inventory/inventory`,
  DELETE_INVENTORY_URL: `${environment.gatewayUrl}/api/services/inventory/inventory`,


  //Inventory Movement
  GET_INVENTORY_MOVEMENT_URL : `${environment.gatewayUrl}/api/services/inventory/inventorymovement`,


  TOAST_ERROR_LIFETIME:5000, //Toast timeout
  TOAST_SUCCESS_LIFETIME:3000,
  HTTP_SERVICE_RETRY:3, //Http servisine gonderilen istegin tekrarlanma istegi
}

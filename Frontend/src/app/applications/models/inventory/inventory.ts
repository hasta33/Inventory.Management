export class InventoryModel {
  id: number | any;
  name: string | any;
  companyId: number | any;
  categoryId: number | any;
  categorySubId: number | any;
  brandId: number | any;
  modelId: number | any;
  barcode: number | any;
  serialNumber: string | any;
  imei: number | any;
  mac: string | any;
  status: string | any;
  responsible: string | any;
  totalCount: number | any;

  inventoryDate: Date | undefined;
  invoiceDate: Date | undefined;

  createdDate: Date | undefined;
  updatedDate: Date | undefined;
}


export type InventoryListParameters = {
  companyId?: number;
  categoryId?: number;
  categorySubId?: number;
  brandId?: number;
  modelId?: number;
  name?: number;
  barcode?: number;
  serialNumber?: string;
  mac?: string;
  imei?: number;
  responsibleUser?: string;
  status?: string;
}

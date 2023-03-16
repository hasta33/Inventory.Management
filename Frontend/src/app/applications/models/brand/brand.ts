import {BrandSubModel} from "./brandsubmodel";

export class BrandModel {
  id: number | any;
  name: string | any;
  companyId: number | any;
  createdDate: Date | undefined;
  updatedDate: Date | undefined;
  totalCount: number | any;
  models: BrandSubModel[] = [];
}

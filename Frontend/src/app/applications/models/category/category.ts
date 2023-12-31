import {CategorySubModel} from "./categorysub";

export class CategoryModel {
  id: number | any;
  name: string | any;
  companyId: number | any;
  createdDate: Date | undefined;
  updatedDate: Date | undefined;
  totalCount: number | any;
  categorySubs: CategorySubModel[] = []; //any;
}

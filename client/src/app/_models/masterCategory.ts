import { SubCategory } from "./subCategory";

export interface MasterCategory
{
    id: number,
    categoryName: string,
    subCategories: SubCategory[]
}
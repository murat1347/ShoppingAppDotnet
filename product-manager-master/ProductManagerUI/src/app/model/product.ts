import {Category} from "./category";
import {BaseModel} from "./basemodel";

export class Product implements BaseModel {

  public static NameMaxLength = 32;
  public static NameMinLength = 2;
  public static ImageUrlMinLength = 4;
  public static ImageUrlMaxLength = 250;

  constructor(
    public id?: number,
    public imageUrl?:string,
    public name?: string,
    public category? : Category
  ) {}

  public toString = () : string => {
    return `Foo (id: ${this.id})`;
  }
}

export class ProductUpdateDTO implements BaseModel{

  constructor(
    public id?: number,
    public imageUrl?:string,
    public name?: string,
    public categoryId? : number
  ) {}

}

export class ProductPagedResult{

  constructor(
    public count: number,
    public products: Product[],
  ) {
  }

}

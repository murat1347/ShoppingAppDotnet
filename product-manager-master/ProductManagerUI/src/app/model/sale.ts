import {BaseModel} from "./basemodel";
import {Seller} from "./seller";
import {Product} from "./product";
import {Customer} from "./customer";

export class Sale implements BaseModel {

  constructor(
    public id?: number,
    public sellerFirstName?: string,
    public sellerLastName?: string,
    public productName?: string,
    public amount?: number,
    public cost?: number,
    public dateTime?: Date
  ) {
  }
}
export class SalePagedResult {

  constructor(
    public count: number,
    public sales: Sale[],
  ) {
  }
}


export class SaleEditDTO implements BaseModel{

  constructor(
    public id?: number,
    public amount?:string,
    public income?:number,
    public customerId? : number,
    public productId? : number,
    public dateTime? : string
  ) {}

}

export class SaleSingleResult {

  constructor(
    public id?: number,
    public amount?:string,
    public income?:number,
    public customer? : Customer,
    public product? : Product,
    public dateTime? : Date
  ) {
  }
}


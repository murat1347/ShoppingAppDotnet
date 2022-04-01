import {BaseModel} from "./basemodel";
import {Seller} from "./seller";
import {Product} from "./product";

export class Purchase implements BaseModel {

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

export class PurchaseEditDTO implements BaseModel{

  constructor(
    public id?: number,
    public amount?:string,
    public cost?:string,
    public sellerId? : number,
    public productId? : number,
    public dateTime? : string
) {}

}

export class PurchaseSingleResult {

  constructor(
    public id?: number,
    public amount?:string,
    public cost?:string,
    public seller? : Seller,
    public product? : Product,
    public dateTime? : Date
  ) {
  }
}


export class PurchasePagedResult {

  constructor(
    public count: number,
    public purchases: Purchase[],
  ) {
  }
}

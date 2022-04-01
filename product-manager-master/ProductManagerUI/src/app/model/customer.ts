import {Product} from "./product";
import {BaseModel} from "./basemodel";

export class Customer implements BaseModel {

  public static FirstNameMaxLength = 32;
  public static FirstNameMinLength = 2;

  public static LastNameMaxLength = 32;
  public static LastNameMinLength = 2;

  public static EmailMaxLength = 62;
  public static EmailMinLength = 2;

  public static PhoneMaxLength = 32;
  public static PhoneMinLength = 2;

  public static AddressMaxLength = 255;
  public static AddressMinLength = 10;

  constructor(
    public id?: number,
    public firstName?: string,
    public lastName?: string,
    public email?: string,
    public phone?: string,
    public address?: string
  ) {}

}


export class CustomerPagedResult{

  constructor(
    public count: number,
    public customers: Customer[],
  ) {
  }

}

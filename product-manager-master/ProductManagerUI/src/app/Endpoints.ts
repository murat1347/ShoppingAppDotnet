import { environment } from '../environments/environment';

export class Endpoints{

  static readonly URL = environment.apiUrl;
  static readonly API = Endpoints.URL + "/api/v1";

  static readonly Account= {
    root :()=> Endpoints.API + "/Account",
    register :()=> Endpoints.Account.root() + "/register",
    login : ()=> Endpoints.Account.root() + "/login",
  }

  static readonly Customer : AbstractEndpoint={
    Root : Endpoints.API + '/Customer',
    GetAll : () => Endpoints.API + "/Customer",
    GetOne : (id:number) => Endpoints.API + "/Customer/" + id,
    Update : (id:number) => Endpoints.API + "/Customer/" + id,
    Delete : (id:number) => Endpoints.Customer.Root  + "/" + id,
    Create : () =>  Endpoints.Customer.Root
  }

  static readonly Category = {
     GetAll : () => Endpoints.API + "/Category",
     GetOne : (id:number) => Endpoints.API + "/Category/" + id,
     Update : (id:number) => Endpoints.API + "/Category/" + id,
     Delete : (id:number) => Endpoints.API + "/Category/" + id,
     Create : () => Endpoints.API + "/Category"
  }

  static readonly Dashboard = {
    Get : () => Endpoints.API + "/Dashboard/",
    Invalidate : ()=> Endpoints.API + "/Dashboard/Invalidate"
  }

  static readonly Product : AbstractEndpoint = {
    Root : Endpoints.API + '/Product',
    GetAll : ()=> Endpoints.Product.Root,
    GetOne : (id:number) =>  Endpoints.Product.Root  + "/" + id,
    Update : (id:number) =>  Endpoints.Product.Root  + "/" + id,
    Delete : (id:number) => Endpoints.Product.Root  + "/" + id,
    Create : () =>  Endpoints.Product.Root
  }

  static readonly Seller : AbstractEndpoint ={
    Root : Endpoints.API + '/Seller',
    GetAll : () => Endpoints.API + "/Seller",
    GetOne : (id:number) => Endpoints.API + "/Seller/" + id,
    Update : (id:number) => Endpoints.API + "/Seller/" + id,
    Delete : (id:number) => Endpoints.Seller.Root  + "/" + id,
    Create : () =>  Endpoints.Seller.Root
  }

  static readonly Sale : AbstractEndpoint={
    Root : Endpoints.API + '/Sale',
    GetAll : () => Endpoints.API + "/Sale",
    GetOne : (id:number) => Endpoints.API + "/Sale/" + id,
    Update : (id:number) => Endpoints.API + "/Sale/" + id,
    Delete : (id:number) => Endpoints.Sale.Root  + "/" + id,
    Create : () =>  Endpoints.Sale.Root
  }

  static readonly Purchase : AbstractEndpoint={
    Root : Endpoints.API + '/Purchase',
    GetAll : () => Endpoints.API + "/Purchase",
    GetOne : (id:number) => Endpoints.API + "/Purchase/" + id,
    Update : (id:number) => Endpoints.API + "/Purchase/" + id,
    Delete : (id:number) => Endpoints.Purchase.Root  + "/" + id,
    Create : () =>  Endpoints.Purchase.Root
  }
}


export interface AbstractEndpoint{
  Root : string,
  GetAll() : string;
  GetOne(id:number) : string
  Update(id:number) : string
  Delete(id:number) : string
  Create() : string
}

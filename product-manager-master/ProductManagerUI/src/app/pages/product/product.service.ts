import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {Product, ProductPagedResult} from "../../model/product";
import {Endpoints} from "../../Endpoints";
import {SortDirection} from "@angular/material/sort";
import {HttpParamBuilder} from "../../helper/httpparambuilder";
import {AbstractService} from "../../helper/abstract.service";
import {ProductFilterFormControls} from "./product.component";

@Injectable({ providedIn: 'root' })
export class ProductService extends AbstractService<Product>{

  constructor(_http: HttpClient) {super(_http,Endpoints.Product)}

  getAll(sort : string, order: SortDirection, page:number, pageSize:number=30, productFormControl:ProductFilterFormControls) : Observable<ProductPagedResult>{

    const paramBuilder = new HttpParamBuilder(Endpoints.Product.GetAll())
      .set('pageNumber',page)
      .set('pageSize',pageSize)
      .set('id',productFormControl.id.value)
      .set('categoryId',productFormControl.categoryId.value)
      .set('productName',productFormControl.productName.value)
      .set('order',order)
      .set('sort',sort);

    return this._http.get<ProductPagedResult>(paramBuilder.getUrl());
  }

  findByName(productName : string) : Observable<ProductPagedResult>{
    const paramBuilder = new HttpParamBuilder(Endpoints.Product.GetAll())
      .set('productName',productName)
      .set('pageSize',10)
      .set('order','productName')

    return this._http.get<ProductPagedResult>(paramBuilder.getUrl());
  }

}

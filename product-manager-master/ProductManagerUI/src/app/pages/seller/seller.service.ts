import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SortDirection} from "@angular/material/sort";
import {CustomerFilterFormControls} from "../customer/customer.component";
import {Observable} from "rxjs";
import {CustomerPagedResult} from "../../model/customer";
import {HttpParamBuilder} from "../../helper/httpparambuilder";
import {Endpoints} from "../../Endpoints";
import {Seller, SellerPagedResult} from "../../model/seller";
import {AbstractService} from "../../helper/abstract.service";

@Injectable({
  providedIn: 'root'
})
export class SellerService extends AbstractService<Seller>{

  constructor(http: HttpClient) {
    super(http,Endpoints.Seller)
  }

  getAll(sort : string, order: SortDirection, page:number, pageSize:number=30, formControls: CustomerFilterFormControls) : Observable<SellerPagedResult>{

    const paramBuilder = new HttpParamBuilder(Endpoints.Seller.GetAll())
      .set('pageNumber',page)
      .set('pageSize',pageSize)
      .set('order',order)
      .set('sort',sort)
      .set('id',formControls.id.value)
      .set('firstName', formControls.firstName.value)
      .set('lastName',formControls.lastName.value)
      .set('phone',formControls.phone.value)
      .set('email',formControls.email.value)

    return this._http.get<SellerPagedResult>(paramBuilder.getUrl());
  }
}

import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SortDirection} from "@angular/material/sort";
import {Observable} from "rxjs";
import {HttpParamBuilder} from "../../helper/httpparambuilder";
import {Endpoints} from "../../Endpoints";
import {PurchaseFilterFormControls} from "./purchase.component";
import {Purchase, PurchasePagedResult, PurchaseSingleResult} from "../../model/purchase";
import {AbstractService} from "../../helper/abstract.service";

@Injectable({
  providedIn: 'root'
})
export class PurchaseService extends AbstractService<Purchase>{

  constructor(http: HttpClient) {
    super(http,Endpoints.Purchase)
  }

  getAll(sort : string, order: SortDirection, page:number, pageSize:number=30, formControls: PurchaseFilterFormControls) : Observable<PurchasePagedResult>{

    const paramBuilder = new HttpParamBuilder(Endpoints.Purchase.GetAll())
      .set('pageNumber',page)
      .set('pageSize',pageSize)
      .set('order',order)
      .set('sort',sort)
      .set('id',formControls.id.value)
      .set('sellerFirstName', formControls.sellerFirstName.value)
      .set('sellerLastName',formControls.sellerLastName.value)
      .set('productName',formControls.productName.value)
      .set('amountMin',formControls.amountMin.value)
      .set('amountMax',formControls.amountMax.value)
      .set('costMin',formControls.costMin.value)
      .set('costMax',formControls.costMax.value);

      if(formControls.dateTimeMin.value){
        paramBuilder.set('dateTimeMin',formControls.dateTimeMin.value.toUTCString().replace("UTC","GMT"))
      }

      if(formControls.dateTimeMax.value){
        paramBuilder.set('dateTimeMax',formControls.dateTimeMax.value.toUTCString().replace("UTC","GMT"))
      }

    return this._http.get<PurchasePagedResult>(paramBuilder.getUrl());
  }

  single(id: number) : Observable<PurchaseSingleResult>{
    return this._http.get<PurchaseSingleResult>(this._endpoint.GetOne(id));
  }
}

import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SortDirection} from "@angular/material/sort";
import {PurchaseFilterFormControls} from "../purchase/purchase.component";
import {Observable} from "rxjs";
import {PurchasePagedResult, PurchaseSingleResult} from "../../model/purchase";
import {HttpParamBuilder} from "../../helper/httpparambuilder";
import {Endpoints} from "../../Endpoints";
import {Sale, SalePagedResult, SaleSingleResult} from "../../model/sale";
import {SaleFilterFormControls} from "./sale.component";
import {AbstractService} from "../../helper/abstract.service";

@Injectable({
  providedIn: 'root'
})
export class SaleService extends AbstractService<Sale>{

  constructor(http: HttpClient) {
    super(http,Endpoints.Sale)
  }

  getAll(sort : string, order: SortDirection, page:number, pageSize:number=30, formControls:SaleFilterFormControls) : Observable<SalePagedResult>{

    const paramBuilder = new HttpParamBuilder(Endpoints.Sale.GetAll())
      .set('pageNumber',page)
      .set('pageSize',pageSize)
      .set('order',order)
      .set('sort',sort)
      .set('id',formControls.id.value)
      .set('customerFirstName', formControls.customerFirstName.value)
      .set('customerLastName',formControls.customerLastName.value)
      .set('productName',formControls.productName.value)
      .set('amountMin',formControls.amountMin.value)
      .set('amountMax',formControls.amountMax.value)
      .set('incomeMin',formControls.incomeMin.value)
      .set('incomeMax',formControls.incomeMax.value);

    if(formControls.dateTimeMin.value){
      paramBuilder.set('dateTimeMin',formControls.dateTimeMin.value.toUTCString().replace("UTC","GMT"))
    }

    if(formControls.dateTimeMax.value){
      paramBuilder.set('dateTimeMax',formControls.dateTimeMax.value.toUTCString().replace("UTC","GMT"))
    }

    return this._http.get<SalePagedResult>(paramBuilder.getUrl());
  }

  single(id: number) : Observable<SaleSingleResult>{
    return this._http.get<SaleSingleResult>(this._endpoint.GetOne(id));
  }

}

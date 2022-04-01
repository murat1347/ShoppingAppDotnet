import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SortDirection} from "@angular/material/sort";
import {Observable} from "rxjs";
import {ProductPagedResult} from "../../model/product";
import {Endpoints} from "../../Endpoints";
import { HttpParams } from '@angular/common/http';
import {HttpParamBuilder} from "../../helper/httpparambuilder";
import {Customer, CustomerPagedResult} from "../../model/customer";
import {CustomerFilterFormControls} from "./customer.component";
import {AbstractService} from "../../helper/abstract.service";

@Injectable({
  providedIn: 'root'
})
export class CustomerService extends AbstractService<Customer> {

  constructor(http: HttpClient) {
    super(http,Endpoints.Customer)
  }

  getAll(sort : string, order: SortDirection, page:number, pageSize:number=30, formControls: CustomerFilterFormControls) : Observable<CustomerPagedResult>{

    const paramBuilder = new HttpParamBuilder(Endpoints.Customer.GetAll())
      .set('pageNumber',page)
      .set('pageSize',pageSize)
      .set('order',order)
      .set('sort',sort)
      .set('id',formControls.id.value)
      .set('firstName', formControls.firstName.value)
      .set('lastName',formControls.lastName.value)
      .set('phone',formControls.phone.value)
      .set('email',formControls.email.value)
    return this._http.get<CustomerPagedResult>(paramBuilder.getUrl());
  }
}

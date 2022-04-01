import {Observable} from "rxjs";
import {Product} from "../model/product";
import {AbstractEndpoint, Endpoints} from "../Endpoints";
import {HttpClient} from "@angular/common/http";
import {BaseModel} from "../model/basemodel";

export class AbstractService<T extends BaseModel>{

  constructor(protected _http: HttpClient,
              protected _endpoint: AbstractEndpoint
  ) {}

  getOne(id: number) : Observable<T>{
    return this._http.get<T>(this._endpoint.GetOne(id));
  }

  merge(entity: T) : Observable<T> {
    if(entity.id){
      return this.update(entity);
    }else{
      return this.create(entity);
    }
  }

  update(entity : T) : Observable<T>{
    return this._http.put<T>(this._endpoint.Update(entity.id!),entity);
  }

  create(entity: T) : Observable<T>{
    return this._http.post<T>(this._endpoint.Create(),entity);
  }

  delete(id:number) : Observable<Product>{
    return this._http.delete(this._endpoint.Delete(id));
  }

}

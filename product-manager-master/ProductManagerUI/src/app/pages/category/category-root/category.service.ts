import {HttpClient} from "@angular/common/http";
import {Endpoints} from "../../../Endpoints";
import {Observable, throwError} from "rxjs";
import {Category} from "../../../model/category";
import {Injectable} from "@angular/core";
import {catchError} from "rxjs/operators";

@Injectable({ providedIn: 'root' })
export class CategoryService {

  constructor(private _http: HttpClient) {}

  getAll() : Observable<Category[]>{
    return this._http.get<Category[]>(Endpoints.Category.GetAll());
  }
  getOne(id: number) : Observable<Category>{
    return this._http.get<Category>(Endpoints.Category.GetOne(id));
  }

  merge(category: Category) : Observable<Category> {
    if(category.id){
      return this.update(category);
    }else{
      return this.create(category);
    }
  }

  update(category : Category) : Observable<Category>{
    return this._http.put<Category>(Endpoints.Category.Update(category.id!),category);
  }

  create(category: Category) : Observable<Category>{
    return this._http.post<Category>(Endpoints.Category.Create(),category)
  }

  delete(id:number) : Observable<Category>{
    return this._http.delete(Endpoints.Category.Delete(id));
  }

}

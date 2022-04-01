import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

import {Endpoints} from "../../Endpoints";
import {Dashboard} from "../../model/dasboard";

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private _http: HttpClient) {}

  getDashboard() : Observable<Dashboard>{
    return this._http.get<Dashboard>(Endpoints.Dashboard.Get());
  }

  invalidateCache() : Observable<any>{
    return this._http.post<any>(Endpoints.Dashboard.Invalidate(),{});
  }
}

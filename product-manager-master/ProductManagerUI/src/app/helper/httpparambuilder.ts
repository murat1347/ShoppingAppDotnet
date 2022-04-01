import { HttpParams } from '@angular/common/http';


export class HttpParamBuilder{

  httpParams : HttpParams;
  constructor(private root:string) {
      this.httpParams = new HttpParams();
  }

  set(key:string, value: any) : HttpParamBuilder{
    if(value){
      this.httpParams = this.httpParams.set(key,value);
    }
    return this;
  }

  getUrl(){
    return this.root +"?" + this.httpParams.toString();
  }
}

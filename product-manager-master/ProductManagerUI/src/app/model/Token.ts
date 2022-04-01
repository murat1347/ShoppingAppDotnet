export class Token{

  constructor(private _token:string){
  }

  public getToken(){
    return this._token;
  }

  public setToken(token: string){
    this._token = token;
  }
}

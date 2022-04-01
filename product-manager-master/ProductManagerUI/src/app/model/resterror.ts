export class RestError{
  constructor(
    public name? : string,
    public errors? : string[]
  ) {
  }

  static GetRestErrorsFromRequest(err : any) : RestError[]{
    let error = err.error.errors;

    let arr = [];
    for (let prop in error) {
      // @ts-ignore
      let restError = new RestError(prop,error[prop])
      arr.push(restError);
    }
    return arr;
  }

}

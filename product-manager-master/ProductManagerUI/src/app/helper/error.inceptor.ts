import {Component, Injectable} from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthenticationService } from './authentication.service';
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'error-component',
  template: '<h4>An error happened please try again.</h4>',
  styles: [`
    .example-pizza-party {
      color: hotpink;
    }
  `],
})
export class ErrorComponent {}

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService,
              private _snackBar: MatSnackBar
  ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {
      if (err.status === 401) {
        // auto logout if 401 response returned from api
        this.authenticationService.logout();
        location.reload(true);
      }else if(err.status >= 500 || !err.status){
        this._snackBar.openFromComponent(ErrorComponent, {
          duration: 2000,
        });
      }

      return throwError(err);
    }))
  }
}

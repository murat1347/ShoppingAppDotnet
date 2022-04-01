import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from "../../helper/authentication.service";
import {first} from "rxjs/operators";

import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginError = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
  ) {

      if (this.authenticationService.currentUserValue) {
        this.router.navigate(['/']);
      }
   }

  ngOnInit(): void {
  }

  login(email: string, password: string) {

    this.loginError = false;

    this.authenticationService.login(email,password).pipe(first())
      .subscribe({
      next: () => {
        console.log("next");
        const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
        this.router.navigate([returnUrl]);
      },
      error: error => {
        console.log(error);
        this.loginError = true;
      }
    });

  }

}

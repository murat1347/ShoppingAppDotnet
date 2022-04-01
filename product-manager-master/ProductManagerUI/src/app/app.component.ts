import { Component } from '@angular/core';
import { faCog } from '@fortawesome/free-solid-svg-icons';
import {AuthenticationService} from "./helper/authentication.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'my-app';
  faCog = faCog;

  constructor(public authenticationService : AuthenticationService) {
  }
}

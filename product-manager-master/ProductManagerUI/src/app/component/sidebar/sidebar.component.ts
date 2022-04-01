import { Component, OnInit } from '@angular/core';
import {AuthenticationService} from "../../helper/authentication.service";
import {AppRoutingModule, RouteValues} from "../../app-routing.module";

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  RouteValues = RouteValues;

  constructor(public authenticationService : AuthenticationService) {
  }

  ngOnInit(): void {
  }

}

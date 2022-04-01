import {Component, OnInit, ViewChild} from '@angular/core';
import {Customer} from "../../model/customer";
import {MatSort} from "@angular/material/sort";
import {MatPaginator} from "@angular/material/paginator";
import {FormControl} from "@angular/forms";
import {CustomerService} from "../../pages/customer/customer.service";
import {MatDialog} from "@angular/material/dialog";
import {merge, of as observableOf} from "rxjs";
import {catchError, map, startWith, switchMap} from "rxjs/operators";
import {DeleteDialogComponent} from "../delete-dialog/delete-dialog.component";
import {CustomerFilterFormControls} from "../../pages/customer/customer.component";
import { RouteValues } from 'src/app/app-routing.module';

@Component({
  selector: 'app-customer-find-dialog',
  templateUrl: './customer-find-dialog.component.html',
  styleUrls: ['./customer-find-dialog.component.scss']
})
export class CustomerFindDialogComponent implements OnInit {

  RouteValues = RouteValues;

  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'email', 'phone','address', 'select'];

  displayedColumnFilters: string[] = ['id-filter', 'firstName-filter', 'lastName-filter'
    , 'email-filter', 'phone-filter','address-filter'];

  data: Customer[] = [];

  @ViewChild(MatSort, { static: false }) sort?: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator?: MatPaginator;

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  formControls : CustomerFilterFormControls = {
    id : new FormControl(),
    firstName : new FormControl(),
    lastName : new FormControl(),
    email : new FormControl(),
    phone : new FormControl()
  }

  constructor(private _customerService:CustomerService,
              public dialog: MatDialog

  ) { }

  ngOnInit(): void {
  }

  tableChange(){
    merge(this.sort!.sortChange, this.paginator!.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this._customerService!.getAll(
            this.sort!.active, this.sort!.direction, 1 + this.paginator!.pageIndex,this.paginator?.pageSize, this.formControls)
            .pipe(catchError(() => observableOf(null)));
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;
          this.isRateLimitReached = data === null;

          if (data === null) {
            return [];
          }

          this.resultsLength = data.count;
          return data.customers;
        })
      ).subscribe(data => this.data = data);
  }

  ngAfterViewInit() {

    // If the user changes the sort order, reset back to the first page.
    this.sort!.sortChange.subscribe(() => this.paginator!.pageIndex = 0);
    this.tableChange();

    for (let prop in this.formControls) {
      let key: string = prop;
      // @ts-ignore
      this.formControls[key].valueChanges.subscribe(_ => {
        this.tableChange();
      });
    }
  }

  delete( id: number){
    this.dialog.open(DeleteDialogComponent).afterClosed().subscribe(res=>{
      if(res){
        this._customerService.delete(id).subscribe(s=>this.tableChange());
      }
    });
  }

}

import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {Customer} from "../../model/customer";
import {merge, of as observableOf} from "rxjs";
import {catchError, debounceTime, distinctUntilChanged, map, startWith, switchMap, tap} from "rxjs/operators";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {CustomerService} from "./customer.service";
import {Form, FormControl} from "@angular/forms";
import {RouteValues} from "../../app-routing.module";
import {DeleteDialogComponent} from "../../component/delete-dialog/delete-dialog.component";
import {MatDialog} from "@angular/material/dialog";
import {DeleteErrorDialogComponent} from "../../component/delete-error-dialog/delete-error-dialog.component";

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit,AfterViewInit {

  RouteValues = RouteValues;

  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'email', 'phone','address', 'edit','delete'];

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
      this.formControls[key].valueChanges.pipe(
        tap(() => this.isLoadingResults = true),
        debounceTime(500),
        distinctUntilChanged())
        .subscribe(() => {
          this.sort?.sortChange.emit();
        });
    }
  }

  delete( id: number){
    this.dialog.open(DeleteDialogComponent).afterClosed().subscribe(res=>{
      if(res){
        this._customerService.delete(id).subscribe(s=>this.tableChange(),
          err=> this.dialog.open(DeleteErrorDialogComponent)
        );
      }
    });
  }

}

export interface CustomerFilterFormControls{
  id : FormControl,
  firstName : FormControl,
  lastName : FormControl,
  email : FormControl,
  phone: FormControl
}

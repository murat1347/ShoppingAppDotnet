import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {Form, FormControl} from "@angular/forms";
import {CustomerFilterFormControls} from "../customer/customer.component";
import {Customer} from "../../model/customer";
import {Purchase} from "../../model/purchase";
import {MatSort} from "@angular/material/sort";
import {MatPaginator} from "@angular/material/paginator";
import {merge, of as observableOf} from "rxjs";
import {catchError, debounceTime, distinctUntilChanged, map, startWith, switchMap, tap} from "rxjs/operators";
import {PurchaseService} from "./purchase.service";
import { RouteValues } from 'src/app/app-routing.module';
import {MatDialog} from "@angular/material/dialog";
import {DeleteDialogComponent} from "../../component/delete-dialog/delete-dialog.component";
import {DeleteErrorDialogComponent} from "../../component/delete-error-dialog/delete-error-dialog.component";

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.scss']
})
export class PurchaseComponent implements OnInit, AfterViewInit {

  RouteValues = RouteValues;

  displayedColumns: string[] = ['id', 'sellerFirstName', 'sellerLastName', 'productName', 'amount', 'cost', 'dateTime','edit','delete'];

  displayedColumnFirstFilters: string[] = ['id-filter', 'sellerFirstName-filter', 'sellerLastName-filter'
    , 'productName-filter', 'amountMin-filter', 'costMin-filter', 'dateTimeMin-filter'];

  displayedColumnSecondFilters: string[] = ['id-second-filter', 'sellerFirstName-second-filter', 'sellerLastName-second-filter'
    , 'productName-second-filter', 'amountMax-filter', 'costMax-filter', 'dateTimeMax-filter'];

  data: Purchase[] = [];

  formControls: PurchaseFilterFormControls = {
    id: new FormControl(),
    sellerFirstName: new FormControl(),
    sellerLastName: new FormControl(),
    productName: new FormControl(),
    amountMin: new FormControl(),
    amountMax: new FormControl(),
    costMin: new FormControl(),
    costMax: new FormControl(),
    dateTimeMin: new FormControl(),
    dateTimeMax: new FormControl()
  }

  @ViewChild(MatSort, {static: false}) sort?: MatSort;
  @ViewChild(MatPaginator, {static: false}) paginator?: MatPaginator;

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  constructor(
    private _purchaseService: PurchaseService,
    public dialog: MatDialog
  ) {
  }

  ngOnInit(): void {
  }

  tableChange() {
    merge(this.sort!.sortChange, this.paginator!.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this._purchaseService!.getAll(
            this.sort!.active, this.sort!.direction, 1 + this.paginator!.pageIndex, this.paginator?.pageSize, this.formControls)
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
          return data.purchases;
        })
      ).subscribe(data => this.data = data);
  }

  ngAfterViewInit(): void {
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
        this._purchaseService.delete(id).subscribe(s=>this.tableChange(),
          err=> this.dialog.open(DeleteErrorDialogComponent)
        );
      }
    });
  }

}

export interface PurchaseFilterFormControls{
  id : FormControl,
  sellerFirstName : FormControl,
  sellerLastName : FormControl,
  productName : FormControl,
  amountMin :  FormControl,
  amountMax : FormControl,
  costMin : FormControl,
  costMax : FormControl,
  dateTimeMin : FormControl,
  dateTimeMax : FormControl
}

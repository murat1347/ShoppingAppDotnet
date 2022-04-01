import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {FormControl} from "@angular/forms";
import {PurchaseFilterFormControls} from "../purchase/purchase.component";
import {MatSort} from "@angular/material/sort";
import {MatPaginator} from "@angular/material/paginator";
import {merge, of as observableOf} from "rxjs";
import {catchError, debounceTime, distinctUntilChanged, map, startWith, switchMap, tap} from "rxjs/operators";
import {SaleService} from "./sale.service";
import {Sale} from "../../model/sale";
import { RouteValues } from 'src/app/app-routing.module';
import {MatDialog} from "@angular/material/dialog";
import {DeleteDialogComponent} from "../../component/delete-dialog/delete-dialog.component";
import {DeleteErrorDialogComponent} from "../../component/delete-error-dialog/delete-error-dialog.component";

@Component({
  selector: 'app-sale',
  templateUrl: './sale.component.html',
  styleUrls: ['./sale.component.scss']
})
export class SaleComponent implements OnInit, AfterViewInit {

  RouteValues = RouteValues;

  displayedColumns: string[] = ['id', 'customerFirstName', 'customerLastName', 'productName', 'amount', 'income', 'dateTime','edit','delete'];

  displayedColumnFirstFilters: string[] = ['id-filter', 'customerFirstName-filter', 'customerLastName-filter'
    , 'productName-filter', 'amountMin-filter', 'incomeMin-filter', 'dateTimeMin-filter'];

  displayedColumnSecondFilters: string[] = ['id-second-filter', 'customerFirstName-second-filter', 'customerLastName-second-filter'
    , 'productName-second-filter', 'amountMax-filter', 'incomeMax-filter', 'dateTimeMax-filter'];

  data: Sale[] = []

  constructor(
    private _saleService: SaleService,
    public dialog: MatDialog
  ) { }

  ngAfterViewInit(): void {
        this.tableChange();
    }

  formControls: SaleFilterFormControls = {
    id: new FormControl(),
    customerFirstName: new FormControl(),
    customerLastName: new FormControl(),
    productName: new FormControl(),
    amountMin: new FormControl(),
    amountMax: new FormControl(),
    incomeMin: new FormControl(),
    incomeMax: new FormControl(),
    dateTimeMin: new FormControl(),
    dateTimeMax: new FormControl()
  }

  @ViewChild(MatSort, {static: false}) sort?: MatSort;
  @ViewChild(MatPaginator, {static: false}) paginator?: MatPaginator;

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  ngOnInit(): void {
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

  tableChange() {
    merge(this.sort!.sortChange, this.paginator!.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this._saleService!.getAll(
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
          return data.sales;
        })
      ).subscribe(data => this.data = data);
  }

  delete( id: number){
    this.dialog.open(DeleteDialogComponent).afterClosed().subscribe(res=>{
      if(res){
        this._saleService.delete(id).subscribe(s=>this.tableChange(),
          err=> this.dialog.open(DeleteErrorDialogComponent)
        );
      }
    });
  }

}

export interface SaleFilterFormControls{
  id : FormControl,
  customerFirstName : FormControl,
  customerLastName : FormControl,
  productName : FormControl,
  amountMin :  FormControl,
  amountMax : FormControl,
  incomeMin : FormControl,
  incomeMax : FormControl,
  dateTimeMin : FormControl,
  dateTimeMax : FormControl
}

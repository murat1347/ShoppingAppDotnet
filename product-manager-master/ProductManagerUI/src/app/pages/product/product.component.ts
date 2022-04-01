import {AfterViewInit, Component, ElementRef, HostListener, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator} from "@angular/material/paginator";
import {Product} from "../../model/product";
import {ProductService} from "./product.service";
import {HttpClient} from "@angular/common/http";
import {MatSort, SortDirection} from "@angular/material/sort";
import {merge, Observable, of as observableOf} from 'rxjs';
import {catchError, map, startWith, switchMap, tap} from "rxjs/operators";
import {Category} from "../../model/category";
import {CategoryService} from "../category/category-root/category.service";
import {FormControl} from "@angular/forms";
import {MatSnackBar} from '@angular/material/snack-bar';
import {RouteValues} from "../../app-routing.module";
import {MatDialog} from "@angular/material/dialog";
import {DeleteDialogComponent} from "../../component/delete-dialog/delete-dialog.component";
import {SellerFilterFormControls} from "../seller/seller.component";
import {DeleteErrorDialogComponent} from "../../component/delete-error-dialog/delete-error-dialog.component";

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit, AfterViewInit {

  RouteValues = RouteValues

  displayedColumns: string[] = ['id', 'name', 'category'];

  displayedColumnFilters: string[] = [
    'id-filter', 'name-filter', 'category-filter'
  ];

  data: Product[] = [];

  resultsLength = 0;
  isLoadingResults = true;

  @ViewChild(MatPaginator) paginator?: MatPaginator;
  @ViewChild(MatSort) sort?: MatSort;
  @ViewChild('someVar') element?: ElementRef;

  filteredOptions: Observable<Category[]>;

  categories: Category[] = [];

  formControls: ProductFilterFormControls = {
    id: new FormControl(),
    categoryName: new FormControl(),
    categoryId: new FormControl(),
    productName: new FormControl(),
  }

  constructor(private _httpClient: HttpClient,
              private _productService: ProductService,
              private _categoryService: CategoryService,
              public dialog: MatDialog
  ) {
    this.filteredOptions = new Observable<Category[]>();
  }

  private _filter(value: any): Category[] {
    if (value) {
      let filterValue = "";
      if (value instanceof Category)
        filterValue = value.name?.toLowerCase()!;
      else {
        filterValue = value;
      }

      return this.categories.filter(category => category.name!.toLowerCase().includes(filterValue));
    }
    return [];
  }

  ngOnInit(): void {

    this._categoryService.getAll().subscribe(categories => this.categories = categories);

    this.filteredOptions = this.formControls.categoryName.valueChanges
      .pipe(
        tap(a => {
          a == "" ? this.categorySelected(null) : ""
        }),
        startWith(''),
        map(value => this._filter(value))
      );

    this.formControls.productName.valueChanges.subscribe(value => {
      this.sort?.sortChange.emit();
    });

    this.formControls.id.valueChanges.subscribe(value => {
      this.sort?.sortChange.emit();
    });
  }

  categoryDisplayName(cat: Category): string {
    return cat == null ? "" : cat.name!;
  }

  categorySelected(event: any) {
    if (event == null) {
      this.formControls.categoryId.setValue(null);
      this.sort?.sortChange.emit();
    }else {
      console.log(event);
      let catId = event.option.value.id;
      this.formControls.categoryId.setValue(catId);
      this.sort?.sortChange.emit();
    }
  }

  setCategoryName(name: string) {
    this.formControls.categoryName.setValue(name);
  }


  ngAfterViewInit() {

    // If the user changes the sort order, reset back to the first page.
    this.sort!.sortChange.subscribe(() => this.paginator!.pageIndex = 0);
    this.nChange();
  }

  nChange() {
    merge(this.sort!.sortChange, this.paginator!.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this._productService!.getAll(
            this.sort!.active, this.sort!.direction, 1 + this.paginator!.pageIndex, this.paginator?.pageSize,
            this.formControls)
            .pipe(catchError(() => observableOf(null)));
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;

          if (data === null) {
            return [];
          }

          this.resultsLength = data.count;
          return data.products;
        })
      ).subscribe(data => this.data = data);
  }

  delete(id?: number) {
    if (id) {
      this.dialog.open(DeleteDialogComponent).afterClosed().subscribe(res => {
        if (res) {
          this._productService.delete(id).subscribe(
            s => this.nChange(),
            err => this.dialog.open(DeleteErrorDialogComponent)
          );
        }
      });
    }
  }

}

export interface ProductFilterFormControls {
  id: FormControl,
  categoryName: FormControl,
  categoryId: FormControl,
  productName: FormControl,
}



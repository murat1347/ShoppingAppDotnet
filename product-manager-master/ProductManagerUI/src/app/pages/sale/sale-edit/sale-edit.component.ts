import { Component, OnInit } from '@angular/core';
import {Product} from "../../../model/product";
import {Customer} from "../../../model/customer";
import {Observable} from "rxjs";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ProductService} from "../../product/product.service";
import {CustomerService} from "../../customer/customer.service";
import {PurchaseService} from "../../purchase/purchase.service";
import {MatDialog} from "@angular/material/dialog";
import {ActivatedRoute, Router} from "@angular/router";
import {debounceTime, distinctUntilChanged, map, startWith, switchMap} from "rxjs/operators";
import {CustomerFindDialogComponent} from "../../../component/customer-find-dialog/customer-find-dialog.component";
import {RouteValues} from "../../../app-routing.module";
import {SaleEditDTO} from "../../../model/sale";
import {SaleService} from "../sale.service";
import {RestError} from "../../../model/resterror";

@Component({
  selector: 'app-sale-edit',
  templateUrl: './sale-edit.component.html',
  styleUrls: ['./sale-edit.component.scss']
})
export class SaleEditComponent implements OnInit {

  saleId?: number;

  selectedProduct?: Product;

  selectedCustomer?: Customer

  errors? : RestError[];

  filteredProductOptions: Observable<Product[]>;

  saleForm = new FormGroup({
    product: new FormControl('', [Validators.required]),
    customer: new FormControl({value: '', disabled: true}, [Validators.required]),
    amount: new FormControl('', [Validators.required]),
    income: new FormControl('', [Validators.required]),
    dateTime: new FormControl('', [Validators.required]),
  });

  constructor(private _productService: ProductService,
              private _customerService: CustomerService,
              private _saleService: SaleService,
              public dialog: MatDialog,
              private router: Router,
              private route: ActivatedRoute
  ) {

    this.filteredProductOptions = this.saleForm.controls['product'].valueChanges
      .pipe(
        startWith(''),
        debounceTime(200),
        distinctUntilChanged(),
        switchMap(val => {
          return this.filterProducts(val || '');
        })
      );

  }

  onSelectedProduct(event: any) {
    this.selectedProduct = event.option.value;
  }

  filterProducts(val: string): Observable<Product[]> {
    return this._productService.findByName(val)
      .pipe(
        map(response => response.products)
      )
  }

  openFindCustomerDialog() {

    this.dialog.open(CustomerFindDialogComponent).afterClosed().subscribe((res: Customer) => {
      if (res) {
        this.selectedCustomer = res;
        this.saleForm.controls['customer'].setValue(res.firstName + ' ' + res.lastName);
      }
    });
  }

  public displayProperty(value: any) {
    if (value) {
      return value.name;
    }
  }

  ngOnInit(): void {
    this.getPurchase();
  }

  save() {
    let purchaseEditDTO = new SaleEditDTO();

    purchaseEditDTO.amount = this.saleForm.controls['amount'].value;
    purchaseEditDTO.income = this.saleForm.controls['income'].value;
    purchaseEditDTO.productId = this.selectedProduct!.id;
    purchaseEditDTO.customerId = this.selectedCustomer!.id;
    purchaseEditDTO.dateTime = new Date(this.saleForm.controls['dateTime'].value).toISOString();
    purchaseEditDTO.id = this.saleId;

    // @ts-ignore
    this._saleService.merge(purchaseEditDTO).subscribe(res => {
      this.router.navigate([RouteValues.Sale.Root]);
    },err=> this.errors = RestError.GetRestErrorsFromRequest(err))
  }


  getPurchase() {
    if (this.route.snapshot.paramMap.has('id')) {

      const id = Number(this.route.snapshot.paramMap.get('id'));

      this._saleService.single(id).subscribe(
        (purchase) => {
          this.saleId = id;
          this.selectedProduct = purchase.product;
          this.selectedCustomer = purchase.customer;
          this.saleForm.controls['income'].setValue(purchase.income)
          this.saleForm.controls['amount'].setValue(purchase.amount);
          this.saleForm.controls['dateTime'].setValue(purchase.dateTime);
          this.saleForm.controls['customer'].setValue(purchase.customer?.firstName + ' ' + purchase.customer?.lastName);
          this.saleForm.controls['product'].setValue(purchase.product?.name);
        }
      )
    }
  }

}

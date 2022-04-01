import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Purchase, PurchaseEditDTO, PurchaseSingleResult} from "../../../model/purchase";
import {Observable} from "rxjs";
import {Category} from "../../../model/category";
import {Product} from "../../../model/product";
import {debounceTime, distinctUntilChanged, map, startWith, switchMap} from "rxjs/operators";
import {ProductService} from "../../product/product.service";
import {SellerService} from "../../seller/seller.service";
import {PurchaseService} from "../purchase.service";
import {Seller} from "../../../model/seller";
import {MatDialog} from "@angular/material/dialog";
import {DeleteDialogComponent} from "../../../component/delete-dialog/delete-dialog.component";
import {SellerFindDialogComponent} from "../../../component/seller-find-dialog/seller-find-dialog.component";
import {RouteValues} from "../../../app-routing.module";
import {ActivatedRoute, Router} from "@angular/router";
import { RestError } from 'src/app/model/resterror';

@Component({
  selector: 'app-purchase-edit',
  templateUrl: './purchase-edit.component.html',
  styleUrls: ['./purchase-edit.component.scss']
})
export class PurchaseEditComponent implements OnInit {

  purchaseId?: number;

  errors? : RestError[];

  selectedProduct?: Product;

  selectedSeller?: Seller

  filteredProductOptions: Observable<Product[]>;

  purchaseForm = new FormGroup({
    product: new FormControl('', [Validators.required]),
    seller: new FormControl({value: '', disabled: true}, [Validators.required]),
    amount: new FormControl('', [Validators.required]),
    cost: new FormControl('', [Validators.required]),
    dateTime: new FormControl('', [Validators.required]),
  });

  constructor(private _productService: ProductService,
              private _sellerService: SellerService,
              private _purchaseService: PurchaseService,
              public dialog: MatDialog,
              private router: Router,
              private route: ActivatedRoute
  ) {

    this.filteredProductOptions = this.purchaseForm.controls['product'].valueChanges
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

  openFindSellerDialog() {

    this.dialog.open(SellerFindDialogComponent).afterClosed().subscribe((res: Seller) => {
      if (res) {
        this.selectedSeller = res;
        this.purchaseForm.controls['seller'].setValue(res.firstName + ' ' + res.lastName);
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
    let purchaseEditDTO = new PurchaseEditDTO();

    purchaseEditDTO.cost = this.purchaseForm.controls['cost'].value;
    purchaseEditDTO.amount = this.purchaseForm.controls['amount'].value;
    purchaseEditDTO.productId = this.selectedProduct!.id;
    purchaseEditDTO.sellerId = this.selectedSeller!.id;
    purchaseEditDTO.dateTime = new Date(this.purchaseForm.controls['dateTime'].value).toISOString();
    purchaseEditDTO.id = this.purchaseId;

    // @ts-ignore
    this._purchaseService.merge(purchaseEditDTO).subscribe(res => {
      this.router.navigate([RouteValues.Purchase.Root]);
    }, err=> this.errors = RestError.GetRestErrorsFromRequest(err))
  }


  getPurchase() {
    if (this.route.snapshot.paramMap.has('id')) {

      const id = Number(this.route.snapshot.paramMap.get('id'));

      this._purchaseService.single(id).subscribe(
        (purchase) => {
          this.purchaseId = id;
          this.selectedProduct = purchase.product;
          this.selectedSeller = purchase.seller;
          this.purchaseForm.controls['cost'].setValue(purchase.cost)
          this.purchaseForm.controls['amount'].setValue(purchase.amount);
          this.purchaseForm.controls['dateTime'].setValue(purchase.dateTime);
          this.purchaseForm.controls['seller'].setValue(purchase.seller?.firstName + ' ' + purchase.seller?.lastName);
          this.purchaseForm.controls['product'].setValue(purchase.product?.name);
        }
      )
    }
  }

}

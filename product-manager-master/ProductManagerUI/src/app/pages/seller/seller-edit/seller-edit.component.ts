import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import { Seller } from 'src/app/model/seller';
import {ActivatedRoute, Router} from "@angular/router";
import {SellerService} from "../seller.service";
import {RouteValues} from "../../../app-routing.module";
import {RestError} from "../../../model/resterror";

@Component({
  selector: 'app-seller-edit',
  templateUrl: './seller-edit.component.html',
  styleUrls: ['./seller-edit.component.scss']
})
export class SellerEditComponent implements OnInit {

  sellerId? : number;

  Seller = Seller;

  errors? : RestError[];

  sellerForm = new FormGroup({
    firstName: new FormControl('', [Validators.minLength(Seller.FirstNameMinLength),
      Validators.maxLength(Seller.FirstNameMaxLength), Validators.required]),
    lastName: new FormControl('', [Validators.minLength(Seller.LastNameMinLength),
      Validators.maxLength(Seller.LastNameMaxLength), Validators.required]),
    email: new FormControl('', [Validators.minLength(Seller.EmailMinLength),
      Validators.maxLength(Seller.EmailMaxLength), Validators.required]),
    phone: new FormControl('', [Validators.minLength(Seller.PhoneMinLength),
      Validators.maxLength(Seller.PhoneMaxLength), Validators.required]),
    address: new FormControl('', [Validators.minLength(Seller.AddressMinLength),
      Validators.maxLength(Seller.AddressMaxLength), Validators.required]),
  });

  constructor(private _sellerService: SellerService,
              private router: Router,
              private route : ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.getSeller();
  }

  save(){

    let seller = new Seller();

    seller.firstName = this.sellerForm.controls["firstName"].value;
    seller.lastName = this.sellerForm.controls["lastName"].value;
    seller.email = this.sellerForm.controls["email"].value;
    seller.phone = this.sellerForm.controls["phone"].value;
    seller.address = this.sellerForm.controls["address"].value;

    if (this.sellerId) {
      seller.id = this.sellerId;
    }

    this._sellerService.merge(seller).subscribe(
      seller => {
        this.router.navigate([RouteValues.Seller.Root]);
      },err=> this.errors = RestError.GetRestErrorsFromRequest(err)
    );
  }

  getSeller() {
    if (this.route.snapshot.paramMap.has('id')) {

      const id = Number(this.route.snapshot.paramMap.get('id'));

      this._sellerService.getOne(id).subscribe(
        seller =>{
          this.sellerId = id;
          this.sellerForm.controls["firstName"].setValue(seller.firstName);
          this.sellerForm.controls["lastName"].setValue(seller.lastName);
          this.sellerForm.controls["email"].setValue(seller.email);
          this.sellerForm.controls["phone"].setValue(seller.phone);
          this.sellerForm.controls["address"].setValue(seller.address);
        }
      )
    }

  }

}

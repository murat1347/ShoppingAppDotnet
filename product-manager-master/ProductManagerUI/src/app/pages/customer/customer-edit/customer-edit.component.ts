import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Customer} from "../../../model/customer";
import {RouteValues} from "../../../app-routing.module";
import {CustomerService} from "../customer.service";
import {ActivatedRoute, Router} from "@angular/router";
import {RestError} from "../../../model/resterror";

@Component({
  selector: 'app-customer-edit',
  templateUrl: './customer-edit.component.html',
  styleUrls: ['./customer-edit.component.scss']
})
export class CustomerEditComponent implements OnInit {

  customerId? : number;

  errors? : RestError[];

  Customer = Customer;

  customerForm = new FormGroup({
    firstName: new FormControl('', [Validators.minLength(Customer.FirstNameMinLength),
      Validators.maxLength(Customer.FirstNameMaxLength), Validators.required]),
    lastName: new FormControl('', [Validators.minLength(Customer.LastNameMinLength),
      Validators.maxLength(Customer.LastNameMaxLength), Validators.required]),
    email: new FormControl('', [Validators.minLength(Customer.EmailMinLength),
      Validators.maxLength(Customer.EmailMaxLength), Validators.required]),
    phone: new FormControl('', [Validators.minLength(Customer.PhoneMinLength),
      Validators.maxLength(Customer.PhoneMaxLength), Validators.required]),
    address: new FormControl('', [Validators.minLength(Customer.AddressMinLength),
      Validators.maxLength(Customer.AddressMaxLength), Validators.required]),
  });

  constructor(private _customerService: CustomerService,
              private router: Router,
              private route : ActivatedRoute
              ) { }

  ngOnInit(): void {
    this.getCustomer();
  }

  save(){

    let customer = new Customer();

    customer.firstName = this.customerForm.controls["firstName"].value;
    customer.lastName = this.customerForm.controls["lastName"].value;
    customer.email = this.customerForm.controls["email"].value;
    customer.phone = this.customerForm.controls["phone"].value;
    customer.address = this.customerForm.controls["address"].value;

    if (this.customerId) {
      customer.id = this.customerId;
    }

    this._customerService.merge(customer).subscribe(
      res => {
        this.router.navigate([RouteValues.Customer.Root]);
      }, err => this.errors = RestError.GetRestErrorsFromRequest(err)
    );
  }

  getCustomer() {
    if (this.route.snapshot.paramMap.has('id')) {

      const id = Number(this.route.snapshot.paramMap.get('id'));

      this._customerService.getOne(id).subscribe(
        customer =>{
          this.customerId = id;
          this.customerForm.controls["firstName"].setValue(customer.firstName);
          this.customerForm.controls["lastName"].setValue(customer.lastName);
          this.customerForm.controls["email"].setValue(customer.email);
          this.customerForm.controls["phone"].setValue(customer.phone);
          this.customerForm.controls["address"].setValue(customer.address);
        }
      )
    }
  }

}

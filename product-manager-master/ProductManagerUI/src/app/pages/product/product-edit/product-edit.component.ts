import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from "@angular/forms";
import {Product, ProductUpdateDTO} from "../../../model/product";
import {CategoryEditComponent} from "../../category/category-edit/category-edit.component";
import {Category} from "../../../model/category";
import {Observable} from "rxjs";
import {map, startWith} from "rxjs/operators";
import {CategoryService} from "../../category/category-root/category.service";
import {ProductService} from "../product.service";
import {RouteValues} from "../../../app-routing.module";
import {ActivatedRoute, Route, Router} from "@angular/router";
import {RestError} from "../../../model/resterror";

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent implements OnInit {

  productId?: number

  Product = Product;

  errors?: RestError[];

  productForm = new FormGroup({
    name: new FormControl('', [Validators.minLength(Product.NameMinLength),
      Validators.maxLength(Product.NameMaxLength), Validators.required]),
    imageUrl: new FormControl('', [Validators.minLength(Product.ImageUrlMinLength),
      Validators.maxLength(Product.ImageUrlMaxLength), Validators.required]),
    category: new FormControl('', CategoryValidator(this))
  });

  categories: Category[] = [];

  filteredOptions: Observable<Category[]>;

  constructor(
    private _categoryService: CategoryService,
    private _productService: ProductService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.filteredOptions = new Observable<Category[]>();
  }

  private _filter(value: any): Category[] {
    if (value instanceof String) {
      const filterValue = value.toLowerCase();
      return this.categories.filter(category => category.name!.toLowerCase().includes(filterValue));
    } else {
      return this.categories;
    }
  }

  ngOnInit(): void {
    this._categoryService.getAll().subscribe(categories => {
      this.categories = categories;
      this.getProduct();
    });

    this.filteredOptions = this.productForm.controls['category'].valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }

  save() {

    let product = new ProductUpdateDTO();

    product.name = this.productForm.controls["name"].value;
    product.imageUrl = this.productForm.controls["imageUrl"].value;

    let category = this.categories.find(c => c.name == this.productForm.controls["category"].value);

    if (category) {
      product.categoryId = category.id;
    }

    if (this.productId) {
      product.id = this.productId;
    }

    this._productService.merge(product).subscribe(
      product => {
        this.router.navigate([RouteValues.Product.Root]);
      },
      err => this.errors = RestError.GetRestErrorsFromRequest(err)
    );
  }

  getProduct() {
    if (this.route.snapshot.paramMap.has('id')) {

      const id = Number(this.route.snapshot.paramMap.get('id'));

      this._productService.getOne(id).subscribe(
        product => {
          this.productId = id;
          this.productForm.controls["name"].setValue(product.name);
          this.productForm.controls["imageUrl"].setValue(product.imageUrl);
          this.productForm.controls["category"].setValue(this.categories.find(cat => cat.id == product.category?.id)?.name);
        }
      )
    }
  }

}

function CategoryValidator(component: ProductEditComponent): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (component.categories && control.value) {
      const forbidden = component.categories.map(c => c.name).indexOf(control.value) == -1;
      return forbidden ? {categoryDoesNotExist: {value: control.value}} : null;
    } else {
      return null;
    }
  };
}

import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Category} from "../../../model/category";
import {FormGroup, FormControl, ValidationErrors, AbstractControl, ValidatorFn} from '@angular/forms';
import {Observable} from 'rxjs';
import {map, startWith,first} from 'rxjs/operators';
import {CategoryService} from "../category-root/category.service";
import {Validators} from '@angular/forms';
import {RouteValues} from "../../../app-routing.module";
import {RestError} from "../../../model/resterror";

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.scss']
})
export class CategoryEditComponent implements OnInit {

  Category = Category;

  categoryId? : number;

  categoryForm = new FormGroup({
    name: new FormControl('', [Validators.minLength(Category.NameMinLength),
      Validators.maxLength(Category.NameMaxLength), Validators.required]),
    parent: new FormControl('', ParentCategoryValidator(this))
  });

  categories: Category[] = [];

  errors? : RestError[];

  filteredOptions: Observable<Category[]>;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private categoryService: CategoryService,
  ) {
    this.filteredOptions = new Observable<Category[]>();
  }

  ngOnInit(): void {

    this.getCategory();

    this.categoryService.getAll().subscribe(categories => this.categories = categories);

    this.filteredOptions = this.categoryForm.controls['parent'].valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }

  private _filter(value: any): Category[] {
    if(value){
      const filterValue = value.toLowerCase();
      return this.categories.filter(category => category.name!.toLowerCase().includes(filterValue));
    }
    return [];
  }

  getCategory() {
    if (this.route.snapshot.paramMap.has('id')) {

      const id = Number(this.route.snapshot.paramMap.get('id'));

       this.categoryService.getOne(id).subscribe(
        category =>{
          this.categoryForm.controls["name"].setValue(category.name);
          this.categoryForm.controls["parent"].setValue(this.categories.find(cat=>cat.id == category.parentId)?.name);
          this.categoryId = id;
        }
      )
    }
  }

  save() {

    this.errors = undefined;

    let category = new Category();

    category.name = this.categoryForm.controls["name"].value;
    let parent = this.categories.find(c=>c.name == this.categoryForm.controls["parent"].value);

    if(parent){
      category.parentId = parent.id;
    }

    if(this.categoryId){
      category.id = this.categoryId;
    }

    this.categoryService.merge(category).subscribe(
      res =>this.router.navigate([RouteValues.Category.Root]),
      err=> this.errors = RestError.GetRestErrorsFromRequest(err)
      )
  }

}

function ParentCategoryValidator(component: CategoryEditComponent): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (component.categories && control.value) {
      const forbidden = component.categories.map(c => c.name).indexOf(control.value) == -1;
      return forbidden ? {parentDoesNotExist: {value: control.value}} : null;
    } else {
      return null;
    }
  };
}

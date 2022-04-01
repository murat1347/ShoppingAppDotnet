import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './account/login/login.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { JwtInterceptor} from './helper/jwt.inceptor';
import {ErrorComponent, ErrorInterceptor} from './helper/error.inceptor';
import { AuthGuard } from './helper/auth.guard';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { CategoryComponent } from './pages/category/category-root/category.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTreeModule} from "@angular/material/tree";
import {MatIconModule} from "@angular/material/icon";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatCardModule} from "@angular/material/card";
import {MatButtonModule} from "@angular/material/button";
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { CategoryEditComponent } from './pages/category/category-edit/category-edit.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {MatInput, MatInputModule} from "@angular/material/input";
import {ReactiveFormsModule} from "@angular/forms";
import {MatAutocompleteModule} from "@angular/material/autocomplete";
import {MatNativeDateModule, MatOptionModule} from "@angular/material/core";
import { LogoutComponent } from './account/logout/logout.component';
import { ProductComponent } from './pages/product/product.component';
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatTableModule} from "@angular/material/table";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {MatSortModule} from "@angular/material/sort";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import { CustomerComponent } from './pages/customer/customer.component';
import { SellerComponent } from './pages/seller/seller.component';
import { PurchaseComponent } from './pages/purchase/purchase.component';
import { SaleComponent } from './pages/sale/sale.component';
import { SidebarComponent } from './component/sidebar/sidebar.component';
import {MatDatepickerModule} from "@angular/material/datepicker";
import {BarChartModule, PieChartModule} from "@swimlane/ngx-charts";
import { ProductEditComponent } from './pages/product/product-edit/product-edit.component';
import { DeleteDialogComponent } from './component/delete-dialog/delete-dialog.component';
import {MatDialogModule} from "@angular/material/dialog";
import { PurchaseEditComponent } from './pages/purchase/purchase-edit/purchase-edit.component';
import { SaleEditComponent } from './pages/sale/sale-edit/sale-edit.component';
import { SellerEditComponent } from './pages/seller/seller-edit/seller-edit.component';
import { CustomerEditComponent } from './pages/customer/customer-edit/customer-edit.component';
import { SellerFindDialogComponent } from './component/seller-find-dialog/seller-find-dialog.component';
import { CustomerFindDialogComponent } from './component/customer-find-dialog/customer-find-dialog.component';
import {MatProgressBarModule} from "@angular/material/progress-bar";
import { TruncatePipePipe } from './truncate-pipe.pipe';
import { DeleteErrorDialogComponent } from './component/delete-error-dialog/delete-error-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    CategoryComponent,
    NotFoundComponent,
    CategoryEditComponent,
    LogoutComponent,
    ProductComponent,
    ErrorComponent,
    CustomerComponent,
    SellerComponent,
    PurchaseComponent,
    SaleComponent,
    SidebarComponent,
    ProductEditComponent,
    DeleteDialogComponent,
    PurchaseEditComponent,
    SaleEditComponent,
    SellerEditComponent,
    CustomerEditComponent,
    SellerFindDialogComponent,
    CustomerFindDialogComponent,
    TruncatePipePipe,
    DeleteErrorDialogComponent,
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        BrowserAnimationsModule,
        MatButtonModule,
        MatTreeModule,
        MatIconModule,
        MatCheckboxModule,
        MatFormFieldModule,
        MatCardModule,
        MatInputModule,
        FontAwesomeModule,
        ReactiveFormsModule,
        MatAutocompleteModule,
        MatOptionModule,
        MatPaginatorModule,
        MatTableModule,
        MatProgressSpinnerModule,
        MatSortModule,
        MatSnackBarModule,
        MatDatepickerModule,
        MatNativeDateModule,
        BarChartModule,
        PieChartModule,
        MatDialogModule,
        MatProgressBarModule
    ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from "./account/login/login.component";
import {AuthGuard} from './helper/auth.guard';
import {DashboardComponent} from './pages/dashboard/dashboard.component';
import {CategoryComponent} from "./pages/category/category-root/category.component";
import {NotFoundComponent} from "./pages/not-found/not-found.component";
import {CategoryEditComponent} from "./pages/category/category-edit/category-edit.component";
import {LogoutComponent} from "./account/logout/logout.component";
import {ProductComponent} from "./pages/product/product.component";
import {CustomerComponent} from "./pages/customer/customer.component";
import {PurchaseComponent} from "./pages/purchase/purchase.component";
import {SaleComponent} from "./pages/sale/sale.component";
import {SellerComponent} from "./pages/seller/seller.component";
import {ProductEditComponent} from "./pages/product/product-edit/product-edit.component";
import {SellerEditComponent} from "./pages/seller/seller-edit/seller-edit.component";
import {SaleEditComponent} from "./pages/sale/sale-edit/sale-edit.component";
import {PurchaseEditComponent} from "./pages/purchase/purchase-edit/purchase-edit.component";
import {CustomerEditComponent} from "./pages/customer/customer-edit/customer-edit.component";

export class RouteValues{

  static Login  = 'login';

  static Logout =  'logout';

  static Home = 'home';

  static Category = {
    Root : 'category',
    New  : 'category/new',
    Edit : 'category/edit',
  }

  static Customer ={
    Root : 'customer',
    New  : 'customer/new',
    Edit : 'customer/edit',
  }

  static Product = {
    Root : 'product',
    New  : 'product/new',
    Edit : 'product/edit',
  }

  static Purchase = {
    Root : 'purchase',
    New  : 'purchase/new',
    Edit : 'purchase/edit',
  }

  static Sale = {
    Root : 'sale',
    New  : 'sale/new',
    Edit : 'sale/edit',
  }

  static Seller = {
    Root : 'seller',
    New  : 'seller/new',
    Edit : 'seller/edit',
  }

  static NotFound = 'not-found';
}

const routes: Routes = [

  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path: RouteValues.Home, component: DashboardComponent, canActivate: [AuthGuard]},
  {path: RouteValues.Login, component: LoginComponent},
  {path: RouteValues.Logout, component: LogoutComponent, canActivate: [AuthGuard]},

  {path: RouteValues.Category.Root, component: CategoryComponent, canActivate: [AuthGuard]},
  {path: RouteValues.Category.New, component: CategoryEditComponent, canActivate: [AuthGuard]},
  {path: RouteValues.Category.Edit + '/:id', component: CategoryEditComponent, canActivate: [AuthGuard]},

  {path: RouteValues.Customer.Root, component: CustomerComponent, canActivate: [AuthGuard]},
  {path: RouteValues.Customer.New, component: CustomerEditComponent, canActivate: [AuthGuard]},
  {path: RouteValues.Customer.Edit + '/:id', component: CustomerEditComponent, canActivate: [AuthGuard]},

  {path: RouteValues.Product.Root, component: ProductComponent, canActivate: [AuthGuard]},
  {path: RouteValues.Product.New, component: ProductEditComponent, canActivate: [AuthGuard]},
  {path: RouteValues.Product.Edit + '/:id', component: ProductEditComponent, canActivate: [AuthGuard]},

  {path: RouteValues.Purchase.Root, component: PurchaseComponent, canActivate: [AuthGuard]},
  {path: RouteValues.Purchase.New, component: PurchaseEditComponent, canActivate: [AuthGuard]},
  {path: RouteValues.Purchase.Edit + '/:id', component: PurchaseEditComponent, canActivate: [AuthGuard]},

  {path: RouteValues.Sale.Root, component: SaleComponent, canActivate: [AuthGuard]},
  {path: RouteValues.Sale.New, component: SaleEditComponent, canActivate: [AuthGuard]},
  {path: RouteValues.Sale.Edit + '/:id', component: SaleEditComponent, canActivate: [AuthGuard]},

  {path: RouteValues.Seller.Root, component: SellerComponent, canActivate: [AuthGuard]},
  {path: RouteValues.Seller.New, component: SellerEditComponent, canActivate: [AuthGuard]},
  {path: RouteValues.Seller.Edit + '/:id', component: SellerEditComponent, canActivate: [AuthGuard]},

  {path: RouteValues.NotFound, component: NotFoundComponent},
  {path: '**', redirectTo: 'not-found'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}

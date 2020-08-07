import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { IProduct } from './../../shared/models/product';
import { ShopService } from './../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;

  constructor(
    private shopService: ShopService,
    private activateRoute: ActivatedRoute,
    private breadCrumbService: BreadcrumbService
  ) {
    this.breadCrumbService.set('@productDetails', '');
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(): void {
    this.shopService
      .getProduct(+this.activateRoute.snapshot.paramMap.get('id'))
      .subscribe(
        (product) => {
          this.product = product;
          this.breadCrumbService.set('@productDetails', product.name);
        },
        (error) => {
          console.log(error);
        }
      );
  }
}

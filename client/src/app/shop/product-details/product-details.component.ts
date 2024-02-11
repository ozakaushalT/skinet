import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/Models/Products';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product?: Product
  quantity: number = 1;
  quantityInBasket: number = 0;
  ngOnInit(): void {
    this.loadProduct();
  }
  constructor(private _shopService: ShopService, private _activatedRoutes: ActivatedRoute,
    private bcService: BreadcrumbService, private basketServie: BasketService) {
    this.bcService.set("@productDetails", " ");
  }

  loadProduct() {
    const id = this._activatedRoutes.snapshot.paramMap.get('id');
    if (id) {
      this._shopService.getProduct(+id).subscribe({
        next: res => {
          this.product = res;
          this.bcService.set("@productDetails", this.product.name);
          this.basketServie.basketSource$.pipe(take(1)).subscribe({
            next: basket => {
              const item = basket?.items.find(_ => _.id === +id);
              if (item) {
                this.quantity = item.quantity;
                this.quantityInBasket = item.quantity;
              }
            }
          });
        },
        error: err => console.log(err),
        complete: () => console.log("done")
      });
    }
  }

  incrementQuantity() {
    this.quantity++;
  }
  reduceQuantity() {
    this.quantity--;
  }
  updateBasket() {
    if (this.product) {
      if (this.quantity > this.quantityInBasket) {
        const itemsToAdd = this.quantity - this.quantityInBasket;
        this.quantityInBasket += itemsToAdd;
        this.basketServie.addItemToTheBasket(this.product, itemsToAdd);
      }
      else {
        //means reducing the quantity
        const itemsToRemove = this.quantityInBasket - this.quantity;
        this.quantityInBasket -= itemsToRemove;
        this.basketServie.removeItemFromBasket(this.product.id, itemsToRemove);
      }
    }
  }

  get buttonText() {
    return this.quantityInBasket === 0 ? "Add to basket" : "Update basket";
  }
}

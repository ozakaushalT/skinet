import { Component, Input } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { Product } from 'src/app/shared/Models/Products';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent {
  @Input() product?: Product

  constructor(private _basketService: BasketService) { }

  addItemToBasket() {
    this.product && this._basketService.addItemToTheBasket(this.product);
  }
}

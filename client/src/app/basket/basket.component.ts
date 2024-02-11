import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { BasketItem } from '../shared/Models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {
  constructor(public basketService: BasketService) { }

  reduceQuantity(id: number) {
    this.basketService.removeItemFromBasket(id, 1);
  }
  addQuantity(id: number, quantity: number = 1) {
    this.basketService.addQuantityToTheBasket(id, quantity);
  }
  deleteItemFromTheBasket(id: number) {
    this.basketService.deleteBasketItem(id);
  }
}

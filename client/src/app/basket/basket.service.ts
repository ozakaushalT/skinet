import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Basket, BasketItem, BasketTotals } from '../shared/Models/basket';
import { HttpClient } from '@angular/common/http';
import { Product } from '../shared/Models/Products';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<Basket | null>(null);
  basketSource$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<BasketTotals | null>(null);
  basketTotalSource$ = this.basketTotalSource.asObservable();
  constructor(private _http: HttpClient) { }

  getBasket(id: String) {
    return this._http.get<Basket>(this.baseUrl + 'basket?id=' + id).subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    })
  }

  setBasket(basket: Basket) {
    return this._http.post<Basket>(this.baseUrl + 'basket', basket).subscribe({
      next: updatedBasket => {
        this.basketSource.next(updatedBasket);
        this.calculateTotals();
      }
    })
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem("basket_id", basket.id);
    return basket;
  }

  addItemToTheBasket(item: Product | BasketItem, quantity: number = 1) {
    if (this.isProduct(item)) item = this.mapProductItemToBasketItem(item);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();// basket ?? null
    basket.items = this.addOrUpdateItem(basket.items, item, quantity);
    this.setBasket(basket);
  }

  deleteBasketItem(id: number) {
    const basket = this.getCurrentBasketValue();
    if (!basket) return;
    const itemTobeRemoved = basket.items.find(_ => _.id === id);
    if (!itemTobeRemoved) return;
    basket.items = basket.items.filter(_ => _.id != id);
    this.setBasket(basket);
  }

  removeItemFromBasket(id: number, quantity: number = 1) {
    const basket = this.getCurrentBasketValue();
    if (!basket) return;
    const itemTobeRemoved = basket.items.find(_ => _.id === id);
    if (!itemTobeRemoved) return;
    itemTobeRemoved.quantity -= quantity;
    if (itemTobeRemoved.quantity === 0) {
      basket.items = basket.items.filter(_ => _.id != id);
    }
    if (basket.items.length > 0) {
      this.setBasket(basket);
    } else {
      this.deleteBasket(basket);
    }
  }

  addQuantityToTheBasket(id: number, quantity: number = 1) {
    const basket = this.getCurrentBasketValue();
    if (!basket) return;
    const itemTobeAdded = basket.items.find(_ => _.id === id);
    if (!itemTobeAdded) return;
    itemTobeAdded.quantity += quantity;
    this.setBasket(basket);
  }

  private deleteBasket(basket: Basket) {
    return this._http.delete(this.baseUrl + "basket?id=" + basket.id).subscribe({
      next: () => {
        this.basketSource.next(null);
        this.basketTotalSource.next(null);
        localStorage.removeItem("basket_id");
      }
    })
  }

  private addOrUpdateItem(items: BasketItem[], itemToAdd: BasketItem, quantity: number): BasketItem[] {
    const existingItem = items.find(_ => _.id === itemToAdd.id);
    if (existingItem) {
      existingItem.quantity += quantity;
    }
    else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }

    return items;
  }

  private mapProductItemToBasketItem(item: Product): BasketItem {
    return {
      id: item.id,
      productName: item.name,
      brand: item.productBrand,
      type: item.productType,
      pictureUrl: item.pictureURL,
      price: item.price,
      quantity: 0
    }
  }

  private calculateTotals() {
    const basket = this.getCurrentBasketValue();
    if (!basket) return;
    const shipping = 0;
    const subTotal = basket.items.reduce((prevVal, curVal) => prevVal + (curVal.quantity * curVal.price), 0);
    const total = shipping + subTotal;
    this.basketTotalSource.next({ shipping, subTotal, total });
  }

  private isProduct(item: Product | BasketItem): item is Product {
    return (item as Product).productBrand !== undefined;
  }
}

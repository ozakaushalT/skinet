import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Skinet | Kaushal';
  constructor(
    private _basketService: BasketService,
    private _accntService: AccountService
  ) {}
  ngOnInit(): void {
    this.loadUser();
    this.loadBasket();
  }

  loadBasket() {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) {
      this._basketService.getBasket(basketId);
    }
  }

  loadUser() {
    let token = localStorage.getItem('token');
    this._accntService.loadCurrentUser(token).subscribe();
  }
}

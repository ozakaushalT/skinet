import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/Models/Products';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product?: Product
  ngOnInit(): void {
    this.loadProduct();
  }
  constructor(private _shopService: ShopService, private _activatedRoutes: ActivatedRoute) {

  }

  loadProduct() {
    const id = this._activatedRoutes.snapshot.paramMap.get('id');
    if (id) {
      this._shopService.getProduct(+id).subscribe({
        next: res => this.product = res,
        error: err => console.log(err),
        complete: () => console.log("done")
      });
    }
  }
}

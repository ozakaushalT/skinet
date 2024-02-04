import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Product } from '../shared/Models/Products';
import { ShopService } from './shop.service';
import { Types } from '../shared/Models/types';
import { Brands } from '../shared/Models/brands';
import { ProdParams } from '../shared/Models/ProductParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: Product[] = [];
  brands: Brands[] = [];
  types: Types[] = [];

  shopParams = new ProdParams();
  @ViewChild('searchKey') search?: ElementRef;
  constructor(private _shopService: ShopService) {
  }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }
  getProducts() {
    this._shopService.getProducts(this.shopParams).subscribe({
      next: res => { this.products = res.data; this.shopParams.PageIndex = res.pageIndex; this.shopParams.PageSize = res.pageSize; this.shopParams.Count = res.count },
      error: err => console.log(err),
      complete: () => console.log("done")
    });
  }
  getBrands() {
    this._shopService.getBrands().subscribe({
      next: res => this.brands = [{ id: 0, name: 'All' }, ...res],
      error: err => console.log(err),
      complete: () => console.log("done")
    });
  }
  getTypes() {
    this._shopService.getTypes().subscribe({
      next: res => this.types = [{ id: 0, name: 'All' }, ...res],
      error: err => console.log(err),
      complete: () => console.log("done")
    });
  }
  onBrandIdSelected(event: any) {
    this.shopParams.brandId = event.target.value;
    this.shopParams.PageIndex = 1;
    this.getProducts();
  }
  onTypeIdSelected(event: any) {
    this.shopParams.typeId = event.target.value;
    this.shopParams.PageIndex = 1;
    this.getProducts();
  }
  onProductSearched() {
    this.shopParams.Search = this.search?.nativeElement.value;
    this.shopParams.PageIndex = 1;
    this.getProducts();
  }
  onResetButtonClicked() {
    this.shopParams.Search = "";
    this.shopParams.PageIndex = 1;
    this.getProducts();
  }
  onSortSelected(event: any) {
    this.shopParams.sortDirection = event.target.value;
    this.getProducts();
  }
  onPageChanged(event: any) {
    this.shopParams.PageIndex = event.page;
    this.getProducts();
  }
}

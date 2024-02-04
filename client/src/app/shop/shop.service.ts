import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/Models/Pagination';
import { Product } from '../shared/Models/Products';
import { Brands } from '../shared/Models/brands';
import { Types } from '../shared/Models/types';
import { ProdParams } from '../shared/Models/ProductParams';

@Injectable({
  providedIn: 'root' //will be loaded when root loads up
})
export class ShopService {
  baseURL: string = "https://localhost:5001/api/";
  constructor(private _http: HttpClient) { }

  getProducts(shopParams: ProdParams) {
    let params = new HttpParams();
    if (shopParams.typeId > 0) params = params.append("typeId", shopParams.typeId);
    if (shopParams.brandId > 0) params = params.append("brandId", shopParams.brandId);
    if (shopParams.Search != "") params = params.append("Search", shopParams.Search);
    params = params.append("sortDirection", shopParams.sortDirection);
    params = params.append("pageIndex", shopParams.PageIndex);
    return this._http.get<Pagination<Product[]>>(this.baseURL + "products", { params });
  }
  getBrands() {
    return this._http.get<Brands[]>(this.baseURL + "products/brands");
  }
  getTypes() {
    return this._http.get<Types[]>(this.baseURL + "products/types");
  }
  getProduct(id: number) {
    return this._http.get<Product>(this.baseURL + "products/" + id);
  }
}

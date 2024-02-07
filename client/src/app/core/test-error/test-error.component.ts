import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent {
  baseUrl: string = environment.apiUrl;
  constructor(private _http: HttpClient) { }

  get404Error() {
    this._http.get(this.baseUrl + "product/555").subscribe({
      next: (res) => console.log(res),
      error: (err) => console.log(err),
      complete: () => console.log("done")
    });
  }
  get500Error() {
    this._http.get(this.baseUrl + "buggy/servererror").subscribe({
      next: (res) => console.log(res),
      error: (err) => console.log(err),
      complete: () => console.log("done")
    });
  }
  get400Error() {
    this._http.get(this.baseUrl + "buggy/badrequest").subscribe({
      next: (res) => console.log(res),
      error: (err) => console.log(err),
      complete: () => console.log("done")
    });
  }
  get400ValidationError() {
    this._http.get(this.baseUrl + "product/hello").subscribe({
      next: (res) => console.log(res),
      error: (err) => console.log(err),
      complete: () => console.log("done")
    });
  }
}

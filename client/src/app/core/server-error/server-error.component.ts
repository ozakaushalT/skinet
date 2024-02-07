import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.scss']
})
export class ServerErrorComponent {
  errors: any;
  constructor(private _router: Router) {
    const navigation = this._router.getCurrentNavigation();
    this.errors = navigation?.extras?.state?.['error'];
  }


}

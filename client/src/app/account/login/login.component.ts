import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  });
  constructor(
    private _accService: AccountService,
    private _router: Router,
    private _route: ActivatedRoute
  ) {}
  // returnUrlPresent: boolean = false;
  // redirectUserTo: string = '';
  returnUrl: string = '';
  ngOnInit(): void {
    // this._route.queryParams.forEach((param) => {
    //   if (param['returnUrl']) {
    //     this.returnUrlPresent = true;
    //     this.redirectUserTo = param['returnUrl'];
    //   } else {
    //     this.returnUrlPresent = false;
    //   }
    // });
    this.returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/shop';
  }

  onSubmit() {
    this._accService.login(this.loginForm.value).subscribe({
      next: () => {
        // if (this.returnUrlPresent)
        //   this._router.navigateByUrl(`${this.redirectUserTo}`);
        // else {
        //   this._router.navigateByUrl('/shop');
        // }
        this._router.navigateByUrl(this.returnUrl);
      },
    });
  }
}

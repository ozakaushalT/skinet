import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../shared/Models/user';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseURL: String = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUserSource$ = this.currentUserSource.asObservable();
  constructor(private _http: HttpClient, private _router: Router) { }

  login(values: any) {
    return this._http.post<User>(this.baseURL + "account/login", values).pipe(
      map(user => {
        localStorage.setItem("token", user.token);
        this.currentUserSource.next(user);
      })
    )
  }

  register(values: any) {
    return this._http.post<User>(this.baseURL + "account/register", values).pipe(
      map(user => {
        localStorage.setItem("token", user.token);
        this.currentUserSource.next(user);
      })
    )
  }

  logOut() {
    localStorage.removeItem("token");
    this.currentUserSource.next(null);
    this._router.navigateByUrl("/");
  }

  checkEmailExist(email: string) {
    this._http.get<boolean>(this.baseURL + "account/emailexists?email=" + email);
  }
}

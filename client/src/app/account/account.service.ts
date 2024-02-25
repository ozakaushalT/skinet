import { Injectable } from '@angular/core';
import { BehaviorSubject, ReplaySubject, map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../shared/Models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseURL: String = environment.apiUrl;
  // private currentUserSource = new BehaviorSubject<User | null>(null);
  private currentUserSource = new ReplaySubject<User | null>(1); // 1 value to cache - by default auth guard checks for this and this returns null-- resulting user to redirect to login page
  currentUserSource$ = this.currentUserSource.asObservable();
  constructor(private _http: HttpClient, private _router: Router) {}

  login(values: any) {
    return this._http.post<User>(this.baseURL + 'account/login', values).pipe(
      map((user) => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      })
    );
  }

  register(values: any) {
    return this._http
      .post<User>(this.baseURL + 'account/register', values)
      .pipe(
        map((user) => {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        })
      );
  }

  logOut() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this._router.navigateByUrl('/');
  }

  checkEmailExist(email: string) {
    return this._http.get<boolean>(
      this.baseURL + 'account/emailexists?email=' + email
    );
  }

  loadCurrentUser(token: string | null) {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    } else {
      let header = new HttpHeaders();
      header = header.set('Authorization', `Bearer ${token}`);
      return this._http
        .get<User>(this.baseURL + 'account', { headers: header })
        .pipe(
          map((user) => {
            if (user) {
              localStorage.setItem('token', user.token);
              this.currentUserSource.next(user);
              return user;
            } else {
              return null;
            }
          })
        );
    }
  }
}

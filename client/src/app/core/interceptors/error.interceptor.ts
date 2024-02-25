import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private _router: Router, private _toastr: ToastrService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error) {
          if (error.status == 400) {
            this._toastr.error(error.message, error.status.toString());
          }
          if (error.status == 401) {
            this._toastr.error(
              'You are not authorized...',
              error.status.toString()
            );
          }
          // if (error.status == 404) {
          //   this._toastr.error(error.message, error.status.toString());
          //   this._router.navigateByUrl("/not-found");
          // }
          if (error.status == 500) {
            const navigationExtras: NavigationExtras = {
              state: { error: error.error },
            };
            this._router.navigateByUrl('/server-error', navigationExtras);
          }
        }
        return throwError(() => new Error(error.message));
      })
    );
  }
}

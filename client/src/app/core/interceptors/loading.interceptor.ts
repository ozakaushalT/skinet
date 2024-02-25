import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, delay, finalize } from 'rxjs';
import { ServiceBusyService } from '../services/service-busy.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private _busyService: ServiceBusyService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    if (!request.url.includes('emailExists')) this._busyService.busy();
    return next.handle(request).pipe(finalize(() => this._busyService.idle()));
  }
}

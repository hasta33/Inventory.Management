import { Injectable, Injector } from "@angular/core";
import {
  HttpEvent,
  HttpRequest,
  HttpHandler,
  HttpInterceptor
} from "@angular/common/http";
import { Observable } from "rxjs";
import { finalize, delay} from "rxjs/operators";
import {NgxSpinnerService} from "ngx-spinner";

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private injector: Injector) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    /*if (!req.url.includes("albums")) {
      return next.handle(req);
    }*/

    //const loaderService = this.injector.get(LoadingService);
    const loaderService = this.injector.get(NgxSpinnerService);

    //if (req.method === 'GET' || 'POST' || 'PUT' || 'DELETE') {
    //  console.log('get metodugeldi')
      loaderService.show();
    //}


    return next.handle(req).pipe(
      delay(250),
      finalize(() => {
        loaderService.hide()
      })
    );
  }
}

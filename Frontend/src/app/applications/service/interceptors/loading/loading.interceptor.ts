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
  constructor(private injector: Injector, private spinner:NgxSpinnerService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    /*if (!req.url.includes("albums")) {
      return next.handle(req);
    }*/

    //const loaderService = this.injector.get(LoadingService);
    const loaderService = this.injector.get(NgxSpinnerService);

    loaderService.show();

    return next.handle(req).pipe(
      delay(1000),
      finalize(() => {
        loaderService.hide()
      })
    );
  }
}

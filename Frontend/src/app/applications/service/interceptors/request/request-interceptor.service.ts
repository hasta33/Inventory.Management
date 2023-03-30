import {Injectable, Injector} from '@angular/core';
import {HttpClient, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {Observable} from "rxjs";
import {AuthService} from "../../auth/auth.service";

@Injectable({
  providedIn: 'root'
})
export class RequestInterceptorService implements HttpInterceptor {

  constructor(private inject:Injector, private service: AuthService, private httpClient: HttpClient) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    console.log('request inceptor alanına girdi')
    let jwtToken = request.clone({
      setHeaders: {
        //Authorization: 'bearer '+authservice.GetAccessToken()
        Authorization: 'bearer '+localStorage.getItem('access_token')
      }
    });
    return next.handle(jwtToken);
  }


}

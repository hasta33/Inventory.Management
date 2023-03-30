import {Injectable, Injector} from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor, HttpParams,
  HttpRequest
} from "@angular/common/http";
import {catchError, Observable, switchMap, throwError} from "rxjs";
import {AuthService} from "../../auth/auth.service";
import {constants} from "../../../constants/constants";
import {environment} from "../../../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor{

  constructor(private inject:Injector, private service: AuthService, private httpClient: HttpClient) {}
  /*intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let authservice=this.inject.get(AuthService);
    let jwtToken = request.clone({
      setHeaders: {
        //Authorization: 'bearer '+authservice.GetAccessToken()
        Authorization: 'bearer '+localStorage.getItem('access_token')
      }
    });
    console.log('interceptor içine girdi')
    return next.handle(jwtToken);
  }
  AddTokenHeader(request: HttpRequest<any>, token:any) {
    return request.clone({headers: request.headers.set('Authorization', 'bearer ' + token)});
  }*/


  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let authService = this.inject.get(AuthService);
    let authReq = request;
    authReq = this.AddTokenHeader(request, localStorage.getItem('access_token'))//authService.GetAccessToken());
    return next.handle(authReq).pipe(
      catchError(errorData => {
        console.log(errorData)
        console.log('token interceptor alanına girdi')
       /* if (errorData.status === 401) {
          // need to implement logout
          //authservice.logout();
          // refresh token logic
          console.log('401 geldi refresh token alınması gerekiyor')
            return this.handleRefrehToken(request, next);
        }*/
        return throwError(errorData);
      })
    );
  }
  handleRefrehToken(request: HttpRequest<any>, next: HttpHandler) {
    console.log('handle refresh token alanına girdi')
    let authService = this.inject.get(AuthService);
    return authService.GenerateRefreshToken().pipe(
      switchMap((data: any) => {
        authService.SaveTokens(data);
        return next.handle(this.AddTokenHeader(request,data.jwtToken))
      }),
      catchError(errorData=>{
        authService.logout();
        return throwError(errorData)
      })
    );
  }
  AddTokenHeader(request: HttpRequest<any>, token: any) {
    return request.clone({ headers: request.headers.set('Authorization', 'bearer ' + token) });
  }

}


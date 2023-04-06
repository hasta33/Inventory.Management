import {Injectable, Injector} from '@angular/core';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from "@angular/common/http";
import {catchError, Observable, switchMap, throwError} from "rxjs";
import {AuthService} from "../../auth/auth.service";
import {timer} from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class TokenInterceptorService implements HttpInterceptor{

  constructor(private inject:Injector) {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let authService = this.inject.get(AuthService);

    //permission token var mı
    if (window.sessionStorage.getItem('access_permission_token')) {
      request = request.clone({
        setHeaders: {Authorization: `Bearer ${window.sessionStorage.getItem('access_permission_token')}`}
      });
    }

    if (request.headers.has('Login')) {
      console.log('login istegi yapılıyor')
      request = this.AddTokenHeader(request, window.sessionStorage.getItem('access_token'))
      return next.handle(request).pipe(
        switchMap(() => {
          /*timer(1000).subscribe(() => {
            authService.GetTokenPermissions().subscribe({
              next:(response:any) => {
                console.log(response)
                window.sessionStorage.setItem('access_permission_token', response.access_token)
                window.sessionStorage.setItem('refresh_permission_token', response.access_token)
              },
              complete: () => { },
              error: (err) => {
                console.log(err)
                //authService.logout();
              }
            });
          })*/
          return next.handle(request);
        }),
        catchError(errorData => {
          if (errorData.status === 401) {
            return this.handleRefreshToken(request, next);
          }
          return throwError(errorData);
        })
      )
    } else if (request.headers.has('Permission')) {
      console.log('Permission istegi yapılıyor')
      return next.handle(request).pipe(
        switchMap(e => {
          return next.handle(request);
        }),
        catchError(errorData => {
          if (errorData.status === 401) {
            console.log(errorData)
            return this.handleRefreshToken(request, next);
          }
          return throwError(errorData)
        })
      );
    } else {
      console.log('normal sayfa istegi yapılıyor')
      return next.handle(request).pipe(
        catchError((err) => {
          //console.log(err)
          if (err instanceof HttpErrorResponse) {
            if (err.status === 401) {
              authService.logout();
              // redirect user to the logout page
              //console.log('TokenInterceptor: bu alan 401 geldi tekrar aktif edilecek')
              return this.handleRefreshToken(request, next);
            }
          }
          return throwError(err);
        })
      )
    }
  }

  handleRefreshToken(request: HttpRequest<any>, next: HttpHandler) {
    let authService = this.inject.get(AuthService);
    return authService.GenerateRefreshToken().pipe(
      switchMap((data: any) => {
        authService.SaveTokens(data);
        console.log('handleRefresh token alanına girdi')
        return next.handle(this.AddTokenHeader(request,data.jwtToken))
      }),
      catchError(errorData=>{
        authService.logout();
        return throwError(errorData)
      })
    );
  }
  AddTokenHeader(request: HttpRequest<any>, token: any) {
    return request.clone({ headers: request.headers.set('Authorization', 'Bearer ' + token) });
  }
}

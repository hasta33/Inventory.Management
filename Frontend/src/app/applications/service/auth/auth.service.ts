import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {constants} from "../../constants/constants";
import {Observable, throwError} from "rxjs";
import {environment} from "../../../../environments/environment";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private httpClient: HttpClient,
              private router: Router) { }

  ProceedLogin(UserCredentials: any): Observable<any> {
    const body = new HttpParams()
      .set('username', UserCredentials.username)
      .set('password', UserCredentials.password)
      .set('client_id', `${environment.keycloak_client_id}`)
      .set('client_secret', `${environment.keycloak_client_secret}`)
      .set('grant_type', `${environment.keycloak_grant_type}`);

    return this.httpClient.post(constants.GET_TOKEN,
      body.toString(),
      {
        headers: new HttpHeaders()
          .set('Content-Type', 'application/x-www-form-urlencoded')
      }
    );
  }

  GetTokenPermissions() {
    const body = new HttpParams()
      .set('grant_type', `${environment.keycloak_permissions_grant_type}`)
      .set('claim_token', `${localStorage.getItem('access_token')}`)
      .set('client_id', `${environment.keycloak_client_id}`)
      .set('client_secret', `${environment.keycloak_client_secret}`)
      .set('audience', `${environment.keycloak_audience}`);

    console.log('permissions token alanına girdi')
    return this.httpClient.post(constants.GET_TOKEN,
      body.toString(),
      {
        headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
      });
  }

  IsLoggedIn(){
    return localStorage.getItem('access_token')!=null;
  }
  GetAccessToken(){
    return localStorage.getItem('access_token')||'';
  }
  HaveAccess(){
    var loggintoken=localStorage.getItem('access_token')||'';
    var _extractedtoken=loggintoken.split('.')[1];
    var _atobdata=atob(_extractedtoken);
    var _finaldata=JSON.parse(_atobdata);
    if(_finaldata.role=='admin'){
      return true
    } else {
      //alert('you not having access');
      return false
    }
  }

  GenerateRefreshToken() {
    console.log('generate refresh token alanına girdi')
    const input = new HttpParams()
      .set('client_id', `${environment.keycloak_client_id}`)
      .set('client_secret', `${environment.keycloak_client_secret}`)
      .set('grant_type', 'refresh_token')
      .set('refresh_token', this.GetRefreshToken())
      .set('access_token', this.GetAccessToken())

    return this.httpClient.post(constants.GET_TOKEN, input.toString(), { headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')} );// + 'refresh', input);
  }
  GetRefreshToken() {
    return localStorage.getItem("refresh_token") || '';
  }
  SaveTokens(tokenData: any) {
    localStorage.setItem('access_token', tokenData.jwtToken);
    localStorage.setItem('refresh_token', tokenData.refreshToken);
  }

  logout() {
    //alert('Your session expired')
    console.log('token süresi bitti')
    localStorage.clear();
    this.router.navigateByUrl('/login');
  }



  private handleError(error: any) {
    let errorMessage = '';
    if (error.errorMessage instanceof ErrorEvent) {
      console.log(error)
      errorMessage = error.error.errorMessage;
    } else {
      errorMessage = `\nStatusCode: ${error.status}}\nResponse: ${error.message}`;
    }
    //window.alert(errorMessage);
    return throwError(() => {
      return errorMessage;
    });
  }
}

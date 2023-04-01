import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {constants} from "../../constants/constants";
import {Observable} from "rxjs";
import {environment} from "../../../../environments/environment";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private httpClient: HttpClient,
              private router: Router) { }

  Login(UserCredentials: any): Observable<any> {
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
          .set('Login', 'login_component')
      }
    );
  }

  GetTokenPermissions() {
    console.log('auth token permission')
    const body = new HttpParams()
      .set('grant_type', `${environment.keycloak_permissions_grant_type}`)
      .set('claim_token', `${window.sessionStorage.getItem('access_token')}`)
      .set('client_id', `${environment.keycloak_client_id}`)
      .set('client_secret', `${environment.keycloak_client_secret}`)
      .set('audience', `${environment.keycloak_audience}`);

    return this.httpClient.post(constants.GET_TOKEN,
      body.toString(),
      {
        headers: new HttpHeaders()
          .set('Content-Type', 'application/x-www-form-urlencoded')
          .set('Permission', 'permission_token')
      });
  }

  IsLoggedIn(){
    return window.sessionStorage.getItem('access_token')!=null;
  }

  HaveAccess(){
    const loginToken = window.sessionStorage.getItem('access_token') || '';
    const _extractedToken=loginToken.split('.')[1];
    const _atobData=atob(_extractedToken);
    const _finalData=JSON.parse(_atobData);
    if(_finalData.role=='admin'){
      return true
    } else {
      //alert('you not having access');
      return false
    }
  }

  GenerateRefreshToken() {
    //console.log('generate refresh token alanına girdi')
    const input = new HttpParams()
      .set('client_id', `${environment.keycloak_client_id}`)
      .set('client_secret', `${environment.keycloak_client_secret}`)
      .set('grant_type', 'refresh_token')
      .set('refresh_token', `${window.sessionStorage.getItem('refresh_token')}`)


    return this.httpClient.post(constants.GET_TOKEN, input.toString(), {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    });
  }

  SaveTokens(tokenData: any) {
    window.sessionStorage.setItem('access_token', tokenData.jwtToken);
    window.sessionStorage.setItem('refresh_token', tokenData.refreshToken);
  }

  logout() {
    //alert('Your session expired')
    //console.log('token süresi bitti')
    window.sessionStorage.clear();
    this.router.navigateByUrl('/login');
  }
}

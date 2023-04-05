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
    console.log('Auth Service: Login')
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
    console.log('Auth Service: Get Token Permissions')
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
    console.log('Auth Service: IsLogged')
    return window.sessionStorage.getItem('access_token')!=null;
  }


  HaveAccess(){
    console.log('Auth Service: HaveAccess')
    const loginToken = window.sessionStorage.getItem('access_permission_token') || '';
    const _extractedToken=loginToken.split('.')[1];
    const _atobData=atob(_extractedToken);
    const _finalData=JSON.parse(_atobData);

    //_finalData.resource_access.api_inventory.roles=='CompanyRole'
      if(_finalData.role=='admin'){
        return true
      } else {
        //alert('you not having access');
        return false
      }
  }

  GenerateRefreshToken() {
    console.log('Auth Service: Generate Refresh Token')
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
    console.log('Auth Service: SaveTokens')
    window.sessionStorage.setItem('access_token', tokenData.jwtToken);
    window.sessionStorage.setItem('refresh_token', tokenData.refreshToken);
  }

  logout() {
    console.log('Auth Service: Logout')
    //alert('Your session expired')
    //console.log('token süresi bitti')
    window.sessionStorage.clear();
    this.router.navigateByUrl('/login');
  }

}

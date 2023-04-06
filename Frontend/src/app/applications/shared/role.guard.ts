import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, Router, RouterStateSnapshot} from '@angular/router';
import {AuthService} from "../service/auth/auth.service";

@Injectable({
  providedIn: 'root'
})
export class RoleGuard {
  constructor(private service: AuthService, private route: Router) {
  }
  private el: HTMLDivElement | undefined;

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if(!this.service.IsLoggedIn()) {
      this.route.navigate(['auth/login'])
      return false;
    }

    const roles  = route.data['roles'];//make sure you are getting the roles here
    if(!roles?.length){
      return true; //no role applied on route so just return true
    }
    const userRoles = this.service.getRoles();
    let isAuthorised=!!userRoles?.length && roles.some((r: any)=>userRoles.includes(r))
    if (isAuthorised) {
      // authorized so return true
      //console.log('RoleGuard: user authorized')
      return true;
    }
    //console.log('RoleGuard: login değil');
    this.route.navigate(['auth/access'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}

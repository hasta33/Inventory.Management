import {Component, OnInit} from '@angular/core';
import {MessageService} from "primeng/api";
import {AuthService} from "../../service/auth/auth.service";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  providers: [MessageService]
})
export class DashboardComponent implements OnInit {

  constructor(private authService: AuthService) {
  }

  ngOnInit() {
    //this.getTokenPermissions();
  }


  /*getTokenPermissions() {
    this.authService.GetTokenPermissions().subscribe({
      next:(response:any) => {
        //console.log(response)
        console.log('Auth Service: Dashboard')
        window.sessionStorage.setItem('access_permission_token', response.access_token)
        window.sessionStorage.setItem('refresh_permission_token', response.access_token);
      },
      complete: () => { },
      error: (err) => {
        console.log(err)
        //authService.logout();
      }
    });
  }*/







}

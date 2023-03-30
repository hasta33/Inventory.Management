import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../service/auth/auth.service";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.authService.GetTokenPermissions().subscribe({
      next: (token: any) => {
        console.log('permission token alındı')
        localStorage.setItem('access_permission_token', token.access_token)
        localStorage.setItem('refresh_permission_token', token.refresh_token)
      },
      complete: () => {

      },
      error: (e) => {
        console.log('permission token alınamadı', e)
      }
    });
  }
}

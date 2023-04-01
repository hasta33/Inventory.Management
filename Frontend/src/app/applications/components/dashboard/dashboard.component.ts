import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../service/auth/auth.service";
import {constants} from "../../constants/constants";
import {MessageService} from "primeng/api";
import {timer} from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  providers: [MessageService]
})
export class DashboardComponent implements OnInit {

  constructor(private authService: AuthService, private messageService: MessageService) {}

  ngOnInit() {
    //timer(1000).subscribe(() => {
    //  this.fonk();
    //})
  }
/*
  fonk() {
    this.authService.GetTokenPermissions().subscribe({
      next: (token: any) => {
        window.sessionStorage.setItem('access_permission_token', token.access_token)
        window.sessionStorage.setItem('refresh_permission_token', token.access_token)
      },
      complete: () => {

      },
      error: (e) => {
        this.messageService.add({ severity: 'error', summary: 'Hata', detail: `Kullanıcı adı veya şifre hatalı \n${e.error.error_description}`, life: constants.TOAST_ERROR_LIFETIME });
        this.messageService.clear('c');
      }
    });
  }*/
}

import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../../service/auth/auth.service";
import {Router} from "@angular/router";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {constants} from "../../../constants/constants";
import {MessageService} from "primeng/api";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [MessageService]
})
export class LoginComponent implements OnInit{

  //valCheck: string[] = ['remember'];

  //password!: string;
  //responseData: any;

  constructor(private service: AuthService,
              private route: Router,
              private messageService: MessageService ) { }

  loginGroup = new FormGroup({
    username: new FormControl("", Validators.required),
    password: new FormControl("", Validators.required)
  })

  ngOnInit(): void {
  }

  Login() {
    if (this.loginGroup.valid) {
      this.service.Login(this.loginGroup.value)
        .subscribe({
          next: (response: any) => {
            window.sessionStorage.setItem('access_token', response.access_token);
            window.sessionStorage.setItem('refresh_token', response.refresh_token);
            this.route.navigate(['']);
          },
          complete: () => { },
          error: (e) => {
            //console.log(e)
            this.messageService.add({ severity: 'error', summary: 'Hata', detail: `Kullanıcı adı veya şifre hatalı \n${e.error.error_description}`, life: constants.TOAST_ERROR_LIFETIME });
            this.messageService.clear('c');
          }
        });
    }
  }
}

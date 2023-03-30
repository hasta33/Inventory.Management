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

  Login = new FormGroup({
    username: new FormControl("", Validators.required),
    password: new FormControl("", Validators.required)
  })

  ngOnInit(): void {
  }

  ProceedLogin() {
    if (this.Login.valid) {
      this.service.ProceedLogin(this.Login.value)
        .subscribe({
          next: (response: any) => {
            localStorage.setItem('access_token', response.access_token)
            localStorage.setItem('refresh_token', response.refresh_token)
            this.route.navigate(['']);
          },
          complete: () => {
            this.service.GetTokenPermissions().subscribe({
              next: (token: any) => {
                console.log('permission token alındı')
                localStorage.setItem('access_permission_token', token.access_token)
                localStorage.setItem('refresh_permission_token', token.refresh_token)
              },
              complete: () => {

              },
              error: (e) => {
                console.log('permission token alınamadı')
                this.messageService.add({ severity: 'error', summary: 'Hata', detail: `Kullanıcı adı veya şifre hatalı \n${e.error.error_description}`, life: constants.TOAST_ERROR_LIFETIME });
                this.messageService.clear('c');
              }
            });
          },
          error: (e) => {
            this.messageService.add({ severity: 'error', summary: 'Hata', detail: `Kullanıcı adı veya şifre hatalı \n${e.error.error_description}`, life: constants.TOAST_ERROR_LIFETIME });
            this.messageService.clear('c');
            return;
          }
        });

    }
  }
}


//console.log(this.Login.value);
/*this.service.ProceedLogin(this.Login.value).subscribe(result => {
  console.log(result)
  if(result!=null){
    //this.responseData = result;
    localStorage.setItem('access_token', result.access_token)
    localStorage.setItem('refresh_token', result.refresh_token)
    this.route.navigate([''])
  }
});*/

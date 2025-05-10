import { Component, OnDestroy } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../service/auth.service';
import { Subscription } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnDestroy {
  model: LoginRequest
  subscription?: Subscription

  constructor(private authService: AuthService,
    private cookieService: CookieService,
    private route: Router
  ) {
    this.model = {
      userName: "",
      password: ""
    }
  }

  onFormSubmit(): void {
    this.subscription = this.authService.Login(this.model).subscribe({
      next: (response) => {
        this.cookieService.set('Authorization', `Bearer ${response.token}`, undefined, '/', undefined, true, 'Strict');
        this.authService.setUser({
          userName: response.userName,
          roles: response.roles
        });
        this.route.navigateByUrl('/');
      }
    });
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }
}

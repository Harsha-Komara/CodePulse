import { Injectable } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResponse } from '../models/login-response.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../models/user.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  $user = new BehaviorSubject<User | undefined>(undefined)

  constructor(private http: HttpClient, private cookieService: CookieService) { }

  Login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}/api/auth/login`, request)
  }

  setUser(user: User): void {

    this.$user.next(user);

    localStorage.setItem('UserName', user.userName);
    localStorage.setItem('Roles', user.roles.join(','))
  }

  getUser(): User | undefined {
    const userName = localStorage.getItem('UserName');
    const roles = localStorage.getItem('Roles');
    if (userName && roles) {
      const user: User = {
        userName: userName,
        roles: roles.split(',')
      }
      return user;
    }
    return undefined;
  }

  user(): Observable<User | undefined> {
    return this.$user.asObservable();
  }

  Logout(): void {
    localStorage.clear();
    this.cookieService.delete('Authorization', '/');
    this.$user.next(undefined);
  }

}

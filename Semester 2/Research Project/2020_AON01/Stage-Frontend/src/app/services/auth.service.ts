import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '@environments/environment';
import { HttpClient } from '@angular/common/http';
import { Claim } from '../classes/claim';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private loginURL = environment.apiURL + 'Authentication/token/';
  private registerURL = environment.apiURL + 'Authentication/register/';
  private rolesURL = environment.apiURL + 'Claims/';
  private loggedIn = new BehaviorSubject<boolean>(this.isTokenValid());
  constructor(
    private router: Router,
    private http: HttpClient,
    private jwtHelper: JwtHelperService
  ) {}

  register(data): Observable<any> {
    return this.http.post(this.registerURL, data);
  }

  login(data): Observable<any> {
    return this.http.post(this.loginURL, data, { responseType: 'text' });
  }

  logOut(): void {
    localStorage.clear();
    this.updateLoggedInStatus();
    this.router.navigate(['login']);
  }

  updateLoggedInStatus(): void {
    this.loggedIn.next(this.isTokenValid());
  }

  saveToken(token): void {
    localStorage.setItem('token', token);
  }

  getToken(): string {
    return localStorage.getItem('token');
  }

  getUserClaim(): Observable<Claim> {
    return this.http.get<Claim>(this.rolesURL);
  }

  getLoggedInStatus(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }

  isTokenValid(): boolean {
    const token = this.getToken();
    if (!token) {
      return false;
    }
    return !this.jwtHelper.isTokenExpired(token);
  }
}

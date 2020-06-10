import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuardService {
  constructor(public authService: AuthService, protected router: Router) {}

  canActivate(): boolean {
    if (!this.authService.isTokenValid()) {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }
}

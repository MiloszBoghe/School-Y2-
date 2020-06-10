import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthService } from './auth.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class RoleGuard implements CanActivate {
  constructor(private router: Router, private authService: AuthService) {}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const roles = next.data.roles as Array<string>;
    return this.authService.getUserClaim().pipe(
      map((claim) => {
        if (!roles.includes(claim.role)) {
          this.router.navigate(['/home']);
          return false;
        }
        return true;
      })
    );
  }
}

import {Component, OnInit} from '@angular/core';
import {Claim} from '@app/classes/claim';
import {AuthService} from '@app/services/auth.service';
import {ActivatedRoute, Router} from '@angular/router';
import {Bedrijf} from '@app/classes/bedrijf';
import {User} from '@app/classes/user';
import {ApiService} from '@app/services/api.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  claim: Claim = null;
  bedrijf: Bedrijf = null;
  user: User = null;
  isLoaded = false;
  selectedUserId: number;

  constructor(
    private API: ApiService,
    private router: Router,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute
  ) {
    this.activatedRoute.params.subscribe(params => {
      this.selectedUserId = params.id;
    });
  }

  ngOnInit(): void {
    this.authService.getUserClaim().subscribe((claim) => {
      this.claim = claim;
      if (
        this.claim.role === 'reviewer' ||
        this.claim.role === 'student' ||
        this.claim.role === 'bedrijf'
      ) {
        this.API.getUserProfile(+this.claim.id).subscribe((u) => {
          if (this.claim.role === 'bedrijf') {
            this.bedrijf = u;
          } else {
            this.user = u;
          }
          this.isLoaded = true;
        });
      } else if (this.claim.role === 'coordinator') {
        this.API.getUserProfile(this.selectedUserId).subscribe((user) => {
            if (user.voornaam === null) {
              this.bedrijf = user;
            } else {
              this.user = user;
            }
            this.isLoaded = true;
          },
          (error => {
            this.router.navigate(['/**']);
          })
        );
      } else {
        this.router.navigate(['/logout']);
      }
    });
  }
}

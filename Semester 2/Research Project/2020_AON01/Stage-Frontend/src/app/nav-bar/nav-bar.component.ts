import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Claim } from '../classes/claim';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css'],
})
export class NavBarComponent implements OnInit {
  claim: Claim;

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.authService.getUserClaim().subscribe((claim) => {
      this.claim = claim;
    });
  }
}

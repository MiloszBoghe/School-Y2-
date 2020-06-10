import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { Claim } from '../classes/claim';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  claim: Claim;

  constructor(private authService: AuthService, public router: Router) {}

  ngOnInit(): void {
    this.authService.getUserClaim().subscribe((claim) => {
      this.claim = claim;
    });
  }
}

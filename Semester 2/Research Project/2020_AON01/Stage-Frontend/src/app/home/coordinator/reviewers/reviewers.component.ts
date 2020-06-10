import { Component, OnInit } from '@angular/core';
import { Router} from '@angular/router';
import { AuthService } from '@app/services/auth.service';
import { Claim } from '@app/classes/claim';

@Component({
  selector: 'app-reviewers',
  templateUrl: './reviewers.component.html',
  styleUrls: ['./reviewers.component.css'],
})
export class ReviewersComponent implements OnInit {
  claim: Claim;

  constructor(private authService: AuthService, public router: Router) {
  }

  ngOnInit(): void {
    this.authService.getUserClaim().subscribe((claim) => {
      this.claim = claim;
    });
  }
}

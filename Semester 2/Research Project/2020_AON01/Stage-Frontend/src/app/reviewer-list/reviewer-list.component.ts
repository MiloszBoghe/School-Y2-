import { Component, Input, OnInit } from '@angular/core';
import { Reviewer } from '../classes/reviewer';
import { ApiService } from '@app/services/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reviewer-list',
  templateUrl: './reviewer-list.component.html',
  styleUrls: ['./reviewer-list.component.css'],
})
export class ReviewerListComponent implements OnInit {
  @Input() id: number;
  @Input() reviewers: Reviewer[];
  constructor(private service: ApiService, private router: Router) {}

  ngOnInit(): void {
    this.fetchReviewers();
  }

  reviewerProfile(id) {
    this.router.navigate(['/profile', id]);
  }

  fetchReviewers(): void {
    if (!this.reviewers) {
      this.service.getReviewers().subscribe((data) => {
        this.reviewers = data;
      });
    }
  }
}

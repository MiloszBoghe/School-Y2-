import { Component, Input, OnInit } from '@angular/core';
import { Review } from '../classes/review';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../services/api.service';
import { Stagevoorstel } from '../classes/stagevoorstel';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  BeoordelingStatus,
  LeesbareBeoordelingStatus,
} from '../classes/enums/beoordeling-status.enum';
import { ReviewCommentaarStates } from '@app/classes/enums/review-commentaar-states.enum';
import { Claim } from '@app/classes/claim';
import { AuthService } from '@app/services/auth.service';

@Component({
  selector: 'app-review-voorstel',
  templateUrl: './review-voorstel.component.html',
  styleUrls: ['./review-voorstel.component.css'],
})
export class ReviewVoorstelComponent implements OnInit {
  feedbackForm: FormGroup;
  voorstel: Stagevoorstel;
  feedbacks: Review[];
  isLoaded = false;
  isFeedbacksLoaded = false;
  beoordelingStatus = BeoordelingStatus;
  leesbareBeoordelingStatus = LeesbareBeoordelingStatus;
  public states = ReviewCommentaarStates;
  public state = ReviewCommentaarStates.review;
  @Input() claim: Claim;

  constructor(
    private route: ActivatedRoute,
    private API: ApiService,
    private authService: AuthService
  ) {
    if (!this.claim) {
      this.authService.getUserClaim().subscribe((claim) => {
        this.claim = claim;
      });
    }
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const id = +params.id;
      this.API.getStagevoorstel(id).subscribe((data) => {
        this.voorstel = data;
        this.isLoaded = true;

        this.API.getReviewsStagevoorstel(id).subscribe((reviewData) => {
          reviewData.forEach(
            (review) => (review.stagevoorstel = this.voorstel)
          );
          this.feedbacks = reviewData;
          this.isFeedbacksLoaded = true;
        });
      });
    });
    this.feedbackForm = new FormGroup({
      text: new FormControl(null),
      status: new FormControl('0', [Validators.required]),
    });
  }

  onSubmit() {
    if (this.feedbackForm.valid) {
      const feedback = new Review();
      const voorstel = this.voorstel;

      feedback.text = this.feedbackForm.get('text').value;
      feedback.stagevoorstel = this.voorstel;

      this.API.postReview(feedback).subscribe(() => {
        this.API.patchStagevoorstel(voorstel).subscribe(() => {
          window.location.reload();
        });
      });
    }
  }

  switchTabs(tab: number) {
    this.state = tab as ReviewCommentaarStates;
  }
}

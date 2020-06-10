import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../services/api.service';
import { Stagevoorstel } from '../classes/stagevoorstel';
import { Review } from '../classes/review';
import { Comment } from '../classes/comment';
import {
  BeoordelingStatus,
  LeesbareBeoordelingStatus,
} from '../classes/enums/beoordeling-status.enum';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Reviewer } from '@app/classes/reviewer';
import { ReviewCommentaarStates } from '@app/classes/enums/review-commentaar-states.enum';
import { Claim } from '@app/classes/claim';
import { AuthService } from '@app/services/auth.service';

@Component({
  selector: 'app-review-feedback',
  templateUrl: './review-feedback.component.html',
  styleUrls: ['./review-feedback.component.css'],
})
export class ReviewFeedbackComponent implements OnInit {
  voorstel: Stagevoorstel;
  feedbacks: Review[];
  @Input() comments: Comment[];
  @Input() claim: Claim;
  coordinatorFeedbackForm: FormGroup;
  isLoaded = false;
  isFeedbacksLoaded = false;
  beoordelingStatus = BeoordelingStatus;
  leesbareBeoordelingStatus = LeesbareBeoordelingStatus;
  reviewersLoaded = false;
  reviewers: Reviewer[];
  reviewersNotAssigned: Reviewer[];
  public states = ReviewCommentaarStates;
  public state = ReviewCommentaarStates.review;

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
    this.API.getReviewers().subscribe((r) => {
      this.reviewers = r;
      this.reviewersLoaded = true;
      if (this.isLoaded) {
        this.reviewersNotAssigned = r.filter(
          (entry) =>
            !this.voorstel.reviewersToegewezen.find(
              (secondEntry) => secondEntry.id === entry.id
            )
        );
      }
    });

    this.route.params.subscribe((params) => {
      const id = +params.id;
      this.API.getStagevoorstel(id).subscribe((data) => {
        this.voorstel = data;
        this.comments = data.comments;
        this.isLoaded = true;
        if (this.reviewersLoaded) {
          this.reviewersNotAssigned = this.reviewers.filter(
            (entry) =>
              !this.voorstel.reviewersToegewezen.find(
                (secondEntry) => secondEntry.id === entry.id
              )
          );
        }
        this.API.getReviewsStagevoorstel(id).subscribe((reviewData) => {
          reviewData.forEach(
            (review) => (review.stagevoorstel = this.voorstel)
          );
          this.feedbacks = reviewData;
          this.isFeedbacksLoaded = true;
        });
        this.coordinatorFeedbackForm = new FormGroup({
          text: new FormControl(null),
          status: new FormControl(this.voorstel.status.toString(), [
            Validators.required,
          ]),
        });
      });
    });
  }

  onSubmit() {
    const feedback = new Review();
    const voorstel = this.voorstel;

    voorstel.status = this.coordinatorFeedbackForm.get('status').value;
    feedback.text = this.coordinatorFeedbackForm.get('text').value;
    feedback.stagevoorstel = this.voorstel;

    this.API.patchStagevoorstel(voorstel).subscribe(() => {
      window.location.reload();
    });
  }

  onSubmitComment() {
    const comment = new Comment();

    // comment.date = new Date(Date.now());
  }

  changeReviewers() {
    const checkedToAdd = document
      .getElementById('reviewerContainer')
      .querySelectorAll('input[type=checkbox]:checked');
    const checkedToRemove = document
      .getElementById('toegewezenReviewers')
      .querySelectorAll('input[type=checkbox]:checked');
    if (checkedToAdd.length === 0 && checkedToRemove.length === 0) {
      alert('Selecteer ten minste één reviewer.');
    } else {
      checkedToAdd.forEach((reviewerli) => {
        this.addReviewer(+reviewerli.id);
        reviewerli.closest('li').remove();
        this.reviewersNotAssigned = this.reviewersNotAssigned.filter(
          (entry) =>
            !this.voorstel.reviewersToegewezen.find(
              (secondEntry) => secondEntry.id === entry.id
            )
        );
      });
      checkedToRemove.forEach((reviewerli) => {
        this.removeReviewer(+reviewerli.id);
        reviewerli.closest('li').remove();
        this.voorstel.reviewersToegewezen = this.voorstel.reviewersToegewezen.filter(
          (entry) =>
            !this.reviewersNotAssigned.find(
              (secondEntry) => secondEntry.id === entry.id
            )
        );
      });
    }
  }

  addReviewer(selectedId: number) {
    if (
      !this.voorstel.reviewersToegewezen.filter(
        (entry) => entry.id === selectedId
      ).length
    ) {
      const reviewer = this.reviewers.find((r) => r.id === selectedId);
      this.voorstel.reviewersToegewezen.push(reviewer);
    }
  }

  removeReviewer(selectedId: number) {
    if (
      this.voorstel.reviewersToegewezen.filter(
        (entry) => entry.id === selectedId
      ).length
    ) {
      const reviewer = this.reviewers.find((r) => r.id === selectedId);
      this.reviewersNotAssigned.push(reviewer);
    }
  }

  saveToewijzingen() {
    this.API.putStagevoorstel(this.voorstel).subscribe(() => {
      alert('Succes!');
    });
  }

  switchTabs(tab: number) {
    this.state = tab as ReviewCommentaarStates;
  }
}

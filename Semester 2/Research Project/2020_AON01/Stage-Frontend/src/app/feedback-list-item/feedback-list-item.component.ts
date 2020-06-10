import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { Review } from '../classes/review';
import { Reviewer } from '../classes/reviewer';
import {
  BeoordelingStatus,
  LeesbareBeoordelingStatus,
} from '../classes/enums/beoordeling-status.enum';
import { ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-feedback-list-item',
  templateUrl: './feedback-list-item.component.html',
  styleUrls: ['./feedback-list-item.component.css'],
})
export class FeedbackListItemComponent implements OnInit, OnChanges {
  reviewForm: FormGroup;
  @Input() feedback: Review;
  @Input() changes: boolean;
  beoordelingStatus = BeoordelingStatus;
  leesbareBeoordelingStatus = LeesbareBeoordelingStatus;
  isFeedback = false;

  constructor(private route: ActivatedRoute, private API: ApiService) {}

  ngOnInit(): void {
    this.isFeedback = this.route.snapshot.url[0].path === 'reviewFeedback';
    if (this.isFeedback) {
      this.reviewForm = new FormGroup({
        status: new FormControl(this.feedback.status.toString()),
      });
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    const feedback = this.feedback;
    if (this.changes) {
      feedback.status = this.reviewForm.get('status').value;
      if (feedback.status === this.feedback.status) {
        this.API.patchReviews(feedback).subscribe(() => {
          this.isFeedback = false;
        });
      }
    }
  }
}

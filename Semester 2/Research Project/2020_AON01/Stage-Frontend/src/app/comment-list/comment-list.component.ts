import { Component, Input, OnInit } from '@angular/core';
import { Comment } from '@app/classes/comment';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Review } from '@app/classes/review';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '@app/services/api.service';
import { Claim } from '@app/classes/claim';
import { Stagevoorstel } from '@app/classes/stagevoorstel';
import { DatePipe } from '@angular/common';
import { AuthService } from '@app/services/auth.service';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.css'],
})
export class CommentListComponent implements OnInit {
  @Input() comments: Comment[];
  @Input() userId: number;
  @Input() stageVoorstelId: number;
  @Input() claim: Claim;
  commentAddForm: FormGroup;
  maxAmountOfComments: number = 20;

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

  get sortedComments(): Comment[] {
    return this.comments
      .sort((a, b) => Date.parse(b.date) - Date.parse(a.date))
      .slice(0, this.maxAmountOfComments);
  }

  ngOnInit(): void {
    // this.comments.sort((a, b) => Date.parse(b.date) - Date.parse(a.date));
    this.commentAddForm = new FormGroup({
      commentText: new FormControl(null, [Validators.required]),
    });
  }

  onSubmit() {
    if (this.commentAddForm.valid) {
      const comment = new Comment();
      comment.userId = this.userId;
      comment.stagevoorstelId = this.stageVoorstelId;
      comment.text = this.commentAddForm.get('commentText').value;

      this.API.postComment(comment).subscribe(() => {
        window.location.reload();
      });
    }
  }
}

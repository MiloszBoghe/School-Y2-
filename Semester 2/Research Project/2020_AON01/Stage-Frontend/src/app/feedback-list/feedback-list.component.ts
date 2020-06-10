import { Component, Input, OnInit } from '@angular/core';
import { Review } from '../classes/review';

@Component({
  selector: 'app-feedback-list',
  templateUrl: './feedback-list.component.html',
  styleUrls: ['./feedback-list.component.css'],
})
export class FeedbackListComponent implements OnInit {
  @Input() feedbacks: Review[];
  changes = false;

  constructor() {}

  ngOnInit(): void {

  }

  runAllPatches() {
    this.changes = true;
  }
}

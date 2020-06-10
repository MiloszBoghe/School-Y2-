import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewVoorstelComponent } from './review-voorstel.component';

describe('ReviewVoorstelComponent', () => {
  let component: ReviewVoorstelComponent;
  let fixture: ComponentFixture<ReviewVoorstelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ReviewVoorstelComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewVoorstelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

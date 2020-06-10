import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StagevoorstellenComponent } from './stagevoorstellen.component';

describe('StagevoorstellenComponent', () => {
  let component: StagevoorstellenComponent;
  let fixture: ComponentFixture<StagevoorstellenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [StagevoorstellenComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StagevoorstellenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

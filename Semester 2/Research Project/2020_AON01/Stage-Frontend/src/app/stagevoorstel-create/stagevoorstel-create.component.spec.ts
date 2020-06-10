import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StagevoorstelCreateComponent } from './stagevoorstel-create.component';

describe('StagevoorstelCreateComponent', () => {
  let component: StagevoorstelCreateComponent;
  let fixture: ComponentFixture<StagevoorstelCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [StagevoorstelCreateComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StagevoorstelCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

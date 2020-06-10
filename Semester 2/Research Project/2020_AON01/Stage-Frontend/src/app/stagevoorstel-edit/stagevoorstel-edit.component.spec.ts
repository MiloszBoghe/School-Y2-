import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StagevoorstelEditComponent } from './stagevoorstel-edit.component';

describe('StagevoorstelEditComponent', () => {
  let component: StagevoorstelEditComponent;
  let fixture: ComponentFixture<StagevoorstelEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [StagevoorstelEditComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StagevoorstelEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

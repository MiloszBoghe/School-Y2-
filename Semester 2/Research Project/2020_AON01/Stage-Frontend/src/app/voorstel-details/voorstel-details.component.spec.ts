import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VoorstelDetailsComponent } from './voorstel-details.component';

describe('VoorstelDetailsComponent', () => {
  let component: VoorstelDetailsComponent;
  let fixture: ComponentFixture<VoorstelDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [VoorstelDetailsComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VoorstelDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VoorstelListComponent } from './voorstel-list.component';

describe('VoorstelListComponent', () => {
  let component: VoorstelListComponent;
  let fixture: ComponentFixture<VoorstelListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [VoorstelListComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VoorstelListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

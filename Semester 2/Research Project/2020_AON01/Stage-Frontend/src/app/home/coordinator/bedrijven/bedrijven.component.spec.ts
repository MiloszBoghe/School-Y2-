import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BedrijvenComponent } from './bedrijven.component';

describe('BedrijvenComponent', () => {
  let component: BedrijvenComponent;
  let fixture: ComponentFixture<BedrijvenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [BedrijvenComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BedrijvenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

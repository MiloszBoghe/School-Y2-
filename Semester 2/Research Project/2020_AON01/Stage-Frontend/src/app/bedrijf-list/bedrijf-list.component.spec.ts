import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BedrijfListComponent } from './bedrijf-list.component';

describe('BedrijfListComponent', () => {
  let component: BedrijfListComponent;
  let fixture: ComponentFixture<BedrijfListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [BedrijfListComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BedrijfListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

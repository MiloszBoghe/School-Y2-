import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BedrijfListItemComponent } from './bedrijf-list-item.component';

describe('BedrijfListItemComponent', () => {
  let component: BedrijfListItemComponent;
  let fixture: ComponentFixture<BedrijfListItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [BedrijfListItemComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BedrijfListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

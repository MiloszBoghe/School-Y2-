import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {AfstudeerrichtingVoorkeuren, ITOmgevingen} from '@app/classes/enums/voorstel-strings.enum';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  @Output() filterEmitter = new EventEmitter<object>();
  @Input() claim;

  afstudeerrichtingFilter: string;
  omgevingFilter: string;

  titelFilter: string;
  bedrijfFilter: string;
  dateFilter: string;
  isBedrijf: boolean;
  Afstudeerrichting = AfstudeerrichtingVoorkeuren;
  ITOmgevingen = ITOmgevingen;

  constructor() {

  }

  ngOnInit() {
    this.titelFilter = '';
    this.dateFilter = '';
    this.bedrijfFilter = '';
    this.afstudeerrichtingFilter = '';
    this.omgevingFilter = '';

    this.isBedrijf = this.claim.role === 'bedrijf';
  }

  onChangeFilter() {
    this.filterEmitter.emit({
      titel: this.titelFilter.toString().toLowerCase(),
      bedrijf: this.bedrijfFilter.toLowerCase(),
      date: this.dateFilter,
      afstudeerrichting: this.afstudeerrichtingFilter,
      ITomgeving: this.omgevingFilter
    });
  }

  keys(input){
    return Object.keys(input);
  }
}

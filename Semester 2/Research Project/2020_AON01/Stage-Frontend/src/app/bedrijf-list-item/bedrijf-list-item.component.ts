import { Component, Input, OnInit } from '@angular/core';
import { Bedrijf } from '@app/classes/bedrijf';

@Component({
  selector: 'app-bedrijf-list-item',
  templateUrl: './bedrijf-list-item.component.html',
  styleUrls: ['./bedrijf-list-item.component.css'],
})
export class BedrijfListItemComponent implements OnInit {
  @Input() bedrijf: Bedrijf;
  constructor() {}

  ngOnInit(): void {}
}

import { Component, Input, OnInit } from '@angular/core';
import { Bedrijf } from '@app/classes/bedrijf';
import { ApiService } from '@app/services/api.service';

@Component({
  selector: 'app-bedrijf-list',
  templateUrl: './bedrijf-list.component.html',
  styleUrls: ['./bedrijf-list.component.css'],
})
export class BedrijfListComponent implements OnInit {
  @Input() id: number;
  @Input() bedrijven: Bedrijf[];

  constructor(private service: ApiService) {}

  fetchBedrijven(): void {
    if (!this.bedrijven) {
      this.service.getBedrijven().subscribe((data) => {
        this.bedrijven = data;
      });
    }
  }

  ngOnInit(): void {
    this.fetchBedrijven();
  }
}

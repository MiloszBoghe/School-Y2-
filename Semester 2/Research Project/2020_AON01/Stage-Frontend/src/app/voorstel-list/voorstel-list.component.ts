import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Stagevoorstel } from '../classes/stagevoorstel';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-voorstel-list',
  templateUrl: './voorstel-list.component.html',
  styleUrls: ['./voorstel-list.component.css'],
})
export class VoorstelListComponent implements OnInit {
  @Input() pathing = '/voorstel';
  @Input() id: number;
  @Input() claim;
  @Input() voorstellen: Stagevoorstel[];

  // EventEmitter for getting every selected voorstel
  @Output() newSelection = new EventEmitter();
  allVoorstellen: Stagevoorstel[];
  selectedVoorstellen = new Array<Stagevoorstel>();

  titelFilter: string;
  bedrijfFilter: string;
  dateFilter: string;
  afstudeerrichtingFilter: string;
  omgevingFilter: string;

  constructor(private apiService: ApiService, public datePipe: DatePipe) {}

  fetchVoorstelList(): void {
    if (!this.voorstellen) {
      if (!this.id) {
        this.apiService.getStagevoorstellen().subscribe((data) => {
          this.voorstellen = data;
          this.allVoorstellen = data;
        });
      } else {
        this.apiService
          .getStagevoorstellenBedrijf(this.id)
          .subscribe((data) => {
            this.voorstellen = data;
            this.allVoorstellen = data;
          });
      }
    }
  }

  ngOnInit(): void {
    this.fetchVoorstelList();
    this.titelFilter = '';
    this.dateFilter = '';
    this.bedrijfFilter = '';
    this.afstudeerrichtingFilter = '';
    this.omgevingFilter = '';
    this.allVoorstellen = this.voorstellen;
  }

  setFilter(o: any) {
    this.titelFilter = o.titel;
    this.bedrijfFilter = o.bedrijf;
    this.dateFilter = o.date;
    this.afstudeerrichtingFilter = o.afstudeerrichting;
    this.omgevingFilter = o.ITomgeving;
    this.runFilter();
    setTimeout(this.checkBoxStateControl.bind(this), 50);
  }

  async runFilter() {
    this.voorstellen = this.allVoorstellen.filter(
      (v) =>
        v.titel.toLowerCase().includes(this.titelFilter.trim()) &&
        v.bedrijfsNaam.toLowerCase().includes(this.bedrijfFilter.trim()) &&
        this.datePipe
          .transform(v.date, 'dd/MM/yyyy')
          .includes(this.dateFilter.trim()) &&
        this.checkDropdowns(v.afstudeerrichtingVoorkeur, this.afstudeerrichtingFilter) &&
        this.checkDropdowns(v.omgeving, this.omgevingFilter)
    );
  }

  checkDropdowns(enumValues, filter)
  {
    return !filter || enumValues.includes(filter);
  }

  checkBoxStateControl() {
    this.selectedVoorstellen.forEach((geselecteerdVoorstel) => {
      if (this.voorstellen.includes(geselecteerdVoorstel)) {
        (document.getElementById(
          geselecteerdVoorstel.id.toString()
        ) as HTMLInputElement).checked = true;
      }
    });
  }

  onClick(voorstel: Stagevoorstel, checkbox: any) {
    if (checkbox.checked) {
      this.selectedVoorstellen.push(voorstel);
    } else {
      this.selectedVoorstellen.splice(
        this.selectedVoorstellen.indexOf(voorstel),
        1
      );
    }
    this.newSelection.emit(this.selectedVoorstellen);
  }
}

import {Component, OnInit} from '@angular/core';
import {AuthService} from '@app/services/auth.service';
import {ApiService} from '@app/services/api.service';
import {Stagevoorstel} from '@app/classes/stagevoorstel';
import {Claim} from '@app/classes/claim';

@Component({
  selector: 'app-bedrijf',
  templateUrl: './bedrijf.component.html',
  styleUrls: ['./bedrijf.component.css'],
})
export class BedrijfComponent implements OnInit {
  id: number;
  claim: Claim;
  isLoaded = false;
  selectedVoorstellen = new Array<Stagevoorstel>();

  constructor(public authService: AuthService, private apiService: ApiService) {
  }

  ngOnInit(): void {
    this.authService.getUserClaim().subscribe((claim) => {
      this.id = claim.id;
      this.claim = claim;
      this.isLoaded = true;
    });
  }

  updateSelectedVoorstellen(event: Array<Stagevoorstel>)
  {
    this.selectedVoorstellen = event;
  }

  deleteStagevoorstel()
  {
    this.selectedVoorstellen.forEach(voorstel => {
      this.apiService.deleteStagevoorstel(voorstel.id).subscribe( () => {

      });
    });
  }
}

import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../services/api.service';
import { Stagevoorstel } from '../classes/stagevoorstel';
import {
  AfstudeerrichtingVoorkeuren,
  ITOmgevingen,
  Verwachtingen,
} from '../classes/enums/voorstel-strings.enum';
import { AuthService } from '@app/services/auth.service';
import { Claim } from '@app/classes/claim';
import { Reviewer } from '@app/classes/reviewer';

@Component({
  selector: 'app-voorstel-details',
  templateUrl: './voorstel-details.component.html',
  styleUrls: ['./voorstel-details.component.css'],
})
export class VoorstelDetailsComponent implements OnInit {
  @Input() voorstel: Stagevoorstel;
  afstudeerrichtingVoorkeuren = AfstudeerrichtingVoorkeuren;
  itOmgevingen = ITOmgevingen;
  verwachtingen = Verwachtingen;
  @Input() claim: Claim;
  reviewers: Reviewer[];

  constructor(
    private route: ActivatedRoute,
    private API: ApiService,
    private authService: AuthService
  ) {
    if (!this.claim) {
      this.authService.getUserClaim().subscribe((claim) => {
        this.claim = claim;
      });
    }
  }

  ngOnInit(): void {
    if (!this.voorstel) {
      this.route.params.subscribe((params) => {
        const id = +params.id;
        this.API.getStagevoorstel(id).subscribe((data) => {
          this.voorstel = data;
        });
      });
    }
  }
}

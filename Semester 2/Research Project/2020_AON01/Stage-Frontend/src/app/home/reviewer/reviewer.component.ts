import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { Reviewer } from '@app/classes/reviewer';
import { ApiService } from '@app/services/api.service';
import { AuthService } from '@app/services/auth.service';
import { Claim } from '@app/classes/claim';
import { Stagevoorstel } from '@app/classes/stagevoorstel';
import { VoorstelListComponent } from '@app/voorstel-list/voorstel-list.component';

@Component({
  selector: 'app-reviewer',
  templateUrl: './reviewer.component.html',
  styleUrls: ['./reviewer.component.css'],
})
export class ReviewerComponent implements OnInit {
  reviewer: Reviewer;
  isLoaded = false;
  claim: Claim;
  toAdd = new Array<Stagevoorstel>();
  toRemove = new Array<Stagevoorstel>();
  toegewezenToShow = new Array<Stagevoorstel>();
  // noinspection JSMismatchedCollectionQueryUpdate
  @ViewChildren(VoorstelListComponent)
  private children: VoorstelListComponent[];

  constructor(private API: ApiService, private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.getUserClaim().subscribe((c) => {
      this.API.getReviewer(c.id).subscribe((data) => {
        this.claim = c;
        this.reviewer = data;
        this.toegewezenToShow = this.reviewer.toegewezenVoorstellen;
        this.reviewer.voorkeurVoorstellen.forEach((voorstel) => {
          const index = this.reviewer.toegewezenVoorstellen.indexOf(
            this.reviewer.toegewezenVoorstellen.find(
              (s) => s.id === voorstel.id
            )
          );
          this.toegewezenToShow.splice(index, 1);
        });
        this.isLoaded = true;
      });
    });
  }

  updateToAdd(event: Array<Stagevoorstel>) {
    this.toAdd = event;
  }

  updateToRemove(event: Array<Stagevoorstel>) {
    this.toRemove = event;
  }

  wijzigVoorkeur() {
    let change = false;
    this.toRemove.forEach((voorstel) => {
      const index = this.reviewer.voorkeurVoorstellen.indexOf(voorstel);
      this.reviewer.voorkeurVoorstellen.splice(index, 1);
      this.toegewezenToShow.push(voorstel);
      change = true;
    });

    this.toAdd.forEach((voorstel) => {
      if (
        !this.reviewer.voorkeurVoorstellen.find((s) => s.id === voorstel.id)
      ) {
        this.reviewer.voorkeurVoorstellen.push(voorstel);
        const index = this.toegewezenToShow.indexOf(voorstel);
        this.toegewezenToShow.splice(index, 1);
        change = true;
      }
    });
    this.toAdd = [];
    this.toRemove = [];
    this.children.forEach((c) => (c.selectedVoorstellen = []));
    if (change) {
      this.API.putReviewer(this.reviewer).subscribe();
    }
  }
}

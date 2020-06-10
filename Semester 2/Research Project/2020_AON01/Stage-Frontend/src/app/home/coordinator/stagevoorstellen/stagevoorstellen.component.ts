import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Claim } from '@app/classes/claim';
import { Stagevoorstel } from '@app/classes/stagevoorstel';
import { ApiService } from '@app/services/api.service';
import { AuthService } from '@app/services/auth.service';

@Component({
  selector: 'app-stagevoorstellen',
  templateUrl: './stagevoorstellen.component.html',
  styleUrls: ['./stagevoorstellen.component.css'],
})
export class StagevoorstellenComponent implements OnInit {
  claim: Claim;
  selectedVoorstellen = new Array<Stagevoorstel>();
  showDeletionSucces = false;
  showDeletionError = false;

  constructor(
    public router: Router,
    private apiService: ApiService,
    private authService: AuthService
  ) {
    this.authService.getUserClaim().subscribe((claim) => {
      this.claim = claim;
    });
  }

  ngOnInit(): void {}

  removeStagevoorstellen() {
    if (this.selectedVoorstellen.length === 1) {
      this.apiService
        .deleteStagevoorstel(this.selectedVoorstellen[0].id)
        .subscribe(
          () => {
            this.showSucces();
          },
          () => {
            this.showError();
          }
        );
    } else {
      const ids = this.selectedVoorstellen.map((s) => s.id).toString();
      this.apiService.deleteStagevoorstellen(ids).subscribe(
        () => {
          this.showSucces();
        },
        () => {
          this.showError();
        }
      );
    }
  }
  updateSelectedVoorstellen(event: Array<Stagevoorstel>) {
    this.selectedVoorstellen = event;
  }

  private showSucces() {
    this.showDeletionSucces = true;
    setTimeout(() => {
      this.showDeletionSucces = false;
      location.reload();
    }, 1500);
  }

  private showError() {
    this.showDeletionError = true;
    setTimeout(() => {
      this.showDeletionError = false;
      location.reload();
    }, 1500);
  }
}

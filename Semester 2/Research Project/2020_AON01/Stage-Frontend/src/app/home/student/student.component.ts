import { Component, OnInit } from '@angular/core';
import { Student } from '@app/classes/student';
import { ApiService } from '@app/services/api.service';
import { AuthService } from '@app/services/auth.service';
import { Stagevoorstel } from '@app/classes/stagevoorstel';
import { Claim } from '@app/classes/claim';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css'],
})
export class StudentComponent implements OnInit {
  student: Student;
  alleVoorstellen: Stagevoorstel[];
  isStudentLoaded = false;
  stageOpdrachtenLoaded = false;
  claim: Claim;

  constructor(
    private apiService: ApiService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.authService.getUserClaim().subscribe((c) => {
      this.apiService.getStudent(c.id).subscribe((student) => {
        this.claim = c;
        this.student = student;
        this.isStudentLoaded = true;

        this.apiService.getStagevoorstellen().subscribe((stagevoorstellen) => {
          this.alleVoorstellen = stagevoorstellen;
          this.student.favorieteOpdrachten.forEach((voorstel) => {
            const index = this.alleVoorstellen.indexOf(
              this.alleVoorstellen.find((s) => s.id === voorstel.id)
            );
            this.alleVoorstellen.splice(index, 1);
          });
          this.stageOpdrachtenLoaded = true;
        });
      });
    });
  }

  addVoorstel(allVoorstellen) {
    const selectedVoorstellen = allVoorstellen.selectedVoorstellen;
    if (selectedVoorstellen.length === 0) {
      return;
    }

    for (let i = 0; i < selectedVoorstellen.length; ++i) {
      for (let j = 0; j < this.alleVoorstellen.length; ++j) {
        if (selectedVoorstellen[i].id === this.alleVoorstellen[j].id) {
          this.student.favorieteOpdrachten.push(selectedVoorstellen[i]);
          this.alleVoorstellen.splice(j, 1);
        }
      }
    }
    allVoorstellen.selectedVoorstellen = [];
    this.apiService.putStudent(this.student).subscribe();
  }

  deleteVoorstel(favorites) {
    const selectedVoorstellen = favorites.selectedVoorstellen;
    if (selectedVoorstellen.length === 0) {
      return;
    }
    for (let i = 0; i < selectedVoorstellen.length; ++i) {
      for (let j = 0; j < this.student.favorieteOpdrachten.length; ++j) {
        if (
          selectedVoorstellen[i].id === this.student.favorieteOpdrachten[j].id
        ) {
          this.alleVoorstellen.push(selectedVoorstellen[i]);
          this.student.favorieteOpdrachten.splice(j, 1);
        }
      }
    }
    favorites.selectedVoorstellen = [];
    this.apiService.putStudent(this.student).subscribe();
  }
}

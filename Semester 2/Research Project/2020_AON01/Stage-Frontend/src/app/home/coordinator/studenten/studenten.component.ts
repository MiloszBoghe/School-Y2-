import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {ApiService} from '@app/services/api.service';
import {Student} from '@app/classes/student';

@Component({
  selector: 'app-studenten',
  templateUrl: './studenten.component.html',
  styleUrls: ['./studenten.component.css'],
})
export class StudentenComponent implements OnInit {
  studenten: Student[];

  constructor(private router: Router, private apiService: ApiService) {
    this.fetchStudents();
  }

  ngOnInit(): void {}

  fetchStudents() {
    this.apiService.getStudenten().subscribe(data => {
        this.studenten = data;
      },
      (error => {
        console.log(error);
      }));
  }


  studentProfile(id) {
    this.router.navigate(['/profile', id]);
  }
}

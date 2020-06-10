import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '@app/services/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-request-reset',
  templateUrl: './request-reset.component.html',
  styleUrls: ['./request-reset.component.css'],
})
export class RequestResetComponent implements OnInit {
  eMailForm: FormGroup;
  isSent: boolean;

  constructor(private apiService: ApiService, private router: Router) {}

  ngOnInit(): void {
    this.eMailForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
    });
    this.isSent = false;
  }

  requestReset() {
    this.apiService
      .postPasswordResetToken(this.eMailForm.value)
      .subscribe(() => {
        this.isSent = true;
      });
    setTimeout(() => {
      this.router.navigate(['/login']);
    }, 5000);
  }
}

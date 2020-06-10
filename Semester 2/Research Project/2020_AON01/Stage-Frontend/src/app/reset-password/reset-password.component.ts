import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { requirePasswordsToBeTheSameValidator } from '@app/services/validators/same-password-validator';
import { ApiService } from '@app/services/api.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css'],
})
export class ResetPasswordComponent implements OnInit {
  passwordResetFormGroup: FormGroup;
  token = '';
  tokenValid = false;
  passwordModified = false;

  constructor(
    private activatedRoute: ActivatedRoute,
    private apiService: ApiService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.token = this.activatedRoute.snapshot.queryParamMap.get('token');

    this.apiService.getTokenValid(this.token).subscribe((data) => {
      this.tokenValid = data as boolean;
    });

    this.passwordResetFormGroup = new FormGroup(
      {
        NewPassWord: new FormControl('', [
          Validators.required,
          Validators.minLength(6),
        ]),
        NewPassWordConfirm: new FormControl('', [Validators.required]),
      },
      [
        requirePasswordsToBeTheSameValidator(
          'NewPassWord',
          'NewPassWordConfirm'
        ),
      ]
    );
  }

  resetPassword() {
    this.apiService
      .patchPasswordReset(this.token, this.passwordResetFormGroup.value)
      .subscribe(() => {
        this.passwordModified = true;
      });
    setTimeout(() => {
      this.router.navigate(['/login']);
    }, 5000);
  }
}

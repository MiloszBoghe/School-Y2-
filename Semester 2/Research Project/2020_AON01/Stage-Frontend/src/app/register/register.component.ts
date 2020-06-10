import {Component, OnInit} from '@angular/core';
import {RegisterStates} from '@app/classes/enums/register-states.enum';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {requirePasswordsToBeTheSameValidator} from '@app/services/validators/same-password-validator';
import {requireIsNumberValidator} from '@app/services/validators/is-number-validator';
import {requireIsPostalCodeValidator} from '@app/services/validators/postal-code-validator';
import {requireIsTelephoneNumberValidator} from '@app/services/validators/is-telephone-number-validator';
import {Bedrijf} from '@app/classes/bedrijf';
import {Student} from '@app/classes/student';
import {Reviewer} from '@app/classes/reviewer';
import {ApiService} from '@app/services/api.service';
import {Contactpersoon} from '@app/classes/contactpersoon';
import {Bedrijfspromotor} from '@app/classes/bedrijfspromotor';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  public states = RegisterStates;
  public state = RegisterStates.student;

  errorMessages: Array<string>;

  studentForm = new FormGroup({wachtwoordGroup: new FormGroup({})});
  reviewerForm: FormGroup;
  bedrijfForm: FormGroup;

  constructor(private API: ApiService, private router: Router) {
  }

  ngOnInit(): void {
    this.studentForm = new FormGroup({
      voornaam: new FormControl(null, [
        Validators.required,
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      achternaam: new FormControl(null, [
        Validators.required,
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      wachtwoordGroup: new FormGroup(
        {
          wachtwoord: new FormControl(null, [
            Validators.required,
            Validators.minLength(6),
          ]),
          wachtwoordBevestiging: new FormControl(null, [Validators.required]),
        },
        [
          requirePasswordsToBeTheSameValidator(
            'wachtwoord',
            'wachtwoordBevestiging'
          ),
        ]
      ),
    });

    this.reviewerForm = new FormGroup({
      voornaam: new FormControl(null, [
        Validators.required,
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      achternaam: new FormControl(null, [
        Validators.required,
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      email: new FormControl(null, [
        Validators.required,
        Validators.email,
        Validators.pattern('^.*[@]pxl\\.be$'),
      ]),
      wachtwoordGroup: new FormGroup(
        {
          wachtwoord: new FormControl(null, [
            Validators.required,
            Validators.minLength(6),
          ]),
          wachtwoordBevestiging: new FormControl(null, [Validators.required]),
        },
        [
          requirePasswordsToBeTheSameValidator(
            'wachtwoord',
            'wachtwoordBevestiging'
          ),
        ]
      ),
    });

    this.bedrijfForm = new FormGroup({
      naamBedrijf: new FormControl(null, [Validators.required]),
      aantalMedewerkers: new FormControl(null, [
        Validators.required,
        requireIsNumberValidator(),
      ]),
      aantalITMedewerkers: new FormControl(null, [
        Validators.required,
        requireIsNumberValidator(),
      ]),
      aantalBegeleiders: new FormControl(null, [
        Validators.required,
        requireIsNumberValidator(),
      ]),
      adresBedrijf: new FormControl(null, [Validators.required]),
      gemeente: new FormControl(null, [
        Validators.required,
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      postcode: new FormControl(null, [
        Validators.required,
        requireIsPostalCodeValidator(),
      ]),

      titelContactPersoon: new FormControl(null, [Validators.required]),
      voornaamContactPersoon: new FormControl(null, [
        Validators.required,
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      achternaamContactPersoon: new FormControl(null, [
        Validators.required,
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      emailContactPersoon: new FormControl(null, [Validators.required, Validators.email]),
      telefoonnummerContactPersoon: new FormControl(null, [
        Validators.required,
        requireIsTelephoneNumberValidator(),
      ]),

      wachtwoordGroup: new FormGroup(
        {
          wachtwoord: new FormControl(null, [
            Validators.required,
            Validators.minLength(6)
          ]),
          wachtwoordBevestiging: new FormControl(null, [Validators.required]),
        },
        [
          requirePasswordsToBeTheSameValidator(
            'wachtwoord',
            'wachtwoordBevestiging'
          ),
        ]
      ),

      titelBedrijfspromotor: new FormControl(null, [Validators.required]),
      voornaamBedrijfspromotor: new FormControl(null, [
        Validators.required,
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      achternaamBedrijfspromotor: new FormControl(null, [
        Validators.required,
        Validators.pattern('^[A-Za-z ]+$'),
      ]),
      emailBedrijfspromotor: new FormControl(null, [Validators.required, Validators.email]),
      telefoonnummerBedrijfspromotor: new FormControl(null, [
        Validators.required,
        requireIsTelephoneNumberValidator(),
      ]),
    });
  }

  switchTabs(tab: number) {
    this.state = tab as RegisterStates;
  }

  registerBedrijf(bedrijfForm: FormGroup) {
    if (bedrijfForm.valid) {
      const bedrijf = new Bedrijf();
      const contactpersoon = new Contactpersoon();
      const bedrijfspromotor = new Bedrijfspromotor();

      bedrijf.naam = bedrijfForm.get('naamBedrijf').value;
      bedrijf.aantalMedewerkers = +bedrijfForm.get('aantalMedewerkers').value;
      bedrijf.aantalITMedewerkers = +bedrijfForm.get('aantalITMedewerkers')
        .value;
      bedrijf.aantalBegeleidendeMedewerkers = +bedrijfForm.get(
        'aantalBegeleiders'
      ).value;
      bedrijf.adres = bedrijfForm.get('adresBedrijf').value;
      bedrijf.gemeente = bedrijfForm.get('gemeente').value;
      bedrijf.postcode = bedrijfForm.get('postcode').value;
      contactpersoon.titel = bedrijfForm.get('titelC').value;
      contactpersoon.voornaam = bedrijfForm.get('voornaamC').value;
      contactpersoon.naam = bedrijfForm.get('achternaamC').value;
      contactpersoon.email = bedrijfForm.get('emailC').value;
      contactpersoon.telefoonnummer = bedrijfForm.get('telefoonnummerC').value;
      bedrijf.contactpersoon = contactpersoon;
      bedrijfspromotor.titel = bedrijfForm.get('titelB').value;
      bedrijfspromotor.voornaam = bedrijfForm.get('voornaamB').value;
      bedrijfspromotor.naam = bedrijfForm.get('achternaamB').value;
      bedrijfspromotor.email = bedrijfForm.get('emailB').value;
      bedrijfspromotor.telefoonnummer = bedrijfForm.get(
        'telefoonnummerB'
      ).value;
      bedrijf.bedrijfspromotor = bedrijfspromotor;
      bedrijf.password = bedrijfForm.controls.wachtwoordGroup.get(
        'wachtwoord'
      ).value;

      this.API.postBedrijf(bedrijf).subscribe(() => {
        this.router.navigate(['/login']);
      });
    }
  }

  registerStudent(studentForm: FormGroup) {
    if (studentForm.valid) {
      const student = new Student();
      student.voornaam = studentForm.get('voornaam').value;
      student.naam = studentForm.get('achternaam').value;
      student.email = studentForm.get('email').value;
      student.password = studentForm.controls.wachtwoordGroup.get(
        'wachtwoord'
      ).value;

      this.API.postUser(student).subscribe(() => {
        this.router.navigate(['/login']);
      }, (error) => {
        this.errorMessages = Object.values(error.error);
      });
    }
  }

  registerReviewer(reviewerForm: FormGroup) {
    if (reviewerForm.valid) {
      const reviewer = new Reviewer();

      reviewer.voornaam = reviewerForm.get('voornaam').value;
      reviewer.naam = reviewerForm.get('achternaam').value;
      reviewer.email = reviewerForm.get('email').value;
      reviewer.password = reviewerForm.controls.wachtwoordGroup.get(
        'wachtwoord'
      ).value;

      this.API.postUser(reviewer).subscribe(() => {
        this.router.navigate(['/login']);
      });
    }
  }
}

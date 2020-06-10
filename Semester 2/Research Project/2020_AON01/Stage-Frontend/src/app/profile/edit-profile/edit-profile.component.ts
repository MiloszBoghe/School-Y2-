import { Component, OnInit } from '@angular/core';
import { Claim } from '@app/classes/claim';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '@app/services/api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Bedrijf } from '@app/classes/bedrijf';
import { User } from '@app/classes/user';
import { requireIsNumberValidator } from '@app/services/validators/is-number-validator';
import { requireIsPostalCodeValidator } from '@app/services/validators/postal-code-validator';
import { requireIsTelephoneNumberValidator } from '@app/services/validators/is-telephone-number-validator';
import { AuthService } from '@app/services/auth.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css'],
})
export class EditProfileComponent implements OnInit {
  claim: Claim = null;
  userProfile: FormGroup;
  bedrijfProfile: FormGroup;
  user: User = null;
  bedrijf: Bedrijf = null;
  isLoaded = false;
  selectedUserId: number;

  constructor(
    private API: ApiService,
    private router: Router,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.selectedUserId = params.id;
    });
    this.authService.getUserClaim().subscribe((claim) => {
      this.claim = claim;
      this.API.getUserProfile(this.selectedUserId).subscribe((u) => {
        if (!u.isBedrijf) {
          this.userProfile = new FormGroup({
            voornaam: new FormControl(u.voornaam, [
              Validators.required,
              Validators.pattern('^[A-Za-z]+$'),
            ]),
            naam: new FormControl(u.naam, [
              Validators.required,
              Validators.pattern('^[A-Za-z]+$'),
            ]),
            email: new FormControl(u.email, [
              Validators.required,
              Validators.email,
            ]),
          });
          this.user = u;
        } else {
          this.bedrijfProfile = new FormGroup({
            naamBedrijf: new FormControl(u.naam, [Validators.required]),
            adresBedrijf: new FormControl(u.adres, [Validators.required]),
            gemeente: new FormControl(u.gemeente, [
              Validators.required,
              Validators.pattern('^[A-Za-z -]+$'),
            ]),
            postcode: new FormControl(u.postcode, [
              Validators.required,
              requireIsPostalCodeValidator(),
            ]),
            aantalMedewerkers: new FormControl(u.aantalMedewerkers.toString(), [
              Validators.required,
              requireIsNumberValidator(),
            ]),
            aantalITMedewerkers: new FormControl(
              u.aantalITMedewerkers.toString(),
              [Validators.required, requireIsNumberValidator()]
            ),
            aantalBegeleiders: new FormControl(
              u.aantalBegeleidendeMedewerkers.toString(),
              [Validators.required, requireIsNumberValidator()]
            ),

            titelContactpersoon: new FormControl(u.contactpersoon.titel, [
              Validators.required,
            ]),
            voornaamContactpersoon: new FormControl(u.contactpersoon.voornaam, [
              Validators.required,
              Validators.pattern('^[A-Za-z ]+$'),
            ]),
            achternaamContactpersoon: new FormControl(u.contactpersoon.naam, [
              Validators.required,
              Validators.pattern('^[A-Za-z ]+$'),
            ]),
            telefoonnummerContactpersoon: new FormControl(
              u.contactpersoon.telefoonnummer,
              [Validators.required, requireIsTelephoneNumberValidator()]
            ),
            emailContactpersoon: new FormControl(u.contactpersoon.email, [
              Validators.required,
              Validators.email,
            ]),
            titelBedrijfspromotor: new FormControl(u.bedrijfspromotor.titel, [
              Validators.required,
            ]),
            voornaamBedrijfspromotor: new FormControl(
              u.bedrijfspromotor.voornaam,
              [Validators.required, Validators.pattern('^[A-Za-z ]+$')]
            ),
            achternaamBedrijfspromotor: new FormControl(
              u.bedrijfspromotor.naam,
              [Validators.required, Validators.pattern('^[A-Za-z ]+$')]
            ),
            telefoonnummerBedrijfspromotor: new FormControl(
              u.bedrijfspromotor.telefoonnummer,
              [Validators.required, requireIsTelephoneNumberValidator()]
            ),
            emailBedrijfspromotor: new FormControl(u.bedrijfspromotor.email, [
              Validators.required,
              Validators.email,
            ]),
          });

          this.bedrijf = u;
        }
        this.isLoaded = true;
      });
    });
  }

  public saveChanges() {
    const id = !this.user ? this.bedrijf.id : this.user.id;
    const profile = this.getFormValues();
    if (!this.noChanges(profile)) {
      if (this.bedrijf !== null) {
        if (this.bedrijfProfile.valid) {
          this.API.putBedrijfProfile(profile).subscribe(() => {
            this.router.navigate(['/profile', id]);
          });
        } else {
          const errors = this.findInvalidControls(this.bedrijfProfile);
          let errorMessage = '';
          errors.forEach(
            (error) => (errorMessage += 'Wrong value: ' + error + '.\n')
          );
          alert(errorMessage);
        }
      } else {
        if (this.userProfile.valid) {
          this.API.putUserProfile(profile).subscribe(() => {
            this.router.navigate(['/profile', id]);
          });
        } else {
          const errors = this.findInvalidControls(this.bedrijfProfile);
          let errorMessage = '';
          errors.forEach(
            (error) => (errorMessage += 'Wrong value: ' + error + '.\n')
          );
          alert(errorMessage);
        }
      }
    } else {
      this.router.navigate(['/profile', id]);
    }
  }

  private getFormValues(): void {
    let profile;
    if (this.bedrijf) {
      profile = JSON.parse(JSON.stringify(this.bedrijf));
      profile.naam = this.bedrijfProfile.get('naamBedrijf').value;
      profile.adres = this.bedrijfProfile.get('adresBedrijf').value;
      profile.gemeente = this.bedrijfProfile.get('gemeente').value;
      profile.postcode = this.bedrijfProfile.get('postcode').value;
      profile.aantalMedewerkers = +this.bedrijfProfile.get('aantalMedewerkers')
        .value;
      profile.aantalITMedewerkers = +this.bedrijfProfile.get(
        'aantalITMedewerkers'
      ).value;
      profile.aantalBegeleidendeMedewerkers = +this.bedrijfProfile.get(
        'aantalBegeleiders'
      ).value;
      profile.contactpersoon.titel = this.bedrijfProfile.get(
        'titelContactpersoon'
      ).value;
      profile.contactpersoon.voornaam = this.bedrijfProfile.get(
        'voornaamContactpersoon'
      ).value;
      profile.contactpersoon.naam = this.bedrijfProfile.get(
        'achternaamContactpersoon'
      ).value;
      profile.contactpersoon.telefoonnummer = this.bedrijfProfile.get(
        'telefoonnummerContactpersoon'
      ).value;
      profile.contactpersoon.email = this.bedrijfProfile.get(
        'emailContactpersoon'
      ).value;
      profile.email = profile.contactpersoon.email;
      profile.bedrijfspromotor.titel = this.bedrijfProfile.get(
        'titelBedrijfspromotor'
      ).value;
      profile.bedrijfspromotor.voornaam = this.bedrijfProfile.get(
        'voornaamBedrijfspromotor'
      ).value;
      profile.bedrijfspromotor.naam = this.bedrijfProfile.get(
        'achternaamBedrijfspromotor'
      ).value;
      profile.bedrijfspromotor.telefoonnummer = this.bedrijfProfile.get(
        'telefoonnummerBedrijfspromotor'
      ).value;
      profile.bedrijfspromotor.email = this.bedrijfProfile.get(
        'emailBedrijfspromotor'
      ).value;
    } else {
      profile = JSON.parse(JSON.stringify(this.user));
      profile.voornaam = this.userProfile.get('voornaam').value;
      profile.naam = this.userProfile.get('naam').value;
      profile.email = this.userProfile.get('email').value;
      profile.userName = profile.email;
    }
    return profile;
  }

  private noChanges(userProfile: any) {
    return this.bedrijf
      ? userProfile.naam === this.bedrijf.naam &&
          userProfile.adres === this.bedrijf.adres &&
          userProfile.gemeente === this.bedrijf.gemeente &&
          userProfile.postcode === this.bedrijf.postcode &&
          userProfile.aantalMedewerkers === this.bedrijf.aantalMedewerkers &&
          userProfile.aantalITMedewerkers ===
            this.bedrijf.aantalITMedewerkers &&
          userProfile.aantalBegeleidendeMedewerkers ===
            this.bedrijf.aantalBegeleidendeMedewerkers &&
          userProfile.contactpersoon.titel ===
            this.bedrijf.contactpersoon.titel &&
          userProfile.contactpersoon.voornaam ===
            this.bedrijf.contactpersoon.voornaam &&
          userProfile.contactpersoon.naam ===
            this.bedrijf.contactpersoon.naam &&
          userProfile.contactpersoon.telefoonnummer ===
            this.bedrijf.contactpersoon.telefoonnummer &&
          userProfile.contactpersoon.email ===
            this.bedrijf.contactpersoon.email &&
          userProfile.bedrijfspromotor.titel ===
            this.bedrijf.bedrijfspromotor.titel &&
          userProfile.bedrijfspromotor.voornaam ===
            this.bedrijf.bedrijfspromotor.voornaam &&
          userProfile.bedrijfspromotor.naam ===
            this.bedrijf.bedrijfspromotor.naam &&
          userProfile.bedrijfspromotor.telefoonnummer ===
            this.bedrijf.bedrijfspromotor.telefoonnummer &&
          userProfile.bedrijfspromotor.email ===
            this.bedrijf.bedrijfspromotor.email
      : userProfile.voornaam === this.user.voornaam &&
          userProfile.naam === this.user.naam &&
          userProfile.email === this.user.email;
  }

  private findInvalidControls(form: FormGroup) {
    const invalid = [];
    const controls = form.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
      }
    }
    return invalid;
  }
}

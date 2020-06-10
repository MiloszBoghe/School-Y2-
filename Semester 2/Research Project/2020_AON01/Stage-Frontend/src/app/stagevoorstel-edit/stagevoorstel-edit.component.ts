import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  AfstudeerrichtingVoorkeuren,
  ITOmgevingen,
  Verwachtingen,
} from '../classes/enums/voorstel-strings.enum';
import { ApiService } from '../services/api.service';
import { Stagevoorstel } from '../classes/stagevoorstel';
import { ActivatedRoute, Router } from '@angular/router';
import { requireCheckboxesToBeCheckedValidator } from '@app/services/validators/checkbox-validator';
import { Claim } from '@app/classes/claim';
import { AuthService } from '@app/services/auth.service';

@Component({
  selector: 'app-stagevoorstel-edit',
  templateUrl: './stagevoorstel-edit.component.html',
  styleUrls: ['./stagevoorstel-edit.component.css'],
})
export class StagevoorstelEditComponent implements OnInit {
  voorstelForm: FormGroup;
  voorkeurEnums;
  omgevingEnums;
  verwachtingEnums;
  afstudeerrichtingVoorkeuren = AfstudeerrichtingVoorkeuren;
  itOmgevingen = ITOmgevingen;
  verwachtingen = Verwachtingen;
  voorstel: Stagevoorstel;
  isLoaded = false;
  @Input() claim: Claim;

  constructor(
    private API: ApiService,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {
    if (!this.claim) {
      this.authService.getUserClaim().subscribe((claim) => {
        this.claim = claim;
      });
    }
  }

  ngOnInit(): void {
    this.voorkeurEnums = Object.keys(AfstudeerrichtingVoorkeuren);
    this.omgevingEnums = Object.keys(ITOmgevingen);
    this.verwachtingEnums = Object.keys(Verwachtingen);
    this.API.getStagevoorstel(this.route.snapshot.params.id).subscribe(
      (data) => {
        this.isLoaded = true;
        this.voorstel = data;
        this.voorstelForm = new FormGroup({
          titel: new FormControl(data.titel, [Validators.required]),
          afstudeerrichtingVoorkeur: new FormGroup(
            {
              applicatieOntwikkeling: new FormControl(
                data.afstudeerrichtingVoorkeur.includes(
                  'applicatieOntwikkeling'
                )
              ),
              netwerkBeheer: new FormControl(
                data.afstudeerrichtingVoorkeur.includes('netwerkBeheer')
              ),
              softwareManagement: new FormControl(
                data.afstudeerrichtingVoorkeur.includes('softwareManagement')
              ),
              AI: new FormControl(
                data.afstudeerrichtingVoorkeur.includes('AI')
              ),
            },
            requireCheckboxesToBeCheckedValidator()
          ),
          opdrachtOmschrijving: new FormControl(data.opdrachtOmschrijving, [
            Validators.required,
          ]),
          omgeving: new FormGroup(
            {
              java: new FormControl(data.omgeving.includes('java')),
              dotNet: new FormControl(data.omgeving.includes('dotNet')),
              web: new FormControl(data.omgeving.includes('web')),
              mobile: new FormControl(data.omgeving.includes('mobile')),
              systemenNetwerken: new FormControl(
                data.omgeving.includes('systemenNetwerken')
              ),
              softwareTesting: new FormControl(
                data.omgeving.includes('softwareTesting')
              ),
              anders: new FormControl(data.omgeving.includes('anders')),
            },
            requireCheckboxesToBeCheckedValidator()
          ),
          omgevingOmschrijving: new FormControl(data.omgevingOmschrijving, [
            Validators.required,
          ]),
          randvoorwaarden: new FormControl(data.randvoorwaarden),
          onderzoeksthema: new FormControl(data.onderzoeksthema, [
            Validators.required,
          ]),
          verwachtingen: new FormGroup(
            {
              sollicitatiegesprek: new FormControl(
                data.verwachtingen.includes('sollicitatiegesprek')
              ),
              CV: new FormControl(data.verwachtingen.includes('CV')),
              vergoeding: new FormControl(
                data.verwachtingen.includes('vergoeding')
              ),
              geen: new FormControl(data.verwachtingen.includes('geen')),
            },
            requireCheckboxesToBeCheckedValidator()
          ),
          aantalGewensteStagiairs: new FormControl(
            data.aantalGewensteStagiairs
          ),
          gereserveerdeStudenten: new FormControl(data.gereserveerdeStudenten),
          bemerkingen: new FormControl(data.bemerkingen),
          periodes: new FormGroup(
            {
              semester1: new FormControl(
                data.periode === 1 || data.periode === 3
              ),
              semester2: new FormControl(
                data.periode === 2 || data.periode === 1
              ),
            },
            requireCheckboxesToBeCheckedValidator()
          ),
        });
      }
    );
  }

  onSubmit() {
    if (this.voorstelForm.valid) {
      this.putVoorsteel();
    }
  }

  putVoorsteel() {
    const voorstel = this.voorstel;

    voorstel.titel = this.voorstelForm.get('titel').value;
    voorstel.opdrachtOmschrijving = this.voorstelForm.get(
      'opdrachtOmschrijving'
    ).value;
    voorstel.omgevingOmschrijving = this.voorstelForm.get(
      'omgevingOmschrijving'
    ).value;
    voorstel.randvoorwaarden = this.voorstelForm.get('randvoorwaarden').value;
    voorstel.onderzoeksthema = this.voorstelForm.get('onderzoeksthema').value;
    voorstel.aantalGewensteStagiairs = this.voorstelForm.get(
      'aantalGewensteStagiairs'
    ).value;
    voorstel.gereserveerdeStudenten = this.voorstelForm.get(
      'gereserveerdeStudenten'
    ).value;
    voorstel.bemerkingen = this.voorstelForm.get('bemerkingen').value;

    // Stage periode extracting
    const periodes = this.voorstelForm.get('periodes');
    if (periodes.get('semester1').value) {
      if (periodes.get('semester2').value) {
        voorstel.periode = 3;
      } else {
        voorstel.periode = 1;
      }
    } else {
      voorstel.periode = 2;
    }

    // afstudeerrichtingVoorkeur extracting
    const voorkeur = this.voorstelForm.value.afstudeerrichtingVoorkeur;
    voorstel.afstudeerrichtingVoorkeur = [];
    for (const voorkeurKey in voorkeur) {
      if (voorkeur[voorkeurKey]) {
        voorstel.afstudeerrichtingVoorkeur.push(voorkeurKey);
      }
    }

    // omgeving extracting
    const omgeving = this.voorstelForm.value.omgeving;
    voorstel.omgeving = [];
    for (const omgevingKey in omgeving) {
      if (omgeving[omgevingKey]) {
        voorstel.omgeving.push(omgevingKey);
      }
    }

    // verwachtingen extracting
    const verwachtingen = this.voorstelForm.value.verwachtingen;
    voorstel.verwachtingen = [];
    for (const verwachtingenKey in verwachtingen) {
      if (verwachtingen[verwachtingenKey]) {
        voorstel.verwachtingen.push(verwachtingenKey);
      }
    }

    this.API.putStagevoorstel(voorstel).subscribe(() => {
      this.router.navigate(['/home']);
    });
  }
}

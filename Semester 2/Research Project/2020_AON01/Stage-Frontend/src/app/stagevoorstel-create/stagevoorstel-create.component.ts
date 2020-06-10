import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../services/api.service';
import { Stagevoorstel } from '../classes/stagevoorstel';
import {
  AfstudeerrichtingVoorkeuren,
  ITOmgevingen,
  Verwachtingen,
} from '../classes/enums/voorstel-strings.enum';
import { requireCheckboxesToBeCheckedValidator } from '@app/services/validators/checkbox-validator';

@Component({
  selector: 'app-stagevoorstel-create',
  templateUrl: './stagevoorstel-create.component.html',
  styleUrls: ['./stagevoorstel-create.component.css'],
})
export class StagevoorstelCreateComponent implements OnInit {
  voorstelForm: FormGroup;
  voorkeurEnums;
  omgevingEnums;
  verwachtingEnums;
  afstudeerrichtingVoorkeuren = AfstudeerrichtingVoorkeuren;
  itOmgevingen = ITOmgevingen;
  verwachtingen = Verwachtingen;

  constructor(private API: ApiService) {}

  ngOnInit(): void {
    this.voorkeurEnums = Object.keys(AfstudeerrichtingVoorkeuren);
    this.omgevingEnums = Object.keys(ITOmgevingen);
    this.verwachtingEnums = Object.keys(Verwachtingen);
    this.voorstelForm = new FormGroup({
      titel: new FormControl(null, [Validators.required]),
      afstudeerrichtingVoorkeur: new FormGroup(
        {
          applicatieOntwikkeling: new FormControl(false),
          netwerkBeheer: new FormControl(false),
          softwareManagement: new FormControl(false),
          AI: new FormControl(false),
        },
        requireCheckboxesToBeCheckedValidator()
      ),
      opdrachtOmschrijving: new FormControl(null, [Validators.required]),
      omgeving: new FormGroup(
        {
          java: new FormControl(false),
          dotNet: new FormControl(false),
          web: new FormControl(false),
          mobile: new FormControl(false),
          systemenNetwerken: new FormControl(false),
          softwareTesting: new FormControl(false),
          anders: new FormControl(false),
        },
        requireCheckboxesToBeCheckedValidator()
      ),
      omgevingOmschrijving: new FormControl(null, [Validators.required]),
      randvoorwaarden: new FormControl(),
      onderzoeksthema: new FormControl(null, [Validators.required]),
      verwachtingen: new FormGroup(
        {
          sollicitatiegesprek: new FormControl(false),
          CV: new FormControl(false),
          vergoeding: new FormControl(false),
          geen: new FormControl(false),
        },
        requireCheckboxesToBeCheckedValidator()
      ),
      aantalGewensteStagiairs: new FormControl('1'),
      gereserveerdeStudenten: new FormControl(),
      bemerkingen: new FormControl(),
      periodes: new FormGroup(
        {
          semester1: new FormControl(false),
          semester2: new FormControl(false),
        },
        requireCheckboxesToBeCheckedValidator()
      ),
    });
  }

  onSubmit() {
    if (this.voorstelForm.valid) {
      this.postVoorsteel();
    }
  }

  postVoorsteel() {
    const values = this.voorstelForm.value;

    const voorstel = new Stagevoorstel();

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
    const voorkeur = values.afstudeerrichtingVoorkeur;
    voorstel.afstudeerrichtingVoorkeur = [];
    for (const voorkeurKey in voorkeur) {
      if (voorkeur[voorkeurKey]) {
        voorstel.afstudeerrichtingVoorkeur.push(voorkeurKey);
      }
    }

    // omgeving extracting
    const omgeving = values.omgeving;
    voorstel.omgeving = [];
    for (const omgevingKey in omgeving) {
      if (omgeving[omgevingKey]) {
        voorstel.omgeving.push(omgevingKey);
      }
    }

    // verwachtingen extracting
    const verwachtingen = values.verwachtingen;
    voorstel.verwachtingen = [];
    for (const verwachtingenKey in verwachtingen) {
      if (verwachtingen[verwachtingenKey]) {
        voorstel.verwachtingen.push(verwachtingenKey);
      }
    }

    this.API.postStagevoorstel(voorstel).subscribe(() => {
      window.location.reload();
    });
  }
}

import {Contactpersoon} from './contactpersoon';
import {Bedrijfspromotor} from './bedrijfspromotor';
import {User} from './user';


export class Bedrijf extends User {
  adres: string;
  postcode: string;
  gemeente: string;
  aantalMedewerkers: number;
  aantalITMedewerkers: number;
  aantalBegeleidendeMedewerkers: number;
  contactpersoon: Contactpersoon;
  bedrijfspromotor: Bedrijfspromotor;
}

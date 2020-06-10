import { BeoordelingStatus } from './enums/beoordeling-status.enum';
import { Review } from './review';
import { Bedrijf } from './bedrijf';
import { ReviewerStagevoorstelFavoriet } from './reviewer-stagevoorstel-favoriet';
import { ReviewerStagevoorstelToegewezen } from './reviewer-stagevoorstel-toegewezen';
import { Contactpersoon } from './contactpersoon';
import { Bedrijfspromotor } from './bedrijfspromotor';
import { StudentStagevoorstelFavoriet } from './student-stagevoorstel-favoriet';
import { StudentStagevoorstelToegewezen } from './student-stagevoorstel-toegewezen';
import { Reviewer } from '@app/classes/reviewer';
import { Student } from '@app/classes/student';
import { Comment } from '@app/classes/comment';

export class Stagevoorstel {
  id: number;
  date: string;
  bedrijf: Bedrijf;
  reviewersFavoriet: Reviewer[];
  reviewersToegewezen: Reviewer[];
  studentenFavorieten: Student[];
  studentToegewezen: Student[];
  titel: string;
  stageStraat: string;
  stageNummer: string;
  stagePostcode: string;
  gemeente: string;
  stageITMedewerkers: number;
  contactpersoon: Contactpersoon;
  bedrijfspromotor: Bedrijfspromotor;
  afstudeerrichtingVoorkeur: string[];
  opdrachtOmschrijving: string;
  omgeving: string[];
  omgevingOmschrijving: string;
  randvoorwaarden: string;
  onderzoeksthema: string;
  verwachtingen: string[];
  aantalGewensteStagiairs: number;
  gereserveerdeStudenten: string;
  bemerkingen: string;
  periode: number;
  status: BeoordelingStatus;
  reviews: Review[];
  comments: Comment[];

  // stagevoorstelModelStudent
  bedrijfsNaam: string;
  studentenFavorietenCount: number;
}

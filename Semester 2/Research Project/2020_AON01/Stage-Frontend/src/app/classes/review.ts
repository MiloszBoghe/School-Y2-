import { Stagevoorstel } from './stagevoorstel';
import { BeoordelingStatus } from './enums/beoordeling-status.enum';
import { Reviewer } from './reviewer';

export class Review {
  id: number;
  date: string; // DateTime;
  text: string;
  status: BeoordelingStatus;
  voorstelStatus: BeoordelingStatus;
  reviewer: Reviewer;
  stagevoorstel: Stagevoorstel;
}

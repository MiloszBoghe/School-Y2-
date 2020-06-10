import { User } from './user';
import { Stagevoorstel } from './stagevoorstel';

export class Student extends User {
  voornaam: string;
  name: string;
  favorieteOpdrachten: Stagevoorstel[];
}


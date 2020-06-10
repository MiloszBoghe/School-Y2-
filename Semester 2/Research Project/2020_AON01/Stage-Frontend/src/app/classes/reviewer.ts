import {User} from './user';
import {Stagevoorstel} from '@app/classes/stagevoorstel';

export class Reviewer extends User {
  toegewezenVoorstellen: Stagevoorstel[];
  voorkeurVoorstellen: Stagevoorstel[];
  // isCoordinator: boolean;
}

import { User } from '@app/classes/user';
import { Stagevoorstel } from '@app/classes/stagevoorstel';

export class Comment {
  user: User;
  userId: number;
  stagevoorstel: Stagevoorstel;
  stagevoorstelId: number;
  date: string;
  text: string;
}

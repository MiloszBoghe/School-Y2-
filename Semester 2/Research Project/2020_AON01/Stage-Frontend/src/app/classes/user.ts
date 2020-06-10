import { Message } from './message';
import { Comment } from './comment';

export class User {
  id: number;
  userName: string;
  email: string;
  password: string;
  messages: Message[];
  comments: Comment[];
  naam: string;
  voornaam: string;
  emailConfirmed: boolean;
  isBedrijf: boolean;
}


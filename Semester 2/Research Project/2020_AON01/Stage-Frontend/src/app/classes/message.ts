import { User } from './user';

export class Message {
  id: number;
  text: string;
  dateTime: Date;
  receiverId: number;
  senderId: number;
  receiverFirstName: string;
  receiverLastName: string;
  senderFirstName: string;
  senderLastName: string;
  emailReceiver: string;
  emailSender: string;
  isReceived: boolean;
}

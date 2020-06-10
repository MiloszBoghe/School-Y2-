import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { User } from '../classes/user';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
})
export class UserListComponent implements OnInit {
  @Input() users: User[];
  checked: boolean;
  @Output() selectedUsers = new Array<User>();

  constructor() {}

  ngOnInit(): void {
    this.checked = false;
  }

  onClick(userClicked: User) {
    this.checked = !this.checked;
    this.calculateSelectedUsers(userClicked);
  }

  calculateSelectedUsers(user) {
    let found = false;
    for (let i = 0; i < this.selectedUsers.length; i++) {
      if (user.id === this.selectedUsers[i].id) {
        this.selectedUsers.splice(i, 1);
        found = true;
        return;
      }
    }
    if (!found) {
      this.selectedUsers.push(user);
    }
  }

  get clearSelectedUsersFunc()
  {
    return this.clearSelectedUsers.bind(this);
  }

  clearSelectedUsers()
  {
    this.selectedUsers = [];
  }

}

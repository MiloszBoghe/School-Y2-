import {Component, OnInit} from '@angular/core';
import {User} from '@app/classes/user';
import {ActivatedRoute} from '@angular/router';
import {ApiService} from '@app/services/api.service';

@Component({
  selector: 'app-deactivate-account',
  templateUrl: './account-management.component.html',
  styleUrls: ['./account-management.component.css'],
})
export class AccountManagementComponent implements OnInit {
  usersActivated: User[];
  usersDeactivated: User[];
  isLoadedActivated = false;
  isLoadedDeactivated = false;

  constructor(private route: ActivatedRoute, private apiService: ApiService) {
  }

  ngOnInit(): void {
    this.apiService.getActivatedUsers().subscribe((data) => {
      this.usersActivated = data;
      this.usersActivated.forEach((user) => (user.emailConfirmed = true));
      this.isLoadedActivated = true;
    });
    this.apiService.getDeactivatedUsers().subscribe((data) => {
      this.usersDeactivated = data;
      this.usersDeactivated.forEach((user) => (user.emailConfirmed = false));
      this.isLoadedDeactivated = true;
    });
  }

  patchUserList(selectedUserList, allUsersList, type) {
    const selectedUsers = selectedUserList.selectedUsers;
    for (let i = 0; i < selectedUsers.length; ++i) {
      for (let j = 0; j < allUsersList.length; ++j) {
        if (selectedUsers[i].id === allUsersList[j].id) {
          const user = allUsersList[j];
          if (type === 'activated') {
            user.emailConfirmed = false;
            this.usersDeactivated.push(user);
            this.usersActivated.splice(j, 1);
          } else {
            user.emailConfirmed = true;
            this.usersActivated.push(user);
            this.usersDeactivated.splice(j, 1);
          }
          this.apiService.patchUser(user.id, user.emailConfirmed).subscribe();
        }
      }
    }
  }


  patchUsers(selectedActivatedUsers, selectedDeactivatedUsers) {
    this.patchUserList(selectedActivatedUsers, this.usersActivated, 'activated');
    this.patchUserList(selectedDeactivatedUsers, this.usersDeactivated, 'deactivated');
    selectedDeactivatedUsers.selectedUsers = [];
    selectedActivatedUsers.selectedUsers = [];
  }
}

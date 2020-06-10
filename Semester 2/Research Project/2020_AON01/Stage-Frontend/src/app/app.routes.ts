import {Routes} from '@angular/router';
import {VoorstelListComponent} from './voorstel-list/voorstel-list.component';
import {VoorstelDetailsComponent} from './voorstel-details/voorstel-details.component';
import {StagevoorstelCreateComponent} from './stagevoorstel-create/stagevoorstel-create.component';
import {LogoutComponent} from './logout/logout.component';
import {LoginComponent} from './login/login.component';
import {StagevoorstelEditComponent} from './stagevoorstel-edit/stagevoorstel-edit.component';
import {ReviewVoorstelComponent} from './review-voorstel/review-voorstel.component';
import {ReviewFeedbackComponent} from './review-feedback/review-feedback.component';
import {HomeComponent} from './home/home.component';
import {ReviewersComponent} from './home/coordinator/reviewers/reviewers.component';
import {BedrijvenComponent} from './home/coordinator/bedrijven/bedrijven.component';
import {StudentenComponent} from './home/coordinator/studenten/studenten.component';
import {AuthGuardService} from './services/auth-guard.service';
import {RoleGuard} from './services/role.guard';
import {PageNotFoundComponent} from './page-not-found/page-not-found.component';
import {RegisterComponent} from './register/register.component';
import {ProfileComponent} from './profile/profile.component';
import {AccountManagementComponent} from '@app/home/coordinator/account-management/account-management.component';
import {EditProfileComponent} from '@app/profile/edit-profile/edit-profile.component';
import {RequestResetComponent} from '@app/request-reset/request-reset.component';
import {ResetPasswordComponent} from '@app/reset-password/reset-password.component';

export const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'reviewers',
    component: ReviewersComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'bedrijven',
    component: BedrijvenComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'studenten',
    component: StudentenComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'deactivateAccount',
    component: AccountManagementComponent,
    canActivate: [AuthGuardService, RoleGuard],
    data: {roles: ['coordinator']},
  },
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {
    path: 'profile/:id',
    component: ProfileComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'edit-profile/:id',
    component: EditProfileComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'createVoorstel',
    component: StagevoorstelCreateComponent,
    canActivate: [AuthGuardService, RoleGuard],
    data: {roles: ['bedrijf']},
  },
  {
    path: 'editVoorstel/:id',
    component: StagevoorstelEditComponent,
    canActivate: [AuthGuardService, RoleGuard],
    data: {roles: ['bedrijf']},
  },
  {
    path: 'voorstel/:id',
    component: VoorstelDetailsComponent,
    canActivate: [AuthGuardService, RoleGuard],
    data: {roles: ['bedrijf', 'student', 'coordinator', 'reviewer']},
  },
  {
    path: 'reviewVoorstel/:id',
    component: ReviewVoorstelComponent,
    canActivate: [AuthGuardService, RoleGuard],
    data: {roles: ['reviewer', 'coordinator']},
  },
  {
    path: 'reviewFeedback/:id',
    component: ReviewFeedbackComponent,
    canActivate: [AuthGuardService, RoleGuard],
    data: {roles: ['coordinator']},
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'logout',
    component: LogoutComponent,
  },
  {
    path: 'requestReset',
    component: RequestResetComponent,
  },
  {
    path: 'resetPassword',
    component: ResetPasswordComponent,
  },
  {
    path: '**',
    component: PageNotFoundComponent,
  },
];

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { VoorstelListComponent } from './voorstel-list/voorstel-list.component';
import { RouterModule } from '@angular/router';
import { routes } from './app.routes';
import { VoorstelDetailsComponent } from './voorstel-details/voorstel-details.component';
import { StagevoorstelCreateComponent } from './stagevoorstel-create/stagevoorstel-create.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LogoutComponent } from './logout/logout.component';
import { LoginComponent } from './login/login.component';
import { StagevoorstelEditComponent } from './stagevoorstel-edit/stagevoorstel-edit.component';
import { FeedbackListComponent } from './feedback-list/feedback-list.component';
import { FeedbackListItemComponent } from './feedback-list-item/feedback-list-item.component';
import { ReviewVoorstelComponent } from './review-voorstel/review-voorstel.component';
import { ReviewFeedbackComponent } from './review-feedback/review-feedback.component';
import { HomeComponent } from './home/home.component';
import { ReviewerComponent } from './home/reviewer/reviewer.component';
import { StudentComponent } from './home/student/student.component';
import { BedrijfComponent } from './home/bedrijf/bedrijf.component';
import { StudentenComponent } from './home/coordinator/studenten/studenten.component';
import { ReviewersComponent } from './home/coordinator/reviewers/reviewers.component';
import { StagevoorstellenComponent } from './home/coordinator/stagevoorstellen/stagevoorstellen.component';
import { BedrijvenComponent } from './home/coordinator/bedrijven/bedrijven.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AuthGuardService } from './services/auth-guard.service';
import { TokenInterceptorService } from './services/token-interceptor.service';
import { RoleGuard } from './services/role.guard';
import { ReviewerListComponent } from './reviewer-list/reviewer-list.component';
import { RegisterComponent } from './register/register.component';
import { BedrijfListItemComponent } from './bedrijf-list-item/bedrijf-list-item.component';
import { BedrijfListComponent } from './bedrijf-list/bedrijf-list.component';
import { ProfileComponent } from './profile/profile.component';
import { JWT_OPTIONS, JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { AccountManagementComponent } from './home/coordinator/account-management/account-management.component';
import { UserListComponent } from './user-list/user-list.component';
import { EditProfileComponent } from './profile/edit-profile/edit-profile.component';
import { DatePipe } from '@angular/common';
import { SearchComponent } from '@app/search/search.component';
import { CommentListComponent } from '@app/comment-list/comment-list.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { RequestResetComponent } from './request-reset/request-reset.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    VoorstelListComponent,
    VoorstelDetailsComponent,
    StagevoorstelCreateComponent,
    LogoutComponent,
    LoginComponent,
    StagevoorstelEditComponent,
    FeedbackListComponent,
    FeedbackListItemComponent,
    ReviewVoorstelComponent,
    ReviewFeedbackComponent,
    HomeComponent,
    ReviewerComponent,
    StudentComponent,
    BedrijfComponent,
    StudentenComponent,
    ReviewersComponent,
    StagevoorstellenComponent,
    BedrijvenComponent,
    PageNotFoundComponent,
    ReviewerListComponent,
    RegisterComponent,
    BedrijfListItemComponent,
    BedrijfListComponent,
    ProfileComponent,
    AccountManagementComponent,
    UserListComponent,
    EditProfileComponent,
    SearchComponent,
    CommentListComponent,
    ResetPasswordComponent,
    RequestResetComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    FormsModule,
    ReactiveFormsModule,
    JwtModule,
  ],
  providers: [
    DatePipe,
    AuthGuardService,
    RoleGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    },
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

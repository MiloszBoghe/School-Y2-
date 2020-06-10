import {Injectable} from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpParams,
} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {Review} from '../classes/review';
import {Stagevoorstel} from '../classes/stagevoorstel';
import {Bedrijf} from '../classes/bedrijf';
import {environment} from '@environments/environment';
import {Student} from '../classes/student';
import {Reviewer} from '../classes/reviewer';
import {Message} from '@app/classes/message';
import {User} from '@app/classes/user';
import {Comment} from '@app/classes/comment';

const BASEAPIURL = environment.apiURL;

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) {
  }

  // Generic CRUD methods
  private getFromAPI<T>(url) {
    return this.http
      .get<T>(BASEAPIURL + url)
      .pipe(catchError(this.handleError));
  }

  private postToAPI(url, data) {
    return this.http
      .post(BASEAPIURL + url, data)
      .pipe(catchError(this.handleError));
  }

  private putToAPI(url, data, headers?: HttpHeaders) {
    return this.http
      .put(BASEAPIURL + url, data, {headers})
      .pipe(catchError(this.handleError));
  }

  private patchToAPI(url, data, headers?: HttpHeaders) {
    return this.http.patch(BASEAPIURL + url, data, {headers}).pipe(
      catchError((err, caught) => {
        console.error(err);
        console.error(caught);
        return throwError('Something bad happened; please try again later.');
      })
    );
  }

  private deleteFromAPI(url) {
    return this.http
      .delete(BASEAPIURL + url)
      .pipe(catchError(this.handleError));
  }

  private deleteMultipleFromAPI(url, options) {
    return this.http
      .delete(BASEAPIURL + url, options)
      .pipe(catchError(this.handleError));
  }

  // Error handling

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned error code ${error.status}, ` +
        `body was: ${error.error}`
      );
    }
    console.log(error);
    return throwError(error);
  }

  // Stagevoorstellen

  getStagevoorstellen(): Observable<Stagevoorstel[]> {
    return this.getFromAPI<Stagevoorstel[]>('Stagevoorstellen/');
  }

  getStagevoorstel(id: number): Observable<Stagevoorstel> {
    return this.getFromAPI<Stagevoorstel>('Stagevoorstellen/' + id);
  }

  getStagevoorstellenBedrijf(id: number): Observable<Stagevoorstel[]> {
    return this.getFromAPI<Stagevoorstel[]>('Stagevoorstellen/Bedrijf/' + id);
  }

  postStagevoorstel(voorstel: Stagevoorstel) {
    return this.postToAPI('Stagevoorstellen/', voorstel);
  }

  putStagevoorstel(voorstel: Stagevoorstel) {
    return this.putToAPI('Stagevoorstellen/' + voorstel.id, voorstel);
  }

  patchStagevoorstel(voorstel: Stagevoorstel) {
    return this.patchToAPI(
      'Stagevoorstellen/Status/' + voorstel.id,
      +voorstel.status
    );
  }

  deleteStagevoorstel(id: number) {
    return this.deleteFromAPI('Stagevoorstellen/' + id);
  }

  deleteStagevoorstellen(ids: string) {
    const httpParams = new HttpParams().set('ids', ids);
    const options = {params: httpParams};
    return this.deleteMultipleFromAPI('Stagevoorstellen/', options);
  }

  // Reviews

  getReviewsStagevoorstel(id: number): Observable<Review[]> {
    return this.getFromAPI<Review[]>('Reviews/Stagevoorstel/' + id);
  }

  postReview(review: Review) {
    return this.postToAPI('Reviews/', review);
  }

  patchReviews(review: Review) {
    return this.patchToAPI('Reviews/Status/' + review.id, +review.status);
  }

  deleteReview(review: Review) {
    return this.deleteFromAPI('Reviews/' + review.id);
  }

  // User

  getUserProfile(id: number): Observable<any> {
    return this.getFromAPI<any>('Users/Profile/' + id);
  }

  putUserProfile(user: any): Observable<any> {
    return this.putToAPI('Users/Profile/' + user.id, user);
  }

  postPasswordResetToken(email: string) {
    return this.postToAPI('Authentication/ResetPasswordRequest/', email);
  }

  getTokenValid(token: string) {
    return this.getFromAPI('Authentication/CheckToken/' + token);
  }

  patchPasswordReset(token: string, newPassword: string) {
    return this.patchToAPI('Authentication/ResetPassword/' + token, newPassword);
  }

  // Bedrijven

  getBedrijven(): Observable<Bedrijf[]> {
    return this.getFromAPI<Bedrijf[]>('Bedrijven/');
  }

  putBedrijfProfile(user: any): Observable<any> {
    return this.putToAPI('Users/Profile/Bedrijf/' + user.id, user);
  }

  postBedrijf(bedrijf: Bedrijf) {
    return this.postToAPI('Authentication/register/bedrijf/', bedrijf);
  }

  // Studenten

  getStudenten(): Observable<Student[]> {
    return this.getFromAPI<Student[]>('Studenten/');
  }

  getStudent(id: number): Observable<Student> {
    return this.getFromAPI<Student>('Studenten/' + id);
  }

  putStudent(student: Student) {
    return this.putToAPI('Studenten/' + student.id, student);
  }

  postUser(user: User) {
    return this.postToAPI('Authentication/register/', user);
  }


  // Reviewers

  getReviewers(): Observable<Reviewer[]> {
    return this.getFromAPI<Reviewer[]>('Reviewers/');
  }

  getReviewer(id: number): Observable<Reviewer> {
    return this.getFromAPI<Reviewer>('Reviewers/' + id);
  }

  putReviewer(reviewer: Reviewer) {
    return this.putToAPI('Reviewers/' + reviewer.id, reviewer);
  }

  // Users
  getActivatedUsers(): Observable<User[]> {
    return this.getFromAPI('Users/Activated');
  }

  getDeactivatedUsers(): Observable<User[]> {
    return this.getFromAPI('Users/Deactivated');
  }

  patchUser(id: number, newEmailConfirmed: boolean) {
    return this.patchToAPI(
      'Users/Activation/' + id,
      newEmailConfirmed,
      new HttpHeaders().append('Content-Type', 'application/json')
    );
  }

   // Comments

  postComment(comment: Comment) {
    // comment.date = new Date(Date.now()).toISOString();
    return this.postToAPI('Comments/', comment);
  }
}

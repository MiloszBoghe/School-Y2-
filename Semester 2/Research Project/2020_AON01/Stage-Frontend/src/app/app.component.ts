import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Stage-Frontend';
  loggedIn: Observable<boolean>;
  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.loggedIn = this.authService.getLoggedInStatus();
  }
}

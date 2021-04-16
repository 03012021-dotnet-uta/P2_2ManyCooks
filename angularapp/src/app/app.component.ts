import { Component, OnInit } from '@angular/core';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { AuthModel } from './auth-model';
import { UserService } from './user-service';
import { BehaviorSubject, Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public loading$ = new BehaviorSubject<boolean>(true);
  isUserAdmin: boolean;
  title = 'InTheKitchen';
  // userList: any = [];
  authModel: AuthModel = new AuthModel();

  constructor(public auth: AuthService, private http: HttpClient, private userService: UserService) { }

  ngOnInit(): void {
    this.auth.authModel$.subscribe(reply => {
      console.log("app component reply");
      console.log(reply);
      console.log("app component reply");
      this.authModel = reply;
    });
    this.auth.isAdmin$.subscribe(reply => {
      console.log("app component is admin?");
      console.log(reply);
      this.isUserAdmin = reply;
    });
    console.log(window.location.pathname);
  }

  showAddRecipe() {
    if (window.location.pathname == "/" || window.location.pathname == "/register")
      if (this.auth.loggedIn)
        return true;

      else return false;
  }
}

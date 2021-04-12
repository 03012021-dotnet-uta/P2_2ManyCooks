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
    //* on page start: check the user status
    // this.checkUser();
    // this.loading$.next(false);
    // this.loading$ = this.auth.getLoading$();
    // this.loading$.next(true);
    // this.auth.initialize();
  }

  // getUsers() {
  //   this.http.get('https://localhost:5001/User', this.httpOptions).subscribe((reply)=> {
  //     this.userList = reply;
  //     console.log(this.userList);
  //   });
  // }

  // checkUser() {
  //   // * check if user is logged in
  //   if (this.auth.loggedIn) {
  //     this.userService.checkIfNewUser().subscribe((reply) => {
  //       // * if logged in, check if new user
  //       console.log(reply);
  //       if (reply == null) {
  //         // * if new user, redirect to reg form
  //         window.location.href = "register"
  //       }
  //     });
  //   }
  // }
}

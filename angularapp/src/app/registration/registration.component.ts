import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AuthModel } from '../auth-model';
import { AuthService } from '../auth.service';
import { UserService } from '../user-service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  usermodel: AuthModel;
  // firstname: string;
  // lastname: string;

  constructor(private service: UserService, private auth: AuthService, private route: Router) {
  }

  ngOnInit(): void {
    this.setUserModel(this.auth.authModel);
    this.auth.authModel$.subscribe(reply => {
      console.log("reply for register model");
      console.log(reply);
      this.setUserModel(this.auth.authModel);
    });
  }

  private setUserModel(reply: AuthModel) {
    if (reply == undefined || reply.username == null || reply.firstName == null) {
      console.log("setting an empty one");
      this.usermodel = new AuthModel();
    }

    else
      this.usermodel = reply;
  }

  sendinfo() {
    console.log("sent model:");
    // this.usermodel.firstName = this.firstname;
    // this.usermodel.lastName = this.lastname;
    console.log(this.usermodel);

    if (!this.goodModel())
      return;
    // return;
    this.service.updateUserModel(this.usermodel).subscribe((reply) => {
      console.log("updating user:");
      console.log(reply);
      this.usermodel = reply;
      this.route.navigate(["/"]);
      this.auth.tryRetrieveUser();
    });
  }

  redirectToHome() {
    window.location.href = window.location.origin;
  }

  logout() {
    this.auth.logout();
  }
  goodModel(): boolean {
    if (this.usermodel.firstName.trim() && this.usermodel.lastName.trim()) {
      console.log("good model");
      return true;
    }
    console.log("bad model");
    return false;
  }
}


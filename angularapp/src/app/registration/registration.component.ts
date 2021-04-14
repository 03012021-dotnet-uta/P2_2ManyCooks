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
    this.auth.authModel$.subscribe(reply => {
      if (reply == null)
        this.usermodel = new AuthModel();
      else
        this.usermodel = reply;
    });
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


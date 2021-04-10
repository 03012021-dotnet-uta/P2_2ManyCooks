import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthModel } from '../auth-model';
import { UserService } from '../user-service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  usermodel: AuthModel = new AuthModel();
  // firstname: string;
  // lastname: string;

  constructor(private service: UserService) { }

  ngOnInit(): void {
  }

  sendinfo() {
    console.log("sent model:");
    // this.usermodel.firstName = this.firstname;
    // this.usermodel.lastName = this.lastname;
    console.log(this.usermodel);
    // return;
    this.service.updateUserModel(this.usermodel).subscribe((reply) => {
      console.log("updating user:");
      console.log(reply);
      this.usermodel = reply;
      this.redirectToHome();
    });
  }

  redirectToHome() {
    window.location.href = window.location.origin;
  }

}

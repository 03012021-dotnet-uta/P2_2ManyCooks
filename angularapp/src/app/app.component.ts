import { Component, OnInit } from '@angular/core';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'angularapp';
  userList: any = [];

  constructor(public auth: AuthService, private http: HttpClient){}

  httpOptions = {
    headers: {"Access-Control-Allow-Origin":"http://localhost:4200/"} 
  };
  ngOnInit(): void {
    
  }

  getUsers() {
    this.http.get('https://localhost:5001/User', this.httpOptions).subscribe((reply)=> {
      this.userList = reply;
      console.log(this.userList);
    });
  }
}

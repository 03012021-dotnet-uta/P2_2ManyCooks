import { Component, OnInit } from '@angular/core';
import { AuthModel } from '../auth-model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  test: AuthModel;
  constructor() { }

  ngOnInit(): void {
  }

}

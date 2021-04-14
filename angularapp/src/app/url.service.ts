import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  dotnetBaseUrl = 'https://inthekitchen.azurewebsites.net/';
  angularBaseUrl = 'https://inthekitchenfront.azurewebsites.net/';
  // dotnetBaseUrl = 'https://localhost:5001/';
  // angularBaseUrl = 'http://localhost:4200/';

  constructor() { }
}

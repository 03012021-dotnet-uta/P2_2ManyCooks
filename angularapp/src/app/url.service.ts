import { environment } from './../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  dotnetBaseUrl = 'https://inthekitchen.azurewebsites.net/';
  angularBaseUrl = 'https://inthekitchenfront.azurewebsites.net/';
  // dotnetBaseUrl = 'https://localhost:5001/';
  // angularBaseUrl = 'http://localhost:4200/';

  constructor() {
    if (environment.production) {
      this.dotnetBaseUrl = 'https://inthekitchen.azurewebsites.net/';
      this.angularBaseUrl = 'https://inthekitchenfront.azurewebsites.net/';
    } else {
      this.dotnetBaseUrl = 'https://localhost:5001/';
      this.angularBaseUrl = 'http://localhost:4200/';
    }
  }
}

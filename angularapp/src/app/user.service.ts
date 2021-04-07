import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

// from https://developer.okta.com/blog/2018/04/26/build-crud-app-aspnetcore-angular
@Injectable()
export class UserService {
  private headers: HttpHeaders;
  private accessPointUrl: string = 'https://localhost:5001/User';

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders(
      {
        'Access-Control-Allow-Origin': 'http://localhost:4200/',
        'Content-Type': 'application/json; charset=utf-8',
      });
  }

  public get() {
    // Get all user data
    return this.http.get(this.accessPointUrl, { headers: this.headers });
  }

  public add(payload) {
    return this.http.post(this.accessPointUrl, payload, { headers: this.headers });
  }

  public remove(payload) {
    return this.http.delete(this.accessPointUrl + '/' + payload.id, { headers: this.headers });
  }

  public update(payload) {
    return this.http.put(this.accessPointUrl + '/' + payload.id, payload, { headers: this.headers });
  }
}
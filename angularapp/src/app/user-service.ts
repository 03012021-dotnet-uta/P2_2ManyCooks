import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthModel } from './auth-model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl: string = "https://localhost:5001/";
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };
  constructor(private http: HttpClient) { }


  updateUserModel(authModel: AuthModel): Observable<AuthModel> {
    console.log('here in service ');
    return this.http.put<AuthModel>(`${this.baseUrl}User`, authModel, this.httpOptions);
  }

  checkIfNewUser(): Promise<AuthModel> {
    return this.http.get<AuthModel>(`${this.baseUrl}User/myinfo`, this.httpOptions).toPromise();
  }

  saveFirstTimeUser(authModel: AuthModel): Observable<AuthModel> {
    return this.http.get<AuthModel>(`${this.baseUrl}User/myinfo`, this.httpOptions);
  }
}

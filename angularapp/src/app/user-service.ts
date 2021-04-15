import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthModel } from './auth-model';
import { UrlService } from './url.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };
  constructor(private http: HttpClient, private urlService: UrlService) {
    this.baseUrl = urlService.dotnetBaseUrl + "User";
  }


  updateUserModel(authModel: AuthModel): Observable<AuthModel> {
    console.log('here in service ');
    return this.http.put<AuthModel>(`${this.baseUrl}`, authModel, this.httpOptions);
  }

  checkIfNewUser(): Promise<AuthModel> {
    return this.http.get<AuthModel>(`${this.baseUrl}/myinfo`, this.httpOptions).toPromise();
  }

  saveFirstTimeUser(authModel: AuthModel): Observable<AuthModel> {
    return this.http.get<AuthModel>(`${this.baseUrl}/myinfo`, this.httpOptions);
  }

  isUserAdmin(): Promise<boolean> {
    return this.http.get<boolean>(`${this.baseUrl}/isadmin`, this.httpOptions).toPromise();
  }

  getAllUsers(): Promise<AuthModel[]> {
    return this.http.get<AuthModel[]>(`${this.baseUrl}`, this.httpOptions).toPromise();
  }

  deleteUser(sub: string): Promise<AuthModel[]> {
    return this.http.delete<AuthModel[]>(`${this.baseUrl}/${sub}`, this.httpOptions).toPromise();
  }
}

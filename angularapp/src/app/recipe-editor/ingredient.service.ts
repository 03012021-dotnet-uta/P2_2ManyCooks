import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { UrlService } from '../url.service';

@Injectable({
  providedIn: 'root'
})
export class IngredientService {

  baseUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };
  constructor(private http: HttpClient, private urlService: UrlService) {
    this.baseUrl = urlService.dotnetBaseUrl + "ingredient";
  }

  getIngredients(): Promise<any[]> {
    console.log("in service");
    return this.http.get<any[]>(`${this.baseUrl}`, this.httpOptions).toPromise();
  }


  handleError<T>(text, result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(text + " " + error.message);
      return of(result);
    };
  }
}

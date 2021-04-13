import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Recipe } from './recipe';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  baseUrl: string = "https://localhost:5001/recipe";
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };
  constructor(private http: HttpClient) { }

  getAllRecipes(): Observable<Recipe[]> {
    console.log("in service");
    return this.http.get<Recipe[]>(`${this.baseUrl}/good`, this.httpOptions).pipe(
      tap((x) => {
        console.log("alo?");
      }), catchError(this.handleError<Recipe[]>("error in all recipes", [])
      ));

  }

  handleError<T>(text, result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(text + " " + error.message);
      return of(result);
    };
  }

  getRecipeId(id: number): Observable<Recipe> {
    return this.http.get<Recipe>(`${this.baseUrl}/good/${id}`, this.httpOptions);
  }
}

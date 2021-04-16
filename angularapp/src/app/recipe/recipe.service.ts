import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { UrlService } from '../url.service';
import { Recipe } from './recipe';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  baseUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };
  constructor(private http: HttpClient, private urlService: UrlService) {
    this.baseUrl = urlService.dotnetBaseUrl + "recipe";
  }

  getAllRecipes(): Observable<Recipe[]> {
    console.log("in service");
    return this.http.get<Recipe[]>(`${this.baseUrl}/good`, this.httpOptions).pipe(
      tap((x) => {
        console.log("alo?");
      }), catchError(this.handleError<Recipe[]>("error in all recipes", [])
      ));

  }

  deleteRecipe(recipeId: number): Promise<Recipe[]> {
    return this.http.delete<Recipe[]>(`${this.baseUrl}/${recipeId}`, this.httpOptions).toPromise();
  }


  saveRecipe(recipe: Recipe): Promise<any> {
    return this.http.put<any>(`${this.baseUrl}`, recipe, this.httpOptions).toPromise();
  }

  saveUserHistory(body: any): Promise<Recipe> {
    return this.http.post<Recipe>(`${this.baseUrl}/history`, body, this.httpOptions).toPromise();
  }

  handleError<T>(text, result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(text + " " + error.message);
      return of(result);
    };
  }

  getRecipeId(id: number): Promise<Recipe> {
    return this.http.get<Recipe>(`${this.baseUrl}/good/${id}`, this.httpOptions).toPromise();
  }

  getCalInfo(ingredientName: string): Promise<any> {
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        "X-Api-Key": "K16xlFLX/ItWCuyXa1GAcQ==Uac4RnAmqfnpx6aI",
        /*"x-rapidapi-host": "calorieninjas.p.rapidapi.com"*/
      })
    };
    // fetch(`https://api.calorieninjas.com/v1/nutrition?query=${ingredientName}`, { headers: {"X-Api-Key": "K16xlFLX/ItWCuyXa1GAcQ==Uac4RnAmqfnpx6aI"}})
    //   .then(reply => console.warn(reply));
    return this.http.get<any>(`${this.urlService.dotnetBaseUrl}api/${ingredientName}`, this.httpOptions).toPromise();
  }
}

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Review } from '../review';
import { UrlService } from '../url.service';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  baseUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };
  constructor(private http: HttpClient, private urlService: UrlService) {
    this.baseUrl = urlService.dotnetBaseUrl + "review";
  }

  getReviewsForRecipe(id: number): Promise<Review[]> {
    console.log("in service");
    return this.http.get<Review[]>(`${this.baseUrl}/recipe/${id}`, this.httpOptions).toPromise();
  }


  deleteReview(reviewId: number): Promise<any> {
    return this.http.delete<Review[]>(`${this.baseUrl}/${reviewId}`, this.httpOptions).toPromise();
  }

  sendReviewGetNewReviews(review: Review): Promise<Review[]> {
    console.log("in service posting");
    return this.http.post<Review[]>(`${this.baseUrl}`, review, this.httpOptions).toPromise();
  }


  handleError<T>(text, result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(text + " " + error.message);
      return of(result);
    };
  }
}

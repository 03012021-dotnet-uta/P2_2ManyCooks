import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { take } from 'rxjs/operators';
import { AuthModel } from '../auth-model';
import { AuthService } from '../auth.service';
import { Review } from '../review';
import { ReviewService } from './review.service';
import { Recipe } from './recipe';
import { RecipeSaver } from './recipe-saver';
import { RecipeEnum, RecipeViewType } from './recipe-view-type';
import { RecipeService } from './recipe.service';
import { UserService } from '../user-service';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.css']
})
export class RecipeComponent implements OnInit, OnDestroy {
  recipe: Recipe;
  reviews: Review[];
  addingReview: boolean;
  addRating: number = 5;
  addDescription: string;
  currentUser: AuthModel;
  fetchedReviews: boolean;
  averageRating: string = "No rating yet";
  preparing: boolean = false;
  unsubSubject = new Subject();

  constructor(private route: ActivatedRoute,
    private recipeService: RecipeService,
    private reviewService: ReviewService,
    private authService: AuthService,
    private userService: UserService
  ) { }
  ngOnDestroy(): void {

  }

  ngOnInit(): void {
    // this.route.params.subscribe((reply) => {
    //   console.log("reply");
    //   let r = JSON.parse(reply["recipe"]);
    //   console.log("after parsing:")
    //   console.log(r);
    //   this.recipe = r;
    //   console.log(this.recipe);
    // });
    // this.recipe = this.recipeSaver.storage;
    // console.log("this.recipe");
    // console.log(this.recipe);
    this.authService.authModel$.subscribe(reply => {
      console.log("review current user");
      console.log(reply);
      console.log("review current user");
      this.currentUser = reply;
      take(1);
    });
    if (this.currentUser == null || this.currentUser == undefined) {
      this.currentUser = this.authService.authModel;
    }
    this.getRecipeInfo(+this.route.snapshot.paramMap.get("id"));
  }

  // goToDetail() {
  //   console.log(this.recipe.recipeId);
  // }

  getRecipeInfo(id: number): void {
    console.log("getting recipe");
    this.recipeService.getRecipeId(id).then((reply) => {
      console.log("recipe reply");
      console.log(reply);
      console.log("recipe reply");
      this.recipe = reply;
      console.log("getting reviews");
      this.reviewService.getReviewsForRecipe(this.recipe.recipeId).then((reply) => {
        console.log("reviews");
        console.log(reply);
        console.log("reviews");
        this.reviews = reply;
        this.calculateAverageRating(reply);
        this.fetchedReviews = true;
        console.log("this.fetchedReviews in recipe");
        console.log(this.fetchedReviews);
      });
    }).catch(err => {
      console.log("error getting recipes");
      console.error(err);
    });
  }


  saveRecipePrepare() {
    if (this.authService.loggedIn && this.currentUser != null && this.currentUser != undefined) {
      this.recipeService.saveUserHistory(JSON.stringify({ recipeId: this.recipe.recipeId, sub: this.currentUser.sub })).then(reply => {
        console.log("save reipce prepare:");
        console.log(reply);
        if (reply != null && reply != undefined && reply.recipeId != null) {
          this.recipe = reply;
        }
      });
    }
  }

  startAddReview() {
    const canReview = this.canReview();
    if (canReview)
      this.addingReview = true;
    else
      console.log("user already reviewed");
  }

  canReview(): boolean {
    if (!this.authService.loggedIn || !this.fetchedReviews) {
      console.log("this.authService.loggedIn");
      console.log(this.authService.loggedIn);
      console.log("this.fetchedReviews in canrevi");
      console.log(this.fetchedReviews);
      return false;
    }
    if (!this.reviews) {
      console.log("!this.reviews");
      console.log(!this.reviews);
      return true;
    }
    let x = !this.reviews.some(review => {
      return review.user.email == this.currentUser.email;
    });
    console.log("x: " + x);
    return x;
  }

  cancelAddReview() {
    this.addingReview = false;
  }

  calculateAverageRating(reviews: Review[]) {
    let total = 0;
    let count = 0;
    reviews.forEach(review => {
      total += review.reviewRating;
      count++;
    });
    const ave = (total / count);
    if (!ave) {
      return;
    }
    this.averageRating = "" + ave;
  }

  submitReview() {
    const canReview = this.canReview();
    if (canReview) {
      this.fetchedReviews = false;
      let newReview = new Review();
      newReview.reviewRating = this.addRating;
      newReview.reviewDescription = this.addDescription;
      newReview.recipeId = this.recipe.recipeId;
      this.cancelAddReview();
      this.reviewService.sendReviewGetNewReviews(newReview).then(reply => {
        this.addingReview = false;
        console.log("new reviews:");
        console.log(reply)
        if (reply != null) {
          this.reviews = reply;
          this.calculateAverageRating(reply);
        } else {
          console.error("couldn't add a review");
        }
        this.fetchedReviews = true;
      }).catch((r) => {
        console.log("err in adding a review");
        console.error(r);
      });
    }
    else
      console.log("user already reviewed");
  }

  startPrepare() {
    this.saveRecipePrepare();
    this.preparing = true;
  }

  stopPreparing() {
    this.preparing = false;
    window.scrollTo(0, 0);
    // element.scrollIntoView();
  }
}

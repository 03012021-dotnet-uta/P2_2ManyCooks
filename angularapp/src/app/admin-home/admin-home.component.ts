import { Component, OnInit } from '@angular/core';
import { AuthModel } from '../auth-model';
import { AuthService } from '../auth.service';
import { Recipe } from '../recipe/recipe';
import { RecipeService } from '../recipe/recipe.service';
import { ReviewService } from '../recipe/review.service';
import { Review } from '../review';
import { UserService } from '../user-service';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css']
})
export class AdminHomeComponent implements OnInit {
  userList: AuthModel[];
  recipeList: Recipe[];

  constructor(
    private userService: UserService,
    private auth: AuthService,
    private recipeService: RecipeService,
    private reviewService: ReviewService
  ) { }

  ngOnInit(): void {
    this.auth.isAdmin$.subscribe(r => {
      if (r) {
        this.tryGetUsers();
        if (!this.recipeList)
          this.getRecipes();
      }
    });
    this.tryGetUsers();
    if (!this.recipeList)
      this.getRecipes();
  }

  deleteUser(model: AuthModel) {
    this.userService.deleteUser(model.sub).then(reply => {
      if (this.isReplyNull(reply)) {
        console.log("error");
      } else {
        this.userList = reply;
      }
    }).catch(err => {
      console.log("error in deleteuser");
      console.error(err);
    });
  }

  deleteRecipe(recipe: Recipe) {
    this.recipeService.deleteRecipe(recipe.recipeId).then(reply => {
      if (this.isReplyNull(reply)) {
        console.log("error");
      } else {
        this.recipeList = reply;
      }
    }).catch(err => {
      console.log("error in delete recipe");
      console.error(err);
    });
  }

  deleteReview(review: Review) {
    this.reviewService.deleteReview(review.reviewId).then(reply => {
      if (this.isReplyNull(reply)) {
        console.log("error");
      } else {
        this.getRecipes();
      }
    }).catch(err => {
      console.log("error in delete review");
      console.error(err);
    });
  }

  isReplyNull(reply: any): boolean {
    return reply == null || reply == undefined;
  }

  private tryGetUsers() {
    if (!this.userList)
      this.userService.getAllUsers().then(reply => {
        console.log("user list:");
        console.log(reply);
        this.userList = reply;
      }).catch(err => {
        console.log("error in admin user list");
        console.error(err);
      });
  }

  // getUser(review: Review, userId: number) {
  //   for (let index = 0; index < this.userList.length; index++) {
  //     const user = this.userList[index];
  //     if (review.userId==user.)
  //   }
  // }

  getRecipes() {
    this.recipeService.getAllRecipes().toPromise().then(reply => {
      console.log("admin recipes")
      console.log(reply);
      this.recipeList = reply;
    }).catch(err => {
      console.log("error in admin recipe list");
      console.error(err);
    });
    // }
  }
}

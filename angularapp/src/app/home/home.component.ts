import { Component, OnInit } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { AuthModel } from '../auth-model';
import { Recipe } from '../recipe/recipe';
import { RecipeSaver } from '../recipe/recipe-saver';
import { RecipeViewType, RecipeEnum } from '../recipe/recipe-view-type';
import { RecipeService } from '../recipe/recipe.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  // test: AuthModel;
  recipeList: Recipe[];
  girdStyle = RecipeEnum.normal
  testOnly;

  constructor(private service: RecipeService, private router: Router, private recipeSaver: RecipeSaver) { }

  ngOnInit(): void {
    this.getAllRecipes();
  }

  getAllRecipes() {
    console.log("in get all recipes");
    this.service.getAllRecipes().subscribe((reply) => {
      console.log(reply);
      this.recipeList = reply;
      this.fillTest();
    });
  }

  getRecipeImageStyle(recipe: Recipe): Object {
    return {
      background:
        'linear-gradient(rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2)), url(' + recipe.recipeImage + ') no-repeat center',
      "background-size": "cover"
    };
  }

  fillTest() {
    this.testOnly = Array(10).fill(this.recipeList[0]).map((x, i) => x);
    console.log("this.testOnly");
    console.log(this.testOnly);
  }

  goToDetail(recipe: Recipe) {
    // let params: NavigationExtras = {
    //   queryParams: {
    //     "recipe": JSON.stringify(recipe)
    //   }
    // }
    // this.recipeSaver.storage = recipe;
    this.router.navigate([`recipeDetail/${+recipe.recipeId}`]);
  }
}


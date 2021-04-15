import { Component, OnInit } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { take } from 'rxjs/operators';
import { AuthModel } from '../auth-model';
import { Recipe } from '../recipe/recipe';
import { RecipeSaver } from '../recipe/recipe-saver';
import { RecipeViewType, RecipeEnum } from '../recipe/recipe-view-type';
import { RecipeService } from '../recipe/recipe.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  // test: AuthModel;
  recipeList: Recipe[];
  girdStyle = RecipeEnum.search
  testOnly: Recipe[];
  searchString: string;

  constructor(private service: RecipeService, private router: Router, private recipeSaver: RecipeSaver) { }

  ngOnInit(): void {
    this.getAllRecipes();
  }

  getAllRecipes() {
    console.log("in get all recipes");
    this.service.getAllRecipes().subscribe((reply) => {
      console.log(reply);
      this.recipeList = reply;
      this.testOnly = this.recipeList;
      // this.fillTest();
      take(1);
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
    // this.testOnly = this.recipeList;
    this.testOnly = Array(10).fill(this.recipeList[0]).map((x, i) => {
      let r = new Recipe();
      r.recipeName = x.recipeName + i;
      r.recipeId = x.recipeId;
      r.numTimesPrepared = x.numTimesPrepared;
      r.tags = x.tags;
      r.recipeImage = x.recipeImage;
      r.ingredients = x.ingredients
      return r;
    });
    this.testOnly = this.testOnly.concat(Array(5).fill(this.recipeList[1]).map((x, i) => {
      let r = new Recipe();
      r.recipeName = x.recipeName + i;
      r.recipeId = x.recipeId;
      r.numTimesPrepared = x.numTimesPrepared;
      r.tags = x.tags;
      r.recipeImage = x.recipeImage;
      r.ingredients = x.ingredients
      return r;
    }));
    this.testOnly = this.testOnly.concat(Array(5).fill(this.recipeList[2]).map((x, i) => {
      let r = new Recipe();
      r.recipeName = x.recipeName + i;
      r.recipeId = x.recipeId;
      r.numTimesPrepared = x.numTimesPrepared;
      r.tags = x.tags;
      r.recipeImage = x.recipeImage;
      r.ingredients = x.ingredients
      return r;
    }));
    this.testOnly = this.testOnly.concat(Array(5).fill(this.recipeList[3]).map((x, i) => {
      let r = new Recipe();
      r.recipeName = x.recipeName + i;
      r.recipeId = x.recipeId;
      r.numTimesPrepared = x.numTimesPrepared;
      r.tags = x.tags;
      r.recipeImage = x.recipeImage;
      r.ingredients = x.ingredients
      return r;
    }));
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

  withinSearch(recipe: Recipe): boolean {
    if (!this.searchString) return true;
    if (this.searchString.trim().length <= 0) return true;
    console.log(this.searchString);
    if (recipe.recipeName.toLowerCase().includes(this.searchString.toLowerCase())) {
      console.log(recipe.recipeName);
      return true;
    }
    // if (recipe.recipeAuthor.toLowerCase().includes(this.searchString.toLowerCase())) {
    //   console.log(recipe.recipeAuthor);
    //   return true;
    // }
    if (recipe.tags.some(tag => tag.tagName.toLowerCase().includes(this.searchString.toLowerCase()))) {
      console.log(recipe.tags);
      return true;
    }
    if (recipe.ingredients.some(ing => ing.ingredientName.toLowerCase().includes(this.searchString.toLowerCase()))) {
      console.log(recipe.ingredients);
      return true;
    }
    return false;
  }
}


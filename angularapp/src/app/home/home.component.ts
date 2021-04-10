import { Component, OnInit } from '@angular/core';
import { AuthModel } from '../auth-model';
import { Recipe } from '../recipe/recipe';
import { RecipeService } from '../recipe/recipe.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  // test: AuthModel;
  recipeList: Recipe[];

  constructor(private service: RecipeService) { }

  ngOnInit(): void {
    this.getAllRecipes();
  }

  getAllRecipes() {
    this.service.getAllRecipes().subscribe((reply) => {
      console.log(reply);
      this.recipeList = reply;
    })
  }
}


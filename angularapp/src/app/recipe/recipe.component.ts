import { Component, Input, OnInit } from '@angular/core';
import { Recipe } from './recipe';
import { RecipeEnum, RecipeViewType } from './recipe-view-type';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.css']
})
export class RecipeComponent implements OnInit {
  @Input() recipeViewType: string;
  @Input() recipe: Recipe;
  constructor() { }

  ngOnInit(): void {
  }

  goToDetail() {
    console.log(this.recipe.recipeId);
  }

}

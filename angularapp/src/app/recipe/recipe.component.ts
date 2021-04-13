import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Recipe } from './recipe';
import { RecipeSaver } from './recipe-saver';
import { RecipeEnum, RecipeViewType } from './recipe-view-type';
import { RecipeService } from './recipe.service';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.css']
})
export class RecipeComponent implements OnInit {
  @Input() recipeViewType: string;
  @Input() recipe: Recipe;
  constructor(private route: ActivatedRoute, private recipeService: RecipeService) { }

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
    this.getRecipeInfo(+this.route.snapshot.paramMap.get("id"));
  }

  // goToDetail() {
  //   console.log(this.recipe.recipeId);
  // }

  getRecipeInfo(id: number): void {
    this.recipeService.getRecipeId(id).subscribe((reply) => {
      console.log("recipe reply");
      console.log(reply);
      this.recipe = reply;
    })
  }

}

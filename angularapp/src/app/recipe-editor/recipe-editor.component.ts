import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AuthModel } from '../auth-model';
import { AuthService } from '../auth.service';
import { Recipe } from '../recipe/recipe';
import { RecipeService } from '../recipe/recipe.service';
import { Ingredient } from './ingredient';
import { IngredientService } from './ingredient.service';
import { Tag } from './tag';
import { TagService } from './tag.service';

@Component({
  selector: 'app-recipe-editor',
  templateUrl: './recipe-editor.component.html',
  styleUrls: ['./recipe-editor.component.css']
})
export class RecipeEditorComponent implements OnInit {
  userModel: AuthModel;
  recipe: Recipe;
  newStep: any = {};
  newIngredient: Ingredient = new Ingredient();
  newTag: Tag = new Tag();
  tagChoices: Tag[];
  tagChoice: Tag = new Tag();
  ingChoices: Ingredient[];
  ingChoice: Ingredient;
  tagControl: FormControl = new FormControl('', Validators.required);
  ingControl: FormControl = new FormControl('', Validators.required);
  isLoading: boolean = false;

  constructor(private route: ActivatedRoute,
    private recipeService: RecipeService,
    private authService: AuthService,
    private tagService: TagService,
    private ingService: IngredientService
  ) { }

  ngOnInit(): void {
    this.authService.authModel$.subscribe(reply => {
      if (reply != null || reply != undefined || reply.firstName != null) {
        this.userModel = reply;
      }
    });
    if (this.userModel == null || this.userModel == undefined) {
      this.userModel = this.authService.authModel;
    }
    this.getRecipeInfo(+this.route.snapshot.paramMap.get("id"));
    this.getTagInfo();
    this.getIngInfo();
  }
  getTagInfo() {
    this.tagService.getTags().then(reply => {
      console.log("tag reply");
      console.log(reply);
      this.tagChoices = reply;
    }).catch(err => {
      console.log("error getting tags");
      console.error(err);
    });
  }
  getIngInfo() {
    this.ingService.getIngredients().then(reply => {
      console.log("ingredient reply");
      this.ingChoices = reply;
    }).catch(err => {
      console.log("error getting ingredients");
      console.error(err);
    });
  }

  addNewStep() {
    if (!this.checkNewNumber()) return;
    const s = Object.assign({}, this.newStep);
    this.recipe.steps.push(s);
    this.newStep = {};
  }

  checkNewNumber(): boolean {
    if (this.recipe.steps.some(s => {
      return s.recipeStepNo == this.newStep.recipeStepNo;
    })) return false;
    else
      return true;
  }

  removeStep(step: any) {
    this.recipe.steps = this.recipe.steps.filter(savedStep => {
      return (savedStep.stepDescription != step.stepDescription &&
        savedStep.recipeStepNo != step.recipeStepNo &&
        savedStep.stepImage != savedStep.stepImage);
    });
  }

  addIngChoiceToRecipe() {
    console.log("trying to add: ");
    console.log(this.ingChoice);
    this.ingChoice = this.findIngByName(this.ingControl.value);
    this.addIngToRecipeIngredients(this.ingChoice);
  }

  addIngToRecipeIngredients(newIng): any {
    if (!this.recipe.ingredients.some(savedIngredient => {
      return this.areIngredientsEquivilant(savedIngredient, newIng);
    })) {
      console.log("adding ingredient in recipe");
      const s = Object.assign({}, newIng);
      this.recipe.ingredients.push(s);
      console.log(this.recipe);
      return s;
    } else {
      console.log("found a ingredient in recipe");
      return undefined;
    }
  }

  addNewIngredinet() {
    const addedIng = this.addIngToRecipeIngredients(this.newIngredient);
    if (addedIng) {
      this.ingChoices.push(addedIng);
      this.newIngredient = new Ingredient();
    }
  }


  findIngByName(ingredientName: string): Ingredient {
    for (let index = 0; index < this.ingChoices.length; index++) {
      const tagch = this.ingChoices[index];
      if (tagch.ingredientName == ingredientName) return tagch;
    }
  }

  removeIngredient(ingredient: any) {
    // filter out any equivilant ingredient
    this.recipe.ingredients = this.recipe.ingredients.filter(savedIngredient => {
      // return false if same
      return !this.areIngredientsEquivilant(savedIngredient, ingredient);
    });
  }

  addTagChoiceToRecipe() {
    console.log("trying to add: ");
    console.log(this.tagChoice);
    console.log("form contorl tag: ");
    console.log(this.tagControl.value);
    this.tagChoice = this.findTagByName(this.tagControl.value);
    this.addTagToRecipeTags(this.tagChoice);
  }

  addTagToRecipeTags(newTag: any): any {
    if (!this.recipe.tags.some(savedTag => {
      return this.areTagsEquivilant(savedTag, newTag);
    })) {
      console.log("adding tag in recipe");
      const s = Object.assign({}, newTag);
      console.log(s);
      this.recipe.tags.push(s);
      console.log(this.recipe);
      return s;
    } else {
      console.log("found a tag in recipe");
      return undefined;
    }
  }

  findTagByName(tagname: string): Tag {
    for (let index = 0; index < this.tagChoices.length; index++) {
      const tagch = this.tagChoices[index];
      if (tagch.tagName == tagname) return tagch;
    }
  }

  addNewTag() {
    const addedTag = this.addTagToRecipeTags(this.newTag);
    if (addedTag) {
      this.tagChoices.push(addedTag);
      this.newTag = new Tag();
    }
  }

  removeTag(tag: any) {
    // filter out any equivilant tag
    this.recipe.tags = this.recipe.tags.filter(savedTag => {
      // return false if same
      return !this.areTagsEquivilant(savedTag, tag);
    });
  }

  areTagsEquivilant(tag1: Tag, tag2: Tag) {
    console.log(tag1.tagName.includes(tag2.tagName) || tag2.tagName.includes(tag1.tagName) || tag1.tagName == tag2.tagName);
    return tag1.tagName.includes(tag2.tagName) || tag2.tagName.includes(tag1.tagName) || tag1.tagName == tag2.tagName;
  }


  areIngredientsEquivilant(eng1: Ingredient, eng2: Ingredient) {
    return eng1.ingredientName.includes(eng2.ingredientName) || eng2.ingredientName.includes(eng1.ingredientName) || eng1.ingredientName == eng2.ingredientName;
  }

  getRecipeInfo(id: number): void {
    if (!(id > 0)) {
      this.recipe = new Recipe();
      this.recipe.steps = [];
      this.recipe.recipeAuthors = [];
      this.recipe.ingredients = [];
      this.recipe.tags = [];
      this.recipe.reviews = [];
      return;
    }
    console.log("getting recipe");
    this.recipeService.getRecipeId(id).then((reply) => {
      console.log("recipe reply");
      console.log(reply);
      console.log("recipe reply");
      if (reply == null || reply == undefined || reply)
        this.recipe = reply;
      console.log("getting reviews");
    }).catch(err => {
      console.log("error getting recipes");
      console.error(err);
    });
  }

  sendNewRecipe() {
    this.isLoading = true;
    this.recipeService.saveRecipe(this.recipe).then(reply => {
      console.log("save recipe reply: ");
      console.log(reply);
      window.location.href = "/";
      this.isLoading = false;
    });
  }

}

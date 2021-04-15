import { Injectable, NgModule } from '@angular/core';
import { Recipe } from './recipe';

@Injectable()
export class RecipeSaver {

    public storage: Recipe;

    public constructor() { }

}
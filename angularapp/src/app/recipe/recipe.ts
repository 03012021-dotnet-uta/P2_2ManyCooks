import { NumberInput } from "@angular/cdk/coercion";
import { Moment } from "moment";

export class Recipe {
    recipeId: number;
    recipeName: string;
    recipeDescription: string;
    recipeImage: string;
    recipeAuthor: string;
    dateCreated: Moment;
    dateLastPrepared: Moment;
    numTimesPrepared: number;
    recipeAuthors: any[];
    ingredients: any[];
    tags: any[];
    steps: any[];
}

import { Component, ElementRef, Input, OnInit, Output, ViewChild } from '@angular/core';
import { delay } from 'rxjs/operators';
import { Recipe } from '../recipe';

@Component({
  selector: 'app-recipe-guide',
  templateUrl: './recipe-guide.component.html',
  styleUrls: ['./recipe-guide.component.css']
})
export class RecipeGuideComponent implements OnInit {
  @Input() recipe: Recipe;
  currentStepId: number;

  constructor() { }

  ngOnInit(): void {
    for (let index = 1; index < this.recipe.steps.length; index++) {
      this.recipe.steps[index - 1].nextStep = this.recipe.steps[index];
    }
    this.currentStepId = this.recipe.steps[0].recipeId;
    setTimeout(() => {
      const top = document.querySelector(".selected");
      top.scrollIntoView({ behavior: "smooth", block: "start", inline: "nearest" });
    }, 200);
  }

  scrollToElement(step) {
    // @ViewChild('myDiv');
    this.currentStepId = step.stepId;
    setTimeout(() => {
      // let element = document.querySelector(`#step${step.stepId}`);
      let element = document.querySelector(`.selected`);
      console.log(element);
      element.scrollIntoView({ behavior: "smooth", block: "start", inline: "nearest" });
      // element.scrollIntoView();
      // window.scrollTo(0, document.getElementById(`step${step.stepId}`).offsetTop);
    }, 550);
  }

  updateCurrentStepForUser(step) {

  }

  getPreviousStep(step) {
    let stepppp = null;
    for (let index = 0; index < this.recipe.steps.length; index++) {
      const element = this.recipe.steps[index];
      if (element.nextStep.stepId == step.stepId) {
        return element;
      }
    }
  }

  hasNext(step) {
    return this.recipe.steps[this.recipe.steps.length - 1] != step
  }

  hasPrevious(step) {
    return this.recipe.steps[0] != step
  }

  scrollToPrevious(nextstep) {
    console.log(nextstep);
    console.log("nextstep");
    let step = this.getPreviousStep(nextstep);
    this.scrollToElement(step);
  }

  isAtCurrentStep(step) {
    return step.stepId == this.currentStepId
  }
}

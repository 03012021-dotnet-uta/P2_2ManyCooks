import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { RecipeEditorComponent } from './recipe-editor.component';
import { Recipe } from '../recipe/recipe';
import { Ingredient } from './ingredient';
import { Tag } from './tag';
describe('RecipeEditorComponent', () => {
  let component: RecipeEditorComponent;
  let fixture: ComponentFixture<RecipeEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RecipeEditorComponent],
      imports: [RouterTestingModule, HttpClientTestingModule]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecipeEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('should run', () => {
    fixture = TestBed.createComponent(RecipeEditorComponent);
    component = fixture.componentInstance;
    component.recipe = new Recipe();
    component.recipe.steps = [];
    component.newStep = { "step": "step" };
    component.addNewStep();
    fixture.detectChanges();
  });


  it('should run', () => {
    fixture = TestBed.createComponent(RecipeEditorComponent);
    component = fixture.componentInstance;
    component.recipe = new Recipe();
    component.recipe.steps = [];
    component.newStep = { "step": "step" };
    component.removeStep({});
    fixture.detectChanges();
  });


  it('should run', () => {
    fixture = TestBed.createComponent(RecipeEditorComponent);
    component = fixture.componentInstance;
    component.recipe = new Recipe();
    component.recipe.steps = [];
    component.removeStep({});
    fixture.detectChanges();
  });


  it('should run', () => {
    fixture = TestBed.createComponent(RecipeEditorComponent);
    component = fixture.componentInstance;
    component.recipe = new Recipe();
    component.recipe.ingredients = [];
    component.ingChoice = new Ingredient();
    component.ingChoices = [new Ingredient()];
    component.addIngChoiceToRecipe();
    fixture.detectChanges();
  });


  it('should run', () => {
    fixture = TestBed.createComponent(RecipeEditorComponent);
    component = fixture.componentInstance;
    component.recipe = new Recipe();
    component.recipe.ingredients = [];
    component.ingChoice = new Ingredient();
    component.ingChoices = [new Ingredient()];
    component.addNewIngredinet();
    fixture.detectChanges();
  });

  it('should run', () => {
    fixture = TestBed.createComponent(RecipeEditorComponent);
    component = fixture.componentInstance;
    component.recipe = new Recipe();
    component.recipe.ingredients = [];
    component.ingChoice = new Ingredient();
    component.ingChoices = [new Ingredient()];
    component.findIngByName("");
    fixture.detectChanges();
  });

  it('should run', () => {
    fixture = TestBed.createComponent(RecipeEditorComponent);
    component = fixture.componentInstance;
    component.recipe = new Recipe();
    component.recipe.ingredients = [];
    component.ingChoice = new Ingredient();
    component.ingChoices = [new Ingredient()];
    component.removeIngredient(new Ingredient());
    fixture.detectChanges();
  });

  it('should run', () => {
    fixture = TestBed.createComponent(RecipeEditorComponent);
    component = fixture.componentInstance;
    component.recipe = new Recipe();
    component.recipe.tags = [];
    component.tagChoice = new Tag();
    component.tagChoices = [new Tag()];
    component.addTagChoiceToRecipe();
    fixture.detectChanges();
  });


  it('should run', () => {
    fixture = TestBed.createComponent(RecipeEditorComponent);
    component = fixture.componentInstance;
    component.recipe = new Recipe();
    component.recipe.tags = [];
    component.tagChoice = new Tag();
    component.tagChoices = [new Tag()];
    component.addTagToRecipeTags(new Tag());
    fixture.detectChanges();
  });

  it('should run', () => {
    fixture = TestBed.createComponent(RecipeEditorComponent);
    component = fixture.componentInstance;
    component.recipe = new Recipe();
    component.recipe.tags = [];
    component.tagChoice = new Tag();
    component.tagChoices = [new Tag()];
    component.findTagByName("");
    fixture.detectChanges();
  });

  it('should run', () => {
    fixture = TestBed.createComponent(RecipeEditorComponent);
    component = fixture.componentInstance;
    component.recipe = new Recipe();
    component.recipe.tags = [];
    component.tagChoice = new Tag();
    component.tagChoices = [new Tag()];
    component.addNewTag();
    fixture.detectChanges();
  });

  it('should run', () => {
    fixture = TestBed.createComponent(RecipeEditorComponent);
    component = fixture.componentInstance;
    component.recipe = new Recipe();
    component.recipe.tags = [];
    component.tagChoice = new Tag();
    component.tagChoices = [new Tag()];
    component.removeTag(new Tag());
    fixture.detectChanges();
  });

  it('should run', () => {
    fixture = TestBed.createComponent(RecipeEditorComponent);
    component = fixture.componentInstance;
    component.getRecipeInfo(0);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

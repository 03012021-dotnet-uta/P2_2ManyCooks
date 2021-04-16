import { FormsModule } from '@angular/forms';
import { RecipeSaver } from './../recipe/recipe-saver';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { HomeComponent } from './home.component';
import { Recipe } from '../recipe/recipe';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HomeComponent],
      imports: [RouterTestingModule, HttpClientTestingModule, FormsModule],
      providers: [RecipeSaver]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should run', () => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    component.getRecipeImageStyle(new Recipe())
    fixture.detectChanges();
  });

  it('should run', () => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    component.goToPage(1);
    fixture.detectChanges();
  });

  it('should run', () => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    component.getCurrentRecipes();
    fixture.detectChanges();
  });

  it('should run', () => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    component.toggleSort();
    fixture.detectChanges();
  });

  it('should run', () => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    component.pagesRecipes = [[1], [2]];
    component.currentPage = 1;
    component.sortByDefault();
    fixture.detectChanges();
  });

  it('should run', () => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    component.pagesRecipes = [[1], [2]];
    component.currentPage = 1;
    component.sortByPopularity();
    fixture.detectChanges();
  });

  it('should be undefined', () => {
    expect(component.getAllRecipes()).toBeUndefined();
  });
});

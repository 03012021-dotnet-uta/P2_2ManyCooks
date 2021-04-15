import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { RecipeService } from './recipe.service';

// describe('RecipeService', () => {
//   let service: RecipeService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule]
    });
    service = TestBed.inject(RecipeService);
  });

//   it('should be created', () => {
//     expect(service).toBeTruthy();
//   });
// });

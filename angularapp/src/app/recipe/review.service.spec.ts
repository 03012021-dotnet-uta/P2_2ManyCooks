import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { ReviewService } from './review.service';


describe('ReviewService', () => {
  let service: ReviewService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule]
    });
    service = TestBed.inject(ReviewService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

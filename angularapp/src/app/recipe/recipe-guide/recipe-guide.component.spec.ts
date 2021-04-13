import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeGuideComponent } from './recipe-guide.component';

describe('RecipeGuideComponent', () => {
  let component: RecipeGuideComponent;
  let fixture: ComponentFixture<RecipeGuideComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecipeGuideComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecipeGuideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

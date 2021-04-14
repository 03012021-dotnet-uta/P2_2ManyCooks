import { RecipeEnum, RecipeViewType } from './recipe-view-type';

describe('RecipeViewType', () => {
  it('should create an instance', () => {
    expect(new RecipeViewType(RecipeEnum.normal)).toBeTruthy();
  });
});

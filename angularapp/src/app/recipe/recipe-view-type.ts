export class RecipeViewType {
    constructor(public rtype: RecipeEnum) { }
}

export enum RecipeEnum {
    normal = "normal",
    search = "search",
    main = "main",
}

import { Moment } from "moment"

export class Review {
    reviewId: number
    reviewDescription: string
    reviewRating: number
    reviewDate: Moment
    recipeId: number
    userId: number
    user: any
}

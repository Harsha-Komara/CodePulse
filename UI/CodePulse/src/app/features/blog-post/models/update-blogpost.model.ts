import { Category } from "../../category/models/Category.model";

export interface updateBlogPostRequest {
    title: String,
    urlHandle: string,
    shortDescription: string,
    content: string,
    featuredImageUrl: string,
    publishedDate: Date,
    author: string,
    isVisible: Boolean,
    categories: string[]
}
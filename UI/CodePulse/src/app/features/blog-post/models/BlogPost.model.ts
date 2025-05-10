import { Category } from "../../category/models/Category.model";

export interface BlogPost {
    id: string,
    title: String,
    urlHandle: string,
    shortDescription: string,
    content: string,
    featuredImageUrl: string,
    publishedDate: Date,
    author: string,
    isVisible: Boolean,
    categories: Category[]
}
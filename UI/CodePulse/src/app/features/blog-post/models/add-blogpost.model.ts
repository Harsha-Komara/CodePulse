export interface AddBlogPost {
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
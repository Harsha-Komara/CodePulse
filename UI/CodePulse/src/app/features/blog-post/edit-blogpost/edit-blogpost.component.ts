import { Component, OnDestroy, OnInit } from '@angular/core';
import { updateBlogPostRequest } from '../models/update-blogpost.model';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogpostService } from '../services/blogpost.service';
import { Observable, Subscription } from 'rxjs';
import { Category } from '../../category/models/Category.model';
import { CategoryService } from '../../category/services/category.service';
import { BlogPost } from '../models/BlogPost.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css']
})
export class EditBlogpostComponent implements OnInit, OnDestroy {

  id: string | null = null;
  model?: BlogPost;
  subscription?: Subscription;
  categories$?: Observable<Category[]>;
  selectedCategories?: string[];
  isImageSelectorVisible: boolean = false;

  constructor(private route: ActivatedRoute, private blogPostService: BlogpostService, private categoryService: CategoryService, private router: Router, private imageService: ImageService) {
  }
  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
    this.subscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id) {
          this.subscription = this.blogPostService.getBlogPost(this.id).subscribe({
            next: (response) => {
              this.model = response;
              this.selectedCategories = response.categories.map(x => x.id);
            }
          })
        }

        this.subscription = this.imageService.onSelectImage().subscribe({
          next: (response) => {
            if (this.model) {
              this.model.featuredImageUrl = response.url;
              this.isImageSelectorVisible = !this.isImageSelectorVisible;
            }
          }
        })

      }
    })
  }
  onFormSubmit(): void {
    if (this.id && this.model) {
      var updateBlogPost: updateBlogPostRequest = {
        title: this.model.title,
        urlHandle: this.model.urlHandle,
        shortDescription: this.model.shortDescription,
        content: this.model.content,
        featuredImageUrl: this.model.featuredImageUrl,
        publishedDate: this.model.publishedDate,
        author: this.model.author,
        isVisible: this.model.isVisible,
        categories: this.selectedCategories ?? []
      }
      this.subscription = this.blogPostService.updateBlogPost(this.id, updateBlogPost).subscribe({
        next: (response) => {
          this.router.navigateByUrl('admin/blogpost');
        }
      })
    }
  }
  imageSelector(): void {
    this.isImageSelectorVisible = !this.isImageSelectorVisible;
  }
  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}

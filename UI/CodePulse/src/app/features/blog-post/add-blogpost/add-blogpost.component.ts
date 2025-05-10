import { Component, OnDestroy, OnInit } from '@angular/core';
import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blogpost.model';
import { BlogpostService } from '../services/blogpost.service';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/Category.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent implements OnDestroy, OnInit {
  model: AddBlogPost;
  Subscription?: Subscription;
  categories$?: Observable<Category[]>;
  isImageSelectorVisible: boolean = true;

  constructor(private blogPostService: BlogpostService, private router: Router, private categoryService: CategoryService, private imageService: ImageService) {
    this.model = {
      title: '',
      urlHandle: '',
      shortDescription: '',
      content: '',
      featuredImageUrl: '',
      publishedDate: new Date(),
      author: '',
      isVisible: true,
      categories: []
    }
  }
  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();

    this.Subscription = this.imageService.onSelectImage().subscribe({
      next: (response) => {
        if (this.model) {
          this.model.featuredImageUrl = response.url;
          this.isImageSelectorVisible = !this.isImageSelectorVisible;
        }
      }
    })
  }

  onFormSubmit(): void {
    this.Subscription = this.blogPostService.addBlogPost(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/admin/blogpost')
      }
    });
  }
  imageSelector(): void {
    this.isImageSelectorVisible = !this.isImageSelectorVisible;
  }
  ngOnDestroy(): void {
    this.Subscription?.unsubscribe();
  }

}

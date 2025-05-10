import { Component, OnDestroy, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/Category.model';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit, OnDestroy {

  categories$?: Observable<Category[]>;
  subscription?: Subscription;

  constructor(private categoryService: CategoryService) { }

  ngOnInit(search: string = ""): void {
    this.categories$ = this.categoryService.getAllCategories(search);
  }

  DeleteCategory(id: string) {
    this.subscription = this.categoryService.deleteCategory(id).subscribe({
      next: (response) => {
        this.ngOnInit();
      },
      error: (err) => {
        alert("Error Occured" + err)
      }
    });
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}

import { Component, OnDestroy, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { updateCategoryRequest } from '../models/update-category-request.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})

export class EditCategoryComponent implements OnInit, OnDestroy {

  id: string | null = null;
  Subscription?: Subscription;
  category: updateCategoryRequest;


  constructor(private route: ActivatedRoute, private categoryService: CategoryService, private router: Router) {
    this.category = {
      name: '',
      urlHandle: ''
    }
  }

  ngOnInit(): void {
    this.Subscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id) {
          this.Subscription = this.categoryService.getCategory(this.id).subscribe({
            next: (response) => {
              this.category = response
            }
          });
        }
      }
    })
  };

  OnFormSubmit() {
    if (this.id) {
      this.Subscription = this.categoryService.updateCategory(this.id, this.category).subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/categories')
        }
      })
    }
  };

  ngOnDestroy(): void {
    this.Subscription?.unsubscribe();
  };

}

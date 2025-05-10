import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogpostService } from '../services/blogpost.service';
import { Observable, Subscription } from 'rxjs';
import { BlogPost } from '../models/BlogPost.model';

@Component({
  selector: 'app-blogpost-list',
  templateUrl: './blogpost-list.component.html',
  styleUrls: ['./blogpost-list.component.css']
})
export class BlogpostListComponent implements OnInit, OnDestroy {

  blogPosts$?: Observable<BlogPost[]>;
  subscription?: Subscription;

  constructor(private blogPostService: BlogpostService) {

  }

  ngOnInit(): void {
    this.blogPosts$ = this.blogPostService.getAllBlogPosts();
  }
  DeleteBlogPost(id: string) {
    this.subscription = this.blogPostService.deleteBlogPost(id).subscribe({
      next: (response) => {
        this.ngOnInit();
      },
      error: (err) => {
        alert("Error Occured" + err);
        console.log(err)
      }
    })
  }
  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}

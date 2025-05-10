import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogpostService } from '../../blog-post/services/blogpost.service';
import { Subscription } from 'rxjs';
import { BlogPost } from '../../blog-post/models/BlogPost.model';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blog-details.component.html',
  styleUrls: ['./blog-details.component.css']
})
export class BlogDetailsComponent implements OnInit, OnDestroy {

  url: string | null = null;
  subscription?: Subscription;
  blogPost?: BlogPost

  constructor(private route: ActivatedRoute, private blogPostService: BlogpostService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.url = params.get('url')
      }
    })
    if (this.url) {
      this.subscription = this.blogPostService.getBlogPostByUrl(this.url).subscribe({
        next: (response) => {
          this.blogPost = response;
        }
      })
    }
  }
  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}

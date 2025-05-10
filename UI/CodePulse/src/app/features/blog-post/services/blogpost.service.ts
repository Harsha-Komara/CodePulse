import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blogpost.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BlogPost } from '../models/BlogPost.model';
import { updateBlogPostRequest } from '../models/update-blogpost.model';


@Injectable({
  providedIn: 'root'
})

export class BlogpostService {

  constructor(private http: HttpClient) { }
  addBlogPost(model: AddBlogPost): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/blogpost?addAuth=true`, model)
  }

  getAllBlogPosts(): Observable<BlogPost[]> {
    return this.http.get<BlogPost[]>(`${environment.apiBaseUrl}/api/blogpost`)
  }

  getBlogPost(id: string): Observable<BlogPost> {
    return this.http.get<BlogPost>(`${environment.apiBaseUrl}/api/blogpost/${id}`)
  }

  getBlogPostByUrl(url: string): Observable<BlogPost> {
    return this.http.get<BlogPost>(`${environment.apiBaseUrl}/api/blogpost/${url}`)
  }

  updateBlogPost(id: string, model: updateBlogPostRequest): Observable<void> {
    return this.http.put<void>(`${environment.apiBaseUrl}/api/blogpost/${id}?addAuth=true`, model)
  }

  deleteBlogPost(id: string): Observable<void> {
    return this.http.delete<void>(`${environment.apiBaseUrl}/api/blogpost/${id}?addAuth=true`);
  }
}

import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Category } from '../models/Category.model';
import { environment } from 'src/environments/environment';
import { updateCategoryRequest } from '../models/update-category-request.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  addCategory(model: AddCategoryRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/categories?addAuth=true`, model)
  }

  getAllCategories(search: string = ""): Observable<Category[]> {

    let params = new HttpParams();

    params = params.set('search', search);

    return this.http.get<Category[]>(`${environment.apiBaseUrl}/api/categories`, {
      params: params
    })
  }

  getCategory(id: String): Observable<Category> {
    return this.http.get<Category>(`${environment.apiBaseUrl}/api/categories/${id}`)
  }

  updateCategory(id: string, model: updateCategoryRequest): Observable<void> {
    return this.http.put<void>(`${environment.apiBaseUrl}/api/categories/${id}?addAuth=true`, model)
  }

  deleteCategory(id: string): Observable<void> {
    return this.http.delete<void>(`${environment.apiBaseUrl}/api/categories/${id}?addAuth=true`);
  }
}

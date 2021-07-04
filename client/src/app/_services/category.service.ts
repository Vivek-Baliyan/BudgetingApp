import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MasterCategory } from '../_models/masterCategory';
import { SubCategory } from '../_models/subCategory';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getCategories(id: number) {
    return this.http.get<MasterCategory[]>(this.baseUrl + 'category/' + id);
  }

  saveMaster(category: MasterCategory, appUserId: number) {
    category.appUserId = appUserId;
    return this.http.post(this.baseUrl + 'category/saveMaster', category);
  }

  saveSub(category: SubCategory) {
    return this.http.post(this.baseUrl + 'category/saveSub', category);
  }
}

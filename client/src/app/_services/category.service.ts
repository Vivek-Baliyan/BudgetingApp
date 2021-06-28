import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MasterCategory } from '../_models/masterCategory';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) {}

  getCategories(id: number) {
    return this.http.get<MasterCategory[]>(this.baseUrl + 'category/' + id);
  }
}

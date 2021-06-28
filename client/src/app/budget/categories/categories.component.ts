import { Component, OnInit } from '@angular/core';
import { MasterCategory } from 'src/app/_models/masterCategory';
import { CategoryService } from 'src/app/_services/category.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit {
  categories: any[] = [];

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getCategories(1).subscribe((categories) => {
      if (categories.length > 0) {
        categories.forEach((m) => {
          var masterCategory = Object.assign({ isSubCategory: false }, m);
          this.categories.push(masterCategory);
          m.subCategories.forEach((s) => {
            if (s.masterCategoryId === m.id) {
              var subCategory = Object.assign({ isSubCategory: true }, s);
              this.categories.push(subCategory);
            }
          });
        });
      }
    });
  }
}

import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { take } from 'rxjs/operators';
import { MasterCategory } from 'src/app/_models/masterCategory';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { CategoryService } from 'src/app/_services/category.service';
import { UsersService } from 'src/app/_services/users.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit {
  user: User;
  @Output() cancelCategory = new EventEmitter();
  categoryForm: FormGroup;

  categories: any[] = [];
  masterCategories: MasterCategory[] = [];
  categoryTy;

  constructor(
    private usersService: UsersService,
    private categoryService: CategoryService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.usersService.currentUser$
      .pipe(take(1))
      .subscribe((user) => (this.user = user));
    this.initializeForm();
    this.loadCategories();
  }

  initializeForm() {
    this.categoryForm = this.fb.group({
      masterCategoryId: [0, Validators.required],
      categoryType: ['0', Validators.required],
      categoryName: ['', Validators.required],
    });
  }

  save() {
    if (this.categoryForm.value.categoryType == 1) {
      this.categoryService
        .saveMaster(this.categoryForm.value, this.user.id)
        .subscribe(
          (masterCategories: MasterCategory[]) => {
            this.categories = this.flattenCategories(masterCategories);
          },
          (error) => {
            console.log(error);
          }
        );
    }
    if (this.categoryForm.value.categoryType == 2) {
      this.categoryService.saveSub(this.categoryForm.value).subscribe(
        (masterCategories: MasterCategory[]) => {
          this.categories = this.flattenCategories(masterCategories);
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }

  cancel() {
    this.cancelCategory.emit(false);
  }

  loadCategories() {
    this.categoryService
      .getCategories(this.user.id)
      .subscribe((masterCategories) => {
        this.categories = this.flattenCategories(masterCategories);
      });
  }

  flattenCategories(masterCategories: MasterCategory[]): any[] {
    this.masterCategories = masterCategories;
    var flatCategories: any[] = [];
    if (masterCategories.length > 0) {
      masterCategories.forEach((m) => {
        flatCategories.push(Object.assign({ isSubCategory: false }, m));
        m.subCategories.forEach((s) => {
          if (s.masterCategoryId === m.id) {
            flatCategories.push(Object.assign({ isSubCategory: true }, s));
          }
        });
      });
    }
    return flatCategories;
  }
}

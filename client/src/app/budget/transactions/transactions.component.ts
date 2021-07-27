import { DatePipe } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { take } from 'rxjs/operators';
import { Account } from 'src/app/_models/account';
import { MasterCategory } from 'src/app/_models/masterCategory';
import { Transaction } from 'src/app/_models/transaction';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { CategoryService } from 'src/app/_services/category.service';
import { TransactionService } from 'src/app/_services/transaction.service';
import { UsersService } from 'src/app/_services/users.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css'],
})
export class TransactionComponent implements OnInit {
  user: User;
  accounts: Account[];
  masterCategories: MasterCategory[];
  categories: any[];
  transactions: Transaction[];
  @Output() cancelSave = new EventEmitter();
  transactionForm: FormGroup;
  pipe = new DatePipe('en-IN');

  constructor(
    private transactionService: TransactionService,
    private accountService: AccountService,
    private categoryService: CategoryService,
    private usersService: UsersService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.usersService.currentUser$
      .pipe(take(1))
      .subscribe((user) => (this.user = user));
    this.loadTransactions();
    this.loadAccounts();
    this.loadCategories();
    this.initializeForm();
  }

  initializeForm() {
    this.transactionForm = this.fb.group({
      date: [new Date(), Validators.required],
      accountId: [0, Validators.required],
      categoryId: [0, Validators.required],
      payee: ['', Validators.required],
      memo: [''],
      creditAmount: ['0'],
      debitAmount: ['0'],
    });
  }

  loadAccounts() {
    this.accountService.getAccounts(this.user.id).subscribe(
      (accounts) => {
        this.accounts = accounts;
      },
      (error) => {
        console.log(error);
      }
    );
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
  loadTransactions() {
    this.transactionService
      .getTransactions(this.user.id)
      .subscribe((transactions) => {
        this.transactions = transactions;
      });
  }

  save() {
    this.transactionService.save(this.transactionForm.value).subscribe(
      (transactions: Transaction[]) => {
        this.transactions = transactions;
        this.initializeForm();
      },
      (error) => {
        console.log(error);
      }
    );
  }
  cancel() {
    this.initializeForm();
    this.cancelSave.emit(false);
  }
}

import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { take } from 'rxjs/operators';
import { Account } from 'src/app/_models/account';
import { AccountType } from 'src/app/_models/accountType';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { UsersService } from 'src/app/_services/users.service';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css'],
})
export class AccountsComponent implements OnInit {
  user: User;
  @Output() cancelRegister = new EventEmitter();
  accountForm: FormGroup;
  validationErrors: string[] = [];

  accountTypes: AccountType[];
  accounts: Account[];

  constructor(
    private accountService: AccountService,
    private usersService: UsersService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.usersService.currentUser$
      .pipe(take(1))
      .subscribe((user) => (this.user = user));
    this.initializeForm();
    this.loadAccountTypes();
    this.loadAccounts();
  }

  initializeForm() {
    this.accountForm = this.fb.group({
      accountTypeId: [0, Validators.required],
      accountName: ['', Validators.required],
      openingBalance: ['', Validators.required],
    });
  }

  save() {
    var account: Account = {
      id: 0,
      appUserId: this.user.id,
      accountName: this.accountForm.value.accountName,
      accountType: {
        id: this.accountForm.value.accountTypeId,
        typeName: '',
      },
    };

    this.accountService.saveAccount(account).subscribe(
      (accounts: Account[]) => {
        this.accounts = accounts;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  loadAccountTypes() {
    this.accountService.getAccountTypes().subscribe((types) => {
      this.accountTypes = types;
    });
  }
  loadAccounts() {
    this.accountService.getAccounts(this.user.id).subscribe((accounts) => {
      this.accounts = accounts;
    });
  }
}

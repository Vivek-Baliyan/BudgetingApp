import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
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
  @Output() cancelSave = new EventEmitter();
  accountForm: FormGroup;
  validationErrors: string[] = [];

  accountTypes: AccountType[];
  accounts: Account[];

  constructor(
    private accountService: AccountService,
    private usersService: UsersService,
    private toastr: ToastrService,
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
      accountId: [0],
      accountName: ['', Validators.required],
      openingBalance: ['', Validators.required],
    });
  }

  save() {
    var account: Account = {
      appUserId: this.user.id,
      id: this.accountForm.value.accountId,
      accountName: this.accountForm.value.accountName,
      accountType: {
        id: this.accountForm.value.accountTypeId,
        typeName: '',
      },
    };
    if (account.id === 0) {
      this.accountService.save(account).subscribe(
        (accounts: Account[]) => {
          this.accounts = accounts;
          this.initializeForm();
        },
        (error) => {
          console.log(error);
        }
      );
    } else {
      this.accountService.update(account).subscribe((accounts: Account[]) => {
        this.accounts = accounts;
        this.initializeForm();
      },
      (error) => {
        console.log(error);
      });
    }
  }

  edit(account: Account) {
    this.accountForm.setValue({
      accountTypeId: account.accountType.id,
      accountId: account.id,
      accountName: account.accountName,
      openingBalance: 0,
    });
  }
  delete(account: Account) {}

  cancel() {
    this.initializeForm();
    this.cancelSave.emit(false);
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

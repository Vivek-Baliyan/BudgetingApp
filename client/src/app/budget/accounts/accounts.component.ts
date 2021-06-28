import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Account } from 'src/app/_models/account';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css'],
})
export class AccountsComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  accountForm: FormGroup;
  maxDate: Date;
  validationErrors: string[] = [];

  accounts: Account[];
  constructor(private memberService: MembersService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadAccounts();
  }

  initializeForm() {
    this.accountForm = this.fb.group({
      accountType: ['', Validators.required],
      accountName: ['', Validators.required],
      openingBalance: ['', Validators.required],
    });
  }

  save() {
    this.memberService.saveAccount(this.accountForm.value).subscribe(
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

  loadAccounts() {
    this.memberService.getAccounts(1).subscribe((accounts) => {
      this.accounts = accounts;
    });
  }
}

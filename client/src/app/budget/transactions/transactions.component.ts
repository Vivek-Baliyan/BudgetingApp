import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Transaction } from 'src/app/_models/transaction';
import { User } from 'src/app/_models/user';
import { TransactionService } from 'src/app/_services/transaction.service';
import { UsersService } from 'src/app/_services/users.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css'],
})
export class TransactionComponent implements OnInit {
  user: User;
  transactions: Transaction[];

  constructor(
    private transactionService: TransactionService,
    private usersService: UsersService
  ) {}

  ngOnInit(): void {
    this.usersService.currentUser$
      .pipe(take(1))
      .subscribe((user) => (this.user = user));
    this.loadTransactions();
  }

  loadTransactions() {
    this.transactionService
      .getTransactions(this.user.id)
      .subscribe((transactions) => {
        this.transactions = transactions;
      });
  }
}

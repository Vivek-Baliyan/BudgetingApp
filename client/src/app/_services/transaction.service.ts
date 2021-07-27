import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Account } from '../_models/account';
import { Transaction } from '../_models/transaction';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getTransactions(appUserId: number) {
    return this.http.get<Transaction[]>(
      this.baseUrl + 'transactions/' + appUserId
    );
  }

  getTransactionByAccountId(accountId: number) {
    return this.http.get<Transaction[]>(
      this.baseUrl + 'transactions/' + accountId
    );
  }

  save(transaction) {
    return this.http.post(this.baseUrl + 'transactions/save', transaction);
  }
}

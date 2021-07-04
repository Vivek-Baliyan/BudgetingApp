import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Account } from '../_models/account';
import { AccountType } from '../_models/accountType';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAccounts(id: number) {
    return this.http.get<Account[]>(this.baseUrl + 'account/' + id);
  }

  getAccountTypes() {
    return this.http.get<AccountType[]>(this.baseUrl + 'account/types');
  }

  save(account: Account) {
    return this.http.post(this.baseUrl + 'account/save', account);
  }
  update(account: Account) {
    return this.http.put(this.baseUrl + 'account/update', account);
  }
}

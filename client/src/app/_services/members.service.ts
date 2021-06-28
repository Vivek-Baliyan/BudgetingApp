import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Account } from '../_models/account';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAccounts(id: number) {
    return this.http.get<Account[]>(this.baseUrl + 'users/' + id);
  }

  saveAccount(account: Account) {
    account.appUserId = 1;
    return this.http.post(this.baseUrl + 'users/saveAccount', account);
  }
}

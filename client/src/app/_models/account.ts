import { AccountType } from './accountType';
import { Transaction } from './transaction';

export interface Account {
  id: number;
  accountName: string;
  appUserId: number;
  accountType: AccountType;
  transactions: Transaction[];
}

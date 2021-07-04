import { AccountType } from "./accountType";

export interface Account {
  id: number;
  accountName: string;
  appUserId: number;
  accountType: AccountType;
}

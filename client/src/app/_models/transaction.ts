export interface Transaction {
    id: number;
    payee: string;
    creditAmount: number;
    debtAmount: number;
    accountId: number;
    accountName: string;
  }
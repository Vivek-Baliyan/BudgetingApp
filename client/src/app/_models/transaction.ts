export interface Transaction {
  id: number;
  payee: string;
  date: Date;
  memo?: string;
  creditAmount: number;
  debitAmount: number;
  accountId: number;
  subCategoryId: number;
  accountName?: string;
  subCategoryName?: string;
  masterCategoryName?: string;
}

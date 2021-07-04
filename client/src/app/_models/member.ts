import { Account } from './account';

export interface Member {
  id: number;
  username: string;
  knownAs: string;
  created: Date;
  accounts: Account[];
}

using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IAccountRepository
    {
        void Update(Account account);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AccountDto>> GetAccountsByUserIdAsync(int appUserId);
        Task<Account> GetLastestAccountAsync();
        Task<IEnumerable<AccountType>> GetAccountTypesAsync();
        Task<AppUser> GetUserAccounts(int id);
    }
}
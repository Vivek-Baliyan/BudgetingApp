using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ITransactionRepository
    {
        void Update(AccountTransaction transaction);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<TransactionDto>> GetTransactionsByAccountIdAsync(int id);
        Task<IEnumerable<TransactionDto>> GetTransactionsAsync(int appUserId);
    }
}
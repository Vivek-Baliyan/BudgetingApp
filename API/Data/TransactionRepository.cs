using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TransactionRepository(DataContext context, IMapper mapper) : ITransactionRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Account> GetTransactionsByAccountIdAsync(int id)
        {
            return await _context.Accounts.Include(x => x.Transactions)
            .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsAsync(int appUserId)
        {
            return await _context.Transactions
            .Where(x => x.Account.AppUserId == appUserId)
            .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AccountTransaction transaction)
        {
            _context.Entry(transaction).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Remove(AccountTransaction transaction)
        {
            _context.Transactions.Remove(transaction);
        }
    }
}
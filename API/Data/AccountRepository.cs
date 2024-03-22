using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AccountRepository(DataContext context, IMapper mapper) : IAccountRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<AccountDto>> GetAccountsByUserIdAsync(int AppUserId)
        {
            return await _context.Accounts.Where(x => x.AppUserId == AppUserId)
            .ProjectTo<AccountDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<AccountType>> GetAccountTypesAsync()
        {
            return await _context.AccountTypes.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void Update(Account account)
        {
            _context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Remove(Account account)
        {
            _context.Accounts.Remove(account);
        }

        public async Task<Account> GetLastestAccountAsync()
        {
            return await _context.Accounts
            .Include(x => x.Transactions)
            .OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<AppUser> GetUserAccounts(int id)
        {
            return await _context.Users
            .Include(x => x.Accounts)
            .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
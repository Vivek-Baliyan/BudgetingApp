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
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AccountRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IEnumerable<AccountDto>> GetAccountsByUserIdAsync(int AppUserId)
        {
            return await _context.Accounts.Where(x => x.AppUserId == AppUserId)
            .ProjectTo<AccountDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<AccountType>> GetAccountTypesAsync()
        {
            return await _context.AccountTypes.ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(a => a.Accounts)
            .SingleOrDefaultAsync(x => x.Id == id);
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
    }
}
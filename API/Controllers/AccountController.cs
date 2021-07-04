using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class AccountController : BaseApiController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public AccountController(DataContext context, IAccountRepository accountRepository, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }
        
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<AccountType>>> GetAccountTypess()
        {
            var accounts = await _accountRepository.GetAccountTypesAsync();
            return Ok(accounts);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccountsByUserId(int id)
        {
            var accounts = await _accountRepository.GetAccountsByUserIdAsync(id);
            return Ok(accounts);
        }

        [HttpPost("saveAccount")]
        public async Task<ActionResult<IEnumerable<AccountDto>>> SaveAccount(AccountDto accountDto)
        {
            string accountName = accountDto.AccountName.ToLower();
            if (await AccountExists(accountName, accountDto.AppUserId)) return BadRequest("Account already exists");

            var account = new Account
            {
                AccountTypeId = accountDto.AccountType.Id,
                AccountName = accountDto.AccountName
            };

            var user = await _context.Users
            .Include(a => a.Accounts)
            .SingleOrDefaultAsync(x => x.Id == accountDto.AppUserId);

            user.Accounts.Add(account);

            if (await _accountRepository.SaveAllAsync())
            {
                return await GetAccountsByUserId(accountDto.AppUserId);
            }
            return BadRequest("Problem adding account");
        }

        private async Task<bool> AccountExists(string accountName, int appuserid)
        {
            return await _context.Accounts.Where(x => x.AppUserId == appuserid)
            .AnyAsync(x => x.AccountName.ToLower() == accountName);
        }
    }
}
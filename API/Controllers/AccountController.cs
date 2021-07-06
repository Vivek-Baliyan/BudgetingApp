using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class AccountController : BaseApiController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public AccountController(IAccountRepository accountRepository, IMapper mapper)
        {
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

        [HttpPost("save")]
        public async Task<ActionResult<IEnumerable<AccountDto>>> SaveAccount(AccountDto accountDto)
        {
            string accountName = accountDto.AccountName.ToLower();
            if (await AccountExists(accountName, accountDto.AppUserId)) return BadRequest("Account already exists");

            var account = new Account
            {
                AccountTypeId = accountDto.AccountType.Id,
                AccountName = accountDto.AccountName
            };

            var user = await _accountRepository.GetUserByIdAsync(accountDto.AppUserId);

            user.Accounts.Add(account);


            if (await _accountRepository.SaveAllAsync())
            {
                return await GetAccountsByUserId(accountDto.AppUserId);
            }
            return BadRequest("Problem adding account");
        }
        [HttpPut("update")]
        public async Task<ActionResult<IEnumerable<AccountDto>>> UpdateAccount(AccountDto accountDto)
        {
            var user = await _accountRepository.GetUserByIdAsync(accountDto.AppUserId);

            var account = user.Accounts.SingleOrDefault(x => x.Id == accountDto.Id);

            if (account == null) return NotFound();

            account.AccountName = accountDto.AccountName;
            account.AccountTypeId = accountDto.AccountType.Id;

            _accountRepository.Update(account);

            if (await _accountRepository.SaveAllAsync())
            {
                return await GetAccountsByUserId(account.AppUserId);
            }
            return BadRequest("Problem updating account");
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            var user = await _accountRepository.GetUserByIdAsync(User.GetUserId());

            var account = user.Accounts.SingleOrDefault(x => x.Id == id);

            if (account == null) return NotFound();

            user.Accounts.Remove(account);

            if (await _accountRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete the account");

        }

        private async Task<bool> AccountExists(string AccountName, int AppUserId)
        {
            var user = await _accountRepository.GetUserByIdAsync(AppUserId);

            return user.Accounts.Any(x => x.AccountName == AccountName);
        }
    }
}
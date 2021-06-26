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
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public UsersController(DataContext context, IUserRepository userRepository, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<AccountDto>>> GetAccountsByUserId(int id)
        {
            return await _userRepository.GetAccountsByUserIdAsync(id);
        }

        [HttpPost("saveAccount")]
        public async Task<ActionResult<List<AccountDto>>> SaveAccount(AccountDto accountDto)
        {
            string accountName = accountDto.AccountName.ToLower();
            if (await UserExists(accountName, accountDto.AppUserId)) return BadRequest("Account already exists");

            var account = new Account
            {
                AccountType = accountDto.AccountType,
                AccountName = accountDto.AccountName
            };

            var user = await _context.Users
            .Include(a => a.Accounts)
            .SingleOrDefaultAsync(x => x.Id == accountDto.AppUserId);

            user.Accounts.Add(account);

            if (await _userRepository.SaveAllAsync())
            {
                return await _userRepository.GetAccountsByUserIdAsync(accountDto.AppUserId);
            }
            return BadRequest("Problem adding account");
        }

        private async Task<bool> UserExists(string accountName, int appuserid)
        {
            return await _context.Accounts.Where(x => x.AppUserId == appuserid)
            .AnyAsync(x => x.AccountName.ToLower() == accountName);
        }
    }
}
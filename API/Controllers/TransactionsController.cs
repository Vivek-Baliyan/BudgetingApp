using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TransactionsController : BaseApiController
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionsController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet("{appUserId}")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactionsAsync(int appUserId)
        {
            var transactions = await _transactionRepository.GetTransactionsAsync(appUserId);
            return Ok(transactions);
        }

        [HttpPost("save")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> SaveTransaction(TransactionDto transactionDto)
        {
            var account = await _transactionRepository.GetTransactionsByAccountIdAsync(transactionDto.AccountId);

            var transaction = new AccountTransaction
            {
                Date = System.DateTime.Now,
                Payee = transactionDto.Payee,
                Memo = transactionDto.Memo,
                CreditAmount = transactionDto.CreditAmount,
                DebitAmount = transactionDto.DebitAmount,
                SubCategoryId = transactionDto.SubCategoryId
            };

            account.Transactions.Add(transaction);
            if (await _transactionRepository.SaveAllAsync())
            {
                var transactions = await _transactionRepository.GetTransactionsAsync(account.AppUserId);
                return Ok(transactions);
            }
            return BadRequest("Problem adding account");
        }
    }
}
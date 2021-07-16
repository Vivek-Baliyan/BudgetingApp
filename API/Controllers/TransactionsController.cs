using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
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

        // [HttpGet("{id}")]
        // public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactionsByAccountIdAsync(int id)
        // {
        //     var transactions = await _transactionRepository.GetTransactionsByAccountIdAsync(id);
        //     return Ok(transactions);
        // }
    }
}
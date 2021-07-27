using System;

namespace API.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Payee { get; set; }
        public string Memo { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string MasterCategoryName { get; set; }

    }
}
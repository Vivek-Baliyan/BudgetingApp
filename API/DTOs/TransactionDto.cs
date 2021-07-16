namespace API.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Payee { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
    }
}
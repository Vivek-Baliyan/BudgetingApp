using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Transactions")]
    public class AccountTransaction
    {
        public int Id { get; set; }
        public string Payee { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }   
        public Account Account { get; set; }
        public int AccountId { get; set; }
        
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Accounts")]
    public class Account
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public AppUser AppUser { get; set; }
        public AccountType AccountType { get; set; }
        public int AppUserId { get; set; }
        public int AccountTypeId { get; set; }
    }
}
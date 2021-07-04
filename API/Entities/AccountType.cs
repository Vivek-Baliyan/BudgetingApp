using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("AccountTypes")]
    public class AccountType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
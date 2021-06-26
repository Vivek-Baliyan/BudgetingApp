namespace API.DTOs
{
    public class AccountDto
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string AccountType { get; set; }
        public string AccountName { get; set; }
    }
}
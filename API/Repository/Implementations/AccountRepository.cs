using API.Data;
using API.Entities;
using API.Repository.Interfaces;

namespace API.Repository.Implementations
{
    public class AccountRepository(DataContext db) : Repository<Account>(db), IAccountRepository
    {
        private readonly DataContext _db = db;

        public void Update(Account account)
        {
            _db.Update(account);
        }
    }
}
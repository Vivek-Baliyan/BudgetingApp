using System.Threading.Tasks;
using API.Data;
using API.Repository.Interfaces;

namespace API.Repository.Implementations
{
    public class UnitOfWork
    {
        private readonly DataContext _db;

        public UnitOfWork(DataContext db)
        {
            _db = db;
            AccountRepository = new AccountRepository(_db);
        }

        public IAccountRepository AccountRepository { get; private set; }

        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
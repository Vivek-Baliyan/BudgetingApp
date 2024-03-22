using API.Entities;

namespace API.Repository.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        void Update(Account account);
    }
}
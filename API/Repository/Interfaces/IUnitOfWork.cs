namespace API.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
    }
}
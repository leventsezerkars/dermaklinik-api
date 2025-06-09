namespace DermaKlinik.API.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
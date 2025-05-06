using System;
using System.Threading.Tasks;
using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPatientRepository Patients { get; }
        IGenericRepository<User> Users { get; }
        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
} 
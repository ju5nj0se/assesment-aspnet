using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace JuanJoseHernandez.Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        IDbTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        void Commit();
        void Rollback();
    }
}
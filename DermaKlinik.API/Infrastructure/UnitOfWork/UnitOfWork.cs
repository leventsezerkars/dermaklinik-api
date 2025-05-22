using System;
using System.Threading.Tasks;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Infrastructure.Data;
using DermaKlinik.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using DermaKlinik.API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Infrastructure.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;
        private IDbContextTransaction? _transaction;
        private bool _disposed;

        public UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Veri güncellenirken eşzamanlılık hatası oluştu.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Veritabanı güncelleme hatası oluştu.", ex);
            }
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("Zaten aktif bir transaction bulunmaktadır.");
            }
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                if (_transaction == null)
                {
                    throw new InvalidOperationException("Aktif bir transaction bulunmamaktadır.");
                }

                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Aktif bir transaction bulunmamaktadır.");
            }

            await _transaction.RollbackAsync();
            _transaction.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
                _transaction?.Dispose();
            }
            _disposed = true;
        }
    }
} 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Database
{
    public class UniteOfWork : IUniteOfWork
    {
        private readonly StoreContext _context;
        private Hashtable _repositories;
        public UniteOfWork(StoreContext context)
        {
            _context = context;
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) { _repositories = new Hashtable(); }

            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repoType = typeof(GenericRepository<>);
                var repoInstance = Activator.CreateInstance(repoType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repoInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type]; //return the repo instance from the hashtable
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Repositories;
using TalabatG08.Repository.Data;

namespace TalabatG08.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext dbContext;

        private readonly Hashtable _repositories;

        public UnitOfWork(StoreContext dbContext)
        {
            this.dbContext = dbContext;
            _repositories = new Hashtable();
        }

        public async Task<int> CompleteAsync()
        {
           return await dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
           await dbContext.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var Repository = new GenericRepository<TEntity>(dbContext);
                _repositories.Add(type, Repository);
            }

           //var Repository = new GenericRepository<TEntity>(dbContext);
            return _repositories[type] as IGenericRepository<TEntity>;  
        }
    }
}

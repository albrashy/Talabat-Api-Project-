using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Repositories;

namespace TalabatG08.Core
{
    public interface IUnitOfWork :IAsyncDisposable
    {
        Task<int> CompleteAsync();

        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

    }
}

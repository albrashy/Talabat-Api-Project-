using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Specifications;

namespace TalabatG08.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
       Task<IReadOnlyList<T>> GetAllAsync();    

        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

        Task<T> GetByEntityWithSpecAsync(ISpecifications<T> spec);

        Task<int> GetCountWithSpecAsync(ISpecifications<T> spec);

        Task Add(T item);

        void Delete(T item);    
        void Update(T item);    



    }
}

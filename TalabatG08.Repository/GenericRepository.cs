using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Repositories;
using TalabatG08.Core.Specifications;
using TalabatG08.Repository.Data;

namespace TalabatG08.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if(typeof(T) == typeof(Product)) // Sepecification patttern
            //{
            //   return (IEnumerable<T>) await dbContext.Products.Include(p=>p.productBrand).Include(p=>p.productType).ToListAsync();  
            //}

            return await dbContext.Set<T>().ToListAsync();
        }

      

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);

        }


        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();    
        }

        public async Task<T> GetByEntityWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }


        private IQueryable<T> ApplySpecification (ISpecifications<T> spec)
        {
           
           return SpecificationEvalutor<T>.GetQuery(dbContext.Set<T>(),spec);
            
        }

        public async Task Add(T item)
        {
            await dbContext.Set<T>().AddAsync(item);   
        }

        public void Delete(T item)
        {
           dbContext.Set<T>().Remove(item);
        }

        public void Update(T item)
        {
            dbContext.Set<T>().Update(item);    
        }
    }
}

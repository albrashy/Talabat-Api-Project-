using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Specifications;

namespace TalabatG08.Repository
{
    public class SpecificationEvalutor<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery ,ISpecifications<T> spec )
        {
            var query = inputQuery; //context.products

            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.Orderby is not null)
            {
                query = query.OrderBy(spec.Orderby);
            }

            if (spec.OrderbyDecending is not null)
            {
                query = query.OrderByDescending(spec.OrderbyDecending);
            }

          if(spec.IsPaginationEnable)
             query = query.Skip(spec.Skip).Take(spec.Take);  
          




            query = spec.Includes.Aggregate(query, (CurrentQuer, IncludeExpression) => CurrentQuer.Include(IncludeExpression));



            return query;   

        }


    }
}

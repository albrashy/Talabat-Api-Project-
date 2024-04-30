using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;

namespace TalabatG08.Core.Specifications
{
    public class BaseSpecification<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set; }
        public List<Expression<Func<T, object>>> Includes  { get ; set; } =new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> Orderby { get; set; }
        public Expression<Func<T, object>> OrderbyDecending { get ; set ; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnable { get ; set; }

        public BaseSpecification()
        {
            
        }

        public BaseSpecification(Expression<Func<T,bool>> Criteria)
        {
            this.Criteria = Criteria;   
        }

        public void AddOrderby(Expression<Func<T, object>> Orderby)
        {
            this.Orderby = Orderby;
        }

        public void AddOrderbyDesend(Expression<Func<T, object>> OrderbyDecending)
        {
            this.OrderbyDecending = OrderbyDecending;
        }

        public void Applypagination(int  Skip, int Take)
        {
            IsPaginationEnable = true;
            this.Skip = Skip;
            this.Take = Take;   
        }

    }
}

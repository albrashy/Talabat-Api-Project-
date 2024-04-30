using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;

namespace TalabatG08.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }  //sigture for property

        public List<Expression<Func<T,object>>> Includes { get; set; } 

        public Expression<Func<T,object>> Orderby { get; set; }

        public Expression<Func<T, object>> OrderbyDecending { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public bool IsPaginationEnable { get; set; }    

    }
}

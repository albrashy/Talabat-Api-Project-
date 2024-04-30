using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites.Order_Aggregate;

namespace TalabatG08.Core.Specifications.OrderSpec
{
    public class OrderSpecifications:BaseSpecification<Order>
    {
        public OrderSpecifications(string buyerEmail):base(O=>O.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
            AddOrderbyDesend(o => o.OrderDate);

        }

        public OrderSpecifications(int OrderId, string buyerEmail) : base(O => O.BuyerEmail == buyerEmail && O.Id == OrderId)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
            //AddOrderbyDesend(o => o.OrderDate);

        }
    }
}

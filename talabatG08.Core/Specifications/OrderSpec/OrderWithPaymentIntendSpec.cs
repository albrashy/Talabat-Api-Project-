using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Entites.Order_Aggregate;

namespace TalabatG08.Core.Specifications.OrderSpec
{
    public class OrderWithPaymentIntendSpec:BaseSpecification<Order>
    {
        public OrderWithPaymentIntendSpec(string PaymentintentdId):base(O=>O.PaymentIntendId == PaymentintentdId)
        {
            
        }
    }

}

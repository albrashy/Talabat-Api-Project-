using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Entites.Order_Aggregate;

namespace TalabatG08.Core.Services
{
    public interface IPaymentService
    {
        // fun to create or update payment intent
        Task<CustomerBasket?> CreateOUpdatePaymentIntend(string BasketId);

        Task<Order> UpdatePaymentIntendToSucceedOrFailed(string PaymentIntendId, bool flag);

    }
}

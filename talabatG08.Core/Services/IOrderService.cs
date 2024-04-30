using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites.Order_Aggregate;

namespace TalabatG08.Core.Services
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress);

        Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail);
        Task<Order> GetOrderByIdForSpecificUserAsync(string BuyerEmail,int OrdersId);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsService();
    }
}

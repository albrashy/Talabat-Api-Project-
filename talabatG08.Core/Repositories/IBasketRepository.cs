using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;

namespace TalabatG08.Core.Repositories
{
    public interface IBasketRepository
    {
         Task<CustomerBasket> GetBasketAsync(string BasketId);
         Task<CustomerBasket> UpadteBasketAsync(CustomerBasket Basket);

         Task<bool> DeleteBasketAsync(string BasketId);



    }
}

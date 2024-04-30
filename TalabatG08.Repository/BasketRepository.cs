using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Repositories;

namespace TalabatG08.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            this._database =redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);    
        }

        public async Task<CustomerBasket> GetBasketAsync(string BasketId)
        {
            var basket = await _database.StringGetAsync(BasketId);
            return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpadteBasketAsync(CustomerBasket Basket)
        {
            var CreatedOrUpdated = await _database.StringSetAsync(Basket.Id ,JsonSerializer.Serialize(Basket), TimeSpan.FromDays(1));
            if (!CreatedOrUpdated) return null;

            return await GetBasketAsync(Basket.Id);


        }
    }
}

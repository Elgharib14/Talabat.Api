using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entityes;
using Talabat.Core.Repository;

namespace Talabat.Repository
{
    public class BasketRepo : IBasketRep
    {
        private readonly IDatabase database;

        public BasketRepo(IConnectionMultiplexer redis)
        {
            database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasket(string basketId)
        {
           return await database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasket(string basketId)
        {
            var basket = await database.StringGetAsync(basketId);
            return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasket(CustomerBasket basket)
        {
          var Creatorupdate = await database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(1));

            if (!Creatorupdate) return null!;

            return await GetBasket(basketId: basket  .Id);
        }
    }
}

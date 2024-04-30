using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Entites.Order_Aggregate;

namespace TalabatG08.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext)
        {

            if(!dbContext.ProductBrands.Any()) 
            {
                var brandsData = File.ReadAllText("../TalabatG08.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if(brands?.Count >0 )
                {
                    foreach(var brand in brands )
                    {
                        await dbContext.ProductBrands.AddAsync(brand);
                       
                    }
                    await dbContext.SaveChangesAsync(); 
                }
            }


            if (!dbContext.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../TalabatG08.Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types?.Count > 0)
                {
                    foreach (var type in types)
                    {
                        await dbContext.ProductTypes.AddAsync(type);

                    }
                    await dbContext.SaveChangesAsync();
                }
            }


            if (!dbContext.Products.Any())
            {
                var ProductsData = File.ReadAllText("../TalabatG08.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (Products?.Count > 0)
                {
                    foreach (var Product in Products)
                    {
                        await dbContext.AddAsync(Product);

                    }
                    await dbContext.SaveChangesAsync();
                }
            }


            if (!dbContext.DeliveryMethods.Any())
            {
                var DeliveryData = File.ReadAllText("../TalabatG08.Repository/Data/DataSeed/delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);
                if (DeliveryMethods?.Count > 0)
                {
                    foreach (var deliveryMethd in DeliveryMethods)
                    {
                        await dbContext.Set<DeliveryMethod>().AddAsync(deliveryMethd);

                    }
                    await dbContext.SaveChangesAsync();
                }
            }


        }
    }
}

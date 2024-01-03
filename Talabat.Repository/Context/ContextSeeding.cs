using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entityes;
using Talabat.Core.Entityes.OrderAggregate;

namespace Talabat.Repository.Context
{
    public static class ContextSeeding
    {
        public static async Task SeedingAsync(AppDbcontext dbcontext)
        {
            if (!dbcontext.productBrands.Any())
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/Context/DataSeeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                //


                if (brands?.Count > 0)
                {
                    //// this brands2 i do it to tack the Name from jason file beacuse the Id in Table is identity (but i Solve it in json File) 
                    //var brands2 = brands.Select(a => new ProductBrand()
                    //{
                    //    Name = a.Name,
                    //});
                    foreach (var brand in brands)
                    {
                        await dbcontext.Set<ProductBrand>().AddAsync(brand);

                    }
                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.productTypes.Any())
            {
                var TybesData = File.ReadAllText("../Talabat.Repository/Context/DataSeeding/types.json");
                var Tybes = JsonSerializer.Deserialize<List<ProductType>>(TybesData);

                if (Tybes?.Count > 0)
                {
                    foreach (var tybe in Tybes)
                    {
                        await dbcontext.Set<ProductType>().AddAsync(tybe);

                    }
                    await dbcontext.SaveChangesAsync();
                }




            }

            if (!dbcontext.products.Any())
            {
                var ProductsData = File.ReadAllText("../Talabat.Repository/Context/DataSeeding/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (products?.Count > 0)
                {
                    foreach (var Product in products)
                    {
                        await dbcontext.Set<Product>().AddAsync(Product);

                    }
                    await dbcontext.SaveChangesAsync();
                }




            }

            if (!dbcontext.DeleveryMethods.Any())
            {
                var DeliveryData = File.ReadAllText("../Talabat.Repository/Context/DataSeeding/delivery.json");
                var Deliverys = JsonSerializer.Deserialize<List<DeleveryMethod>>(DeliveryData);

                if (Deliverys?.Count > 0)
                {
                    foreach (var Delivery in Deliverys)
                    {
                        await dbcontext.Set<DeleveryMethod>().AddAsync(Delivery);

                    }
                    await dbcontext.SaveChangesAsync();
                }




            }
        }
    }
}

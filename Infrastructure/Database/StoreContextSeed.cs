using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace Infrastructure.Database
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            Console.WriteLine("Went into Seed Async method");
            context.Products.RemoveRange(context.Products.ToList());
            context.ProductTypes.RemoveRange(context.ProductTypes.ToList());
            context.ProductBrands.RemoveRange(context.ProductBrands.ToList());
            context.DeliveryMethods.RemoveRange(context.DeliveryMethods.ToList());
            context.SaveChanges();
            Console.WriteLine(context.Products.Count());
            if (!context.ProductBrands.Any())
            {
                Console.WriteLine("Went into ProductBrands");
                var brandsData = File.ReadAllText("../Infrastructure/Database/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                context.ProductBrands.AddRange(brands);
            }

            if (!context.ProductTypes.Any())
            {
                Console.WriteLine("Went into ProductTypes");

                var typesData = File.ReadAllText("../Infrastructure/Database/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
            }

            if (!context.Products.Any())
            {
                Console.WriteLine("Went into Products");
                var productsData = File.ReadAllText("../Infrastructure/Database/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }

            if (context.ChangeTracker.HasChanges())
            {
                Console.WriteLine("******* Changes are detected ******");
                try
                {
                    await context.SaveChangesAsync();
                    Console.WriteLine("******* Saved changes ******");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("***************************");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("***************************");
                }

            }
            else
            {
                Console.WriteLine("No changes were detected");
            }

            if (!context.DeliveryMethods.Any())
            {
                Console.WriteLine("Went into Delivery Methods");
                var deliveryData = File.ReadAllText("../Infrastructure/Database/SeedData/delivery.json");
                var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                context.DeliveryMethods.AddRange(delivery);
            }
        }
    }
}
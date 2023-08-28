using Project_Entity.Context;
using Project_Entity.Models;
using System.Text.Json;

namespace R53_GroupB_GadgetPoint.Context
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
			try
			{
				if (!context.Brands.Any())
				{
					var brandData = File.ReadAllText("SeedData/brands.json");
					var brands = JsonSerializer.Deserialize<List<Brand>>(brandData);

					foreach ( var brand in brands )
					{
						context.Brands.Add(brand);
					}
					await context.SaveChangesAsync();
				}

                if (!context.Categories.Any())
                {
                    var categoryData = File.ReadAllText("SeedData/category.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);

                    foreach (var category in categories)
                    {
                        context.Categories.Add(category);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.SubCategories.Any())
                {
                    var subcategoryData = File.ReadAllText("SeedData/subcategory.json");
                    var subcategories = JsonSerializer.Deserialize<List<SubCategory>>(subcategoryData);

                    foreach (var subcategory in subcategories)
                    {
                        context.SubCategories.Add(subcategory);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productData = File.ReadAllText("SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);

                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                    }
                    context.SaveChanges();
                }

            }
            catch (JsonException ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, "JSON deserialization error: " + ex.Message);
            }

        }
    }
}

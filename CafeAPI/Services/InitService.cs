using CafeAPI.Models;
namespace CafeAPI.Services
{
    public static class InitService
    {
        public static List<Category> InitializeCategories()
        {
            List<Category> categories = new();
            categories.Add(new Category
            {
                Id = 1,
                Name = "Coffee"
            });

            categories.Add(new Category
            {
                Id = 2,
                Name = "Tea"
            });

            categories.Add(new Category
            {
                Id = 3,
                Name = "Bakeries"
            });
            return categories; 
        }

        public static List<Product> InitializeProducts()
        {
            List<Product> products = new();
            products.Add(new Product
            {
                Id = 1,
                Name = "Espresso",
                Price = 25,
                CategoryId = 1,
            });

            products.Add(new Product
            {
                Id = 2,
                Name = "Cappuccino",
                Price = 35,
                CategoryId = 1,
            });

            products.Add(new Product
            {
                Id = 3,
                Name = "Latte",
                Price = 40,
                CategoryId = 1,
            });

            products.Add(new Product
            {
                Id = 4,
                Name = "Earl Grey Tea",
                Price = 30,
                CategoryId = 2,
            });

            products.Add(new Product
            {
                Id = 5,
                Name = "Green Tea",
                Price = 30,
                CategoryId = 2,
            });

            products.Add(new Product
            {
                Id = 6,
                Name = "Cinnamon Bun",
                Price = 25,
                CategoryId = 3,
            });

            products.Add(new Product
            {
                Id = 7,
                Name = "Croissant",
                Price = 28,
                CategoryId = 3,
            });

            products.Add(new Product
            {
                Id = 8,
                Name = "Americano",
                Price = 30,
                CategoryId = 1,
            });
            return products;
        }

    }
}

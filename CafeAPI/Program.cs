using CafeAPI.Data;
using CafeAPI.Models;
using CafeAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CafeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<CafeApiDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();

            List<Category> categories = InitService.InitializeCategories();

            List<Product> products = InitService.InitializeProducts();

            app.MapGet("/products", () =>
            {
                
                return products;
            });

            app.MapPost("/products", (Product product) =>
            {
                product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
                products.Add(product);
                return product;
            });

        }
    }
}

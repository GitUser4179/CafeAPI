using CafeAPI.Data;
using CafeAPI.Models;
using CafeAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;

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

            List<Category> categories = InitService.InitializeCategories();

            List<Product> products = InitService.InitializeProducts();

            app.MapGet("/products", (CafeApiDbContext db) =>
            {
                var retrievedProducts = db.Products.ToList();
                return retrievedProducts;
            });

            app.MapPost("/products", (Product product, CafeApiDbContext db) =>
            {
                product.Id = db.Products.Any() ? db.Products.Max(p => p.Id) + 1 : 1;

                db.Products.Add(product);
                db.SaveChanges();
                return product;
            });

            app.MapGet("/products/{id}", (int id, Product product, CafeApiDbContext db) =>
            {
                var retrievedProduct = db.Products.FirstOrDefault(p => p.Id == id);

                return product;
            });

            app.MapPut("/products/{id}", (int id, Product selectedProduct, CafeApiDbContext db) =>
            {
                var fetchedProduct = db.Products.FirstOrDefault(p => p.Id == id);

                fetchedProduct.Name = selectedProduct.Name;
                fetchedProduct.Price = selectedProduct.Price;
                fetchedProduct.Category = selectedProduct.Category;

                db.Products.Update(selectedProduct);
                db.SaveChanges();
                return selectedProduct;
            });

            app.MapDelete("/products/{id}", (int id, CafeApiDbContext db) =>
            {
                var prod = db.Products.FirstOrDefault(p => p.Id == id);

                db.Products.Remove(prod);
                db.SaveChanges();
                return products;
            });
            // Categories

            app.MapGet("/categories", (CafeApiDbContext db) =>
            {
                var fethcedCategories = db.Categories.ToList();

                return fethcedCategories;
            });

            app.MapPost("/categories", (Category category, CafeApiDbContext db) =>
            {
                category.Id = db.Categories.Any() ? db.Categories.Max(p => p.Id) + 1 : 1;

                db.Categories.Add(category);
                return category;
            });

            app.MapGet("/categories/{id}", (int id, CafeApiDbContext db) =>
            {
                var category = db.Categories.FirstOrDefault(p => p.Id == id);
                return category;
            });

            app.MapPut("/categories/{id}", (int id, Category selectedCategory, CafeApiDbContext db) =>
            {
                var fetchedCategory = db.Categories.FirstOrDefault(p => p.Id == id);
                fetchedCategory.Name = selectedCategory.Name;


                db.Categories.Update(selectedCategory);
                db.SaveChanges();

                return fetchedCategory;
            });

            app.MapGet("/categories/{id}/products", (int id, CafeApiDbContext db) =>
            {
                var category = db.Categories.FirstOrDefault(p => p.Id == id);
                var product = db.Products.Where(p => p.CategoryId == id).ToList();


                return product;
            });

            app.MapDelete("/categories/{id}", (int id, CafeApiDbContext db) =>
            {
                var selectedCategories = categories.FirstOrDefault(p => p.Id == id);


                categories.Remove(selectedCategories);
                return categories;
            });

            app.Run();
        }
    }
}

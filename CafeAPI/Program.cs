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

            app.MapGet("/products/{id}", (int id) =>
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                return product;
            });

            app.MapPut("/products/{id}", (int id, Product inputTodo, CafeApiDbContext db) =>
            {
                var todo = products.FirstOrDefault(p => p.Id == id);
                    
                todo.Name = inputTodo.Name;
                todo.Price = inputTodo.Price;
                todo.Category = inputTodo.Category;

                return todo;
            });

            app.MapDelete("/products/{id}", (int id) =>
            {
                products.Remove(products.FirstOrDefault(p => p.Id == id));
                return products;
            });
            // Categories

            app.MapGet("/categories", () =>
            {
                return categories;
            });

            app.MapPost("/categories", (Category category) =>
            {
                category.Id = categories.Any() ? categories.Max(p => p.Id) + 1 : 1;
                categories.Add(category);
                return category;
            });

            app.MapGet("/categories/{id}", (int id) =>
            {
                var category = categories.FirstOrDefault(p => p.Id == id);
                return category;
            });

            app.MapPut("/categories/{id}", (int id, Category inputTodo, CafeApiDbContext db) =>
            {
                var todo = categories.FirstOrDefault(p => p.Id == id);

                todo.Name = inputTodo.Name;

                return todo;
            });

            app.MapGet("/categories/{id}/products", (int id) =>
            {
                var category = categories.FirstOrDefault(p => p.Id == id);
                var product = products.Where(p => p.CategoryId == id).ToList();
                return product;
            });

            app.MapDelete("/categories/{id}", (int id) =>
            {
                categories.Remove(categories.FirstOrDefault(p => p.Id == id));
                return categories;
            });

            app.Run();
        }
    }
}

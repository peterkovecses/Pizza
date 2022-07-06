using Microsoft.EntityFrameworkCore;
using Pizza.Data.Entities;

namespace Pizza.Data.Seed
{
    internal class TestDataConfiguration
    {
        public static void ConfigureSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(                
                WithAudit(new Category { Id = 1, Name = "Chicago Deep Dish Pizza" }),
                WithAudit(new Category { Id = 2, Name = "Chicago Thin Crust Pizza" }),
                WithAudit(new Category { Id = 3, Name = "Beer" }),
                WithAudit(new Category { Id = 4, Name = "Wine" })
                );

            modelBuilder.Entity<Product>().HasData(
              WithAudit(new Product { Id = 1, Name = "Chicago Meat Market", Description = "Layers of sausage, meatballs, pepperoni, freshly shredded mozzarella, chunky tomato sauce, and pecorino romano.", PhotoPath = "ProductImages/1.jpg", Price = 19, CategoryId = Category.ChicagoDeepDishPizza}),
              WithAudit(new Product { Id = 2, Name = "Chicago Classic", Description = "Extra sausage, extra cheese, extra good in your mouth.", PhotoPath = "ProductImages/2.jpg", Price = 15, CategoryId = Category.ChicagoDeepDishPizza }),
              WithAudit(new Product { Id = 3, Name = "Prima Pepperoni", Description = "Pepperoni with Uno's own chunky vine-ripened tomato sauce, mozzarella and imported pecorino romano.", PhotoPath = "ProductImages/3.jpg", Price = 15, CategoryId = Category.ChicagoDeepDishPizza }),
              WithAudit(new Product { Id = 4, Name = "Cheese & Tomato", Description = "Just what it sounds like.", PhotoPath = "ProductImages/4.jpg", Price = 12, CategoryId = Category.ChicagoDeepDishPizza }),
              WithAudit(new Product { Id = 5, Name = "Meatball & Ricotta", Description = "Italian style beef and pork meatballs, ricotta cheese, fresh mushrooms, freshly shredded mozzarella, housemade marinara, and pecornio romano. Molto bene!", PhotoPath = "ProductImages/5.jpg", Price = 17, CategoryId = Category.ChicagoDeepDishPizza }),
              WithAudit(new Product { Id = 6, Name = "Four Cheese & Pesto", Description = "Mozzarella, feta, cheddar, romano, and basil pesto sauce topped with diced tomatoes.", PhotoPath = "ProductImages/6.jpg", Price = 15, CategoryId = Category.ChicagoDeepDishPizza }),
              WithAudit(new Product { Id = 7, Name = "Farmer's Market", Description = "Caramelized onions, fresh spinach, sun-dried and plum tomatoes, roasted eggplant, pesto, feta, mozzarella and imported pecorino romano.", PhotoPath = "ProductImages/7.jpg", Price = 15, CategoryId = Category.ChicagoDeepDishPizza }),
              WithAudit(new Product { Id = 8, Name = "New York Deli", Description = "The best of both worlds - an Italian sub in our deep dish crust! Filled with mozzarella, provolone, ham, pepperoni, onions, plum tomatoes and banana peppers, sprinkled with oregano and romano. Served with a side of our house vinaigrette.", PhotoPath = "ProductImages/8.jpg", Price = 17, CategoryId = Category.ChicagoDeepDishPizza }),
              WithAudit(new Product { Id = 9, Name = "BBQ Chicken", Description = "Grilled chicken breast, red onions, mozzarella, aged cheddar and pecorino romano atop a layer of BBQ sauce. With a drizzle of even more BBQ sauce.", PhotoPath = "ProductImages/9.jpg", Price = 14, CategoryId = Category.ChicagoThinCrustPizza }),
              WithAudit(new Product { Id = 10, Name = "Margherita", Description = "Fresh mozzarella and basil with our housemade pizza sauce.", PhotoPath = "ProductImages/10.jpg", Price = 11, CategoryId = Category.ChicagoThinCrustPizza }),
              WithAudit(new Product { Id = 11, Name = "Spicy Hawaiian", Description = "Say aloha to this hot mama of a Hawaiian. We start with a layer of sweet red chili sauce, then add ham, fresh pineapple chunks, jalapeños, mozzarella, aged cheddar, and pecorino romano and finish it with a drizzle of ranch.", PhotoPath = "ProductImages/11.jpg", Price = 14, CategoryId = Category.ChicagoThinCrustPizza }),
              WithAudit(new Product { Id = 12, Name = "Bianco Love", Description = "If you're a fan of cheese and garlic (and really, who isn't?), you'll love this white pizza with creamy ricotta, aged cheddar, mozzarella, romano, extra virgin olive oil, and seasoned garlic.", PhotoPath = "ProductImages/12.jpg", Price = 14, CategoryId = Category.ChicagoThinCrustPizza }),
              WithAudit(new Product { Id = 13, Name = "Taco", Description = "Our crispy cauliflower crust topped with jalapenos, salsa, spiced beef, grated mozzarella and cheddar cheeses. Baked to perfection and topped with chopped lettuce and tomatoes. Served with sour cream and salsa.", PhotoPath = "ProductImages/13.jpg", Price = 16, CategoryId = Category.ChicagoThinCrustPizza }),
              WithAudit(new Product { Id = 14, Name = "Easy Street", Description = "Light and refreshing, Easy Street Wheat is an unfiltered American-style wheat beer. Leaving in the yeast gives the beer a nice, smooth finish and a slightly citrusy flavor.", PhotoPath = "ProductImages/14.jpg", Price = 5, CategoryId = Category.Beer }),
              WithAudit(new Product { Id = 15, Name = "Belgian White", Description = "Blue Moon Belgian White, Belgian-style wheat ale, is a refreshing, medium-bodied, unfiltered Belgian-style wheat ale spiced with fresh coriander and orange peel for a uniquely complex taste and an uncommonly smooth finish.", PhotoPath = "ProductImages/15.jpg", Price = 5, CategoryId = Category.Beer }),
              WithAudit(new Product { Id = 16, Name = "Summer Shandy", Description = "A shandy is beer mixed with a little something extra like soda, juice or ginger ale. Leinenkugel’s® Summer Shandy® is a traditional wheat beer with refreshing natural lemonade flavor that makes it perfect for the sun-splashed summer months. For a fun and fresh food pairing, try it with lighter, brighter foods like shrimp quesadillas, pan-seared scallops with lemon vinaigrette, or lemon chicken with pasta.", PhotoPath = "ProductImages/16.jpg", Price = 5, CategoryId = Category.Beer }),
              WithAudit(new Product { Id = 17, Name = "Mark West Pinot Noir", Description = "California", PhotoPath = "ProductImages/17.jpg", Price = 30, CategoryId = Category.Wine }),
              WithAudit(new Product { Id = 18, Name = "Trinity Oaks Merlot", Description = "California", PhotoPath = "ProductImages/18.jpg", Price = 25, CategoryId = Category.Wine }),
              WithAudit(new Product { Id = 19, Name = "Canyon Road Cabernet", Description = "California", PhotoPath = "ProductImages/19.jpg", Price = 35, CategoryId = Category.Wine })
              );

            modelBuilder.Entity<Order>().HasData(
                WithAudit(new Order { Id = 1, Date = new(2020, 01, 01), CustomerId = 1}),
                WithAudit(new Order { Id = 2, Date = new(2020, 01, 01), CustomerId = 2})
                );

            modelBuilder.Entity<ProductOrder>().HasData(
                WithAudit(new ProductOrder { Id = 1, ProductId = 1, OrderId = 1 }),
                WithAudit(new ProductOrder { Id = 2, ProductId = 1, OrderId = 1 }),
                WithAudit(new ProductOrder { Id = 3, ProductId = 2, OrderId = 2 }),
                WithAudit(new ProductOrder { Id = 4, ProductId = 9, OrderId = 2 }),
                WithAudit(new ProductOrder { Id = 5, ProductId = 14, OrderId = 2 })
                );

            modelBuilder.Entity<Customer>().HasData(
                WithAudit(new Customer { Id = 1, Name = "Tim", Address = "9012 Győr, Vadvirág utca 1.", PhoneNumber = "+367089712234", Email = "tim@gmail.com"}),
                WithAudit(new Customer { Id = 2, Name = "Tom", Address = "9024 Győr, Ikva utca 14. 1/1", PhoneNumber = "+367089712234", Email = "tom@gmail.com" }),
                WithAudit(new Customer { Id = 3, Name = "Tod", Address = "9022 Győr, Szent István utca 37. 1/2", PhoneNumber = "+367089712234", Email = "tod@gmail.com" }),
                WithAudit(new Customer { Id = 4, Name = "Toby", Address = "9028 Győr, József Attila utca 112.", PhoneNumber = "+367089712234", Email = "toby@gmail.com" }),
                WithAudit(new Customer { Id = 5, Name = "Jane", Address = "9023 Győr, KOdály Zoltán utca 28. 3/4.", PhoneNumber = "+367089712234", Email = "jane@gmail.com" })
                );

            T WithAudit<T>(T entity) where T : EntityBase
            {
                entity.DateOfCreation = entity.DateOfUpdate = new(2020, 01, 01);
                return entity;
            }
        }        
    }
}

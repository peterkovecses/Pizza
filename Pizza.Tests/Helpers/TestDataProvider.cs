﻿using Pizza.Bll.Dtos;
using Pizza.Data.Entities;

namespace Pizza.Tests.Helpers
{
    internal class TestDataProvider
    {
        public static IQueryable<Product> GetTestDataForServiceTests()
        {
            return new List<Product>
                    {
                      new Product { Id = 1, Name = "Chicago Meat Market", Description = "Layers of sausage, meatballs, pepperoni, freshly shredded mozzarella, chunky tomato sauce, and pecorino romano.", PhotoPath = "productImages/1.jpg", Price = 19, CategoryId = Category.ChicagoDeepDishPizza},
                      new Product { Id = 2, Name = "Chicago Classic", Description = "Extra sausage, extra cheese, extra good in your mouth.", PhotoPath = "productImages/2.jpg", Price = 15, CategoryId = Category.ChicagoDeepDishPizza },
                      new Product { Id = 3, Name = "Prima Pepperoni", Description = "Pepperoni with Uno's own chunky vine-ripened tomato sauce, mozzarella and imported pecorino romano.", PhotoPath = "productImages/3.jpg", Price = 15, CategoryId = Category.ChicagoDeepDishPizza },
                      new Product { Id = 4, Name = "Cheese & Tomato", Description = "Just what it sounds like.", PhotoPath = "productImages/4.jpg", Price = 12, CategoryId = Category.ChicagoDeepDishPizza },
                      new Product { Id = 5, Name = "Meatball & Ricotta", Description = "Italian style beef and pork meatballs, ricotta cheese, fresh mushrooms, freshly shredded mozzarella, housemade marinara, and pecornio romano. Molto bene!", PhotoPath = "productImages/5.jpg", Price = 17, CategoryId = Category.ChicagoDeepDishPizza },
                      new Product { Id = 6, Name = "Four Cheese & Pesto", Description = "Mozzarella, feta, cheddar, romano, and basil pesto sauce topped with diced tomatoes.", PhotoPath = "pizzaImages/6.jpg", Price = 15, CategoryId = Category.ChicagoDeepDishPizza },
                      new Product { Id = 7, Name = "Farmer's Market", Description = "Caramelized onions, fresh spinach, sun-dried and plum tomatoes, roasted eggplant, pesto, feta, mozzarella and imported pecorino romano.", PhotoPath = "productImages/7.jpg", Price = 15, CategoryId = Category.ChicagoDeepDishPizza },
                      new Product { Id = 8, Name = "New York Deli", Description = "The best of both worlds - an Italian sub in our deep dish crust! Filled with mozzarella, provolone, ham, pepperoni, onions, plum tomatoes and banana peppers, sprinkled with oregano and romano. Served with a side of our house vinaigrette.", PhotoPath = "productImages/8.jpg", Price = 17, CategoryId = Category.ChicagoDeepDishPizza },
                      new Product { Id = 9, Name = "BBQ Chicken", Description = "Grilled chicken breast, red onions, mozzarella, aged cheddar and pecorino romano atop a layer of BBQ sauce. With a drizzle of even more BBQ sauce.", PhotoPath = "productImages/9.jpg", Price = 14, CategoryId = Category.ChicagoThinCrustPizza },
                      new Product { Id = 10, Name = "Margherita", Description = "Fresh mozzarella and basil with our housemade pizza sauce.", PhotoPath = "productImages/10.jpg", Price = 11, CategoryId = Category.ChicagoThinCrustPizza },
                      new Product { Id = 11, Name = "Spicy Hawaiian", Description = "Say aloha to this hot mama of a Hawaiian. We start with a layer of sweet red chili sauce, then add ham, fresh pineapple chunks, jalapeños, mozzarella, aged cheddar, and pecorino romano and finish it with a drizzle of ranch.", PhotoPath = "productImages/11.jpg", Price = 14, CategoryId = Category.ChicagoThinCrustPizza },
                      new Product { Id = 12, Name = "Bianco Love", Description = "If you're a fan of cheese and garlic (and really, who isn't?), you'll love this white pizza with creamy ricotta, aged cheddar, mozzarella, romano, extra virgin olive oil, and seasoned garlic.", PhotoPath = "productImages/12.jpg", Price = 14, CategoryId = Category.ChicagoThinCrustPizza },
                      new Product { Id = 13, Name = "Taco", Description = "Our crispy cauliflower crust topped with jalapenos, salsa, spiced beef, grated mozzarella and cheddar cheeses. Baked to perfection and topped with chopped lettuce and tomatoes. Served with sour cream and salsa.", PhotoPath = "productImages/13.jpg", Price = 16, CategoryId = Category.ChicagoThinCrustPizza },
                      new Product { Id = 14, Name = "Easy Street", Description = "Light and refreshing, Easy Street Wheat is an unfiltered American-style wheat beer. Leaving in the yeast gives the beer a nice, smooth finish and a slightly citrusy flavor.", PhotoPath = "productImages/14.jpg", Price = 5, CategoryId = Category.Beer },
                      new Product { Id = 15, Name = "Belgian White", Description = "Blue Moon Belgian White, Belgian-style wheat ale, is a refreshing, medium-bodied, unfiltered Belgian-style wheat ale spiced with fresh coriander and orange peel for a uniquely complex taste and an uncommonly smooth finish.", PhotoPath = "productImages/15.jpg", Price = 5, CategoryId = Category.Beer },
                      new Product { Id = 16, Name = "Summer Shandy", Description = "A shandy is beer mixed with a little something extra like soda, juice or ginger ale. Leinenkugel’s® Summer Shandy® is a traditional wheat beer with refreshing natural lemonade flavor that makes it perfect for the sun-splashed summer months. For a fun and fresh food pairing, try it with lighter, brighter foods like shrimp quesadillas, pan-seared scallops with lemon vinaigrette, or lemon chicken with pasta.", PhotoPath = "productImages/16.jpg", Price = 5, CategoryId = Category.Beer },
                      new Product { Id = 17, Name = "Mark West Pinot Noir", Description = "California", PhotoPath = "productImages/17.jpg", Price = 30, CategoryId = Category.Wine },
                      new Product { Id = 18, Name = "Trinity Oaks Merlot", Description = "California", PhotoPath = "productImages/18.jpg", Price = 25, CategoryId = Category.Wine },
                      new Product { Id = 19, Name = "Canyon Road Cabernet", Description = "California", PhotoPath = "productImages/19.jpg", Price = 35, CategoryId = Category.Wine }
                    }.AsQueryable();
        }

        public static List<ProductDto> GetTestDataForControllerTests()
        {
            return new List<ProductDto>
                    {
                      new ProductDto { Id = 1, Name = "Chicago Meat Market", Description = "Layers of sausage, meatballs, pepperoni, freshly shredded mozzarella, chunky tomato sauce, and pecorino romano.", PhotoPath = "productImages/1.jpg", Price = 19, CategoryId = Category.ChicagoDeepDishPizza},
                      new ProductDto { Id = 2, Name = "Chicago Classic", Description = "Extra sausage, extra cheese, extra good in your mouth.", PhotoPath = "productImages/2.jpg", Price = 15, CategoryId = Category.ChicagoDeepDishPizza },
                      new ProductDto { Id = 3, Name = "Prima Pepperoni", Description = "Pepperoni with Uno's own chunky vine-ripened tomato sauce, mozzarella and imported pecorino romano.", PhotoPath = "productImages/3.jpg", Price = 15, CategoryId = Category.ChicagoDeepDishPizza },
                      new ProductDto { Id = 4, Name = "Cheese & Tomato", Description = "Just what it sounds like.", PhotoPath = "productImages/4.jpg", Price = 12, CategoryId = Category.ChicagoDeepDishPizza },
                      new ProductDto { Id = 5, Name = "Meatball & Ricotta", Description = "Italian style beef and pork meatballs, ricotta cheese, fresh mushrooms, freshly shredded mozzarella, housemade marinara, and pecornio romano. Molto bene!", PhotoPath = "productImages/5.jpg", Price = 17, CategoryId = Category.ChicagoDeepDishPizza },
                      new ProductDto { Id = 6, Name = "Four Cheese & Pesto", Description = "Mozzarella, feta, cheddar, romano, and basil pesto sauce topped with diced tomatoes.", PhotoPath = "pizzaImages/6.jpg", Price = 15, CategoryId = Category.ChicagoDeepDishPizza },
                      new ProductDto { Id = 7, Name = "Farmer's Market", Description = "Caramelized onions, fresh spinach, sun-dried and plum tomatoes, roasted eggplant, pesto, feta, mozzarella and imported pecorino romano.", PhotoPath = "productImages/7.jpg", Price = 15, CategoryId = Category.ChicagoDeepDishPizza },
                      new ProductDto { Id = 8, Name = "New York Deli", Description = "The best of both worlds - an Italian sub in our deep dish crust! Filled with mozzarella, provolone, ham, pepperoni, onions, plum tomatoes and banana peppers, sprinkled with oregano and romano. Served with a side of our house vinaigrette.", PhotoPath = "productImages/8.jpg", Price = 17, CategoryId = Category.ChicagoDeepDishPizza },
                      new ProductDto { Id = 9, Name = "BBQ Chicken", Description = "Grilled chicken breast, red onions, mozzarella, aged cheddar and pecorino romano atop a layer of BBQ sauce. With a drizzle of even more BBQ sauce.", PhotoPath = "productImages/9.jpg", Price = 14, CategoryId = Category.ChicagoThinCrustPizza },
                      new ProductDto { Id = 10, Name = "Margherita", Description = "Fresh mozzarella and basil with our housemade pizza sauce.", PhotoPath = "productImages/10.jpg", Price = 11, CategoryId = Category.ChicagoThinCrustPizza },
                      new ProductDto { Id = 11, Name = "Spicy Hawaiian", Description = "Say aloha to this hot mama of a Hawaiian. We start with a layer of sweet red chili sauce, then add ham, fresh pineapple chunks, jalapeños, mozzarella, aged cheddar, and pecorino romano and finish it with a drizzle of ranch.", PhotoPath = "productImages/11.jpg", Price = 14, CategoryId = Category.ChicagoThinCrustPizza },
                      new ProductDto { Id = 12, Name = "Bianco Love", Description = "If you're a fan of cheese and garlic (and really, who isn't?), you'll love this white pizza with creamy ricotta, aged cheddar, mozzarella, romano, extra virgin olive oil, and seasoned garlic.", PhotoPath = "productImages/12.jpg", Price = 14, CategoryId = Category.ChicagoThinCrustPizza },
                      new ProductDto { Id = 13, Name = "Taco", Description = "Our crispy cauliflower crust topped with jalapenos, salsa, spiced beef, grated mozzarella and cheddar cheeses. Baked to perfection and topped with chopped lettuce and tomatoes. Served with sour cream and salsa.", PhotoPath = "productImages/13.jpg", Price = 16, CategoryId = Category.ChicagoThinCrustPizza },
                      new ProductDto { Id = 14, Name = "Easy Street", Description = "Light and refreshing, Easy Street Wheat is an unfiltered American-style wheat beer. Leaving in the yeast gives the beer a nice, smooth finish and a slightly citrusy flavor.", PhotoPath = "productImages/14.jpg", Price = 5, CategoryId = Category.Beer },
                      new ProductDto { Id = 15, Name = "Belgian White", Description = "Blue Moon Belgian White, Belgian-style wheat ale, is a refreshing, medium-bodied, unfiltered Belgian-style wheat ale spiced with fresh coriander and orange peel for a uniquely complex taste and an uncommonly smooth finish.", PhotoPath = "productImages/15.jpg", Price = 5, CategoryId = Category.Beer },
                      new ProductDto { Id = 16, Name = "Summer Shandy", Description = "A shandy is beer mixed with a little something extra like soda, juice or ginger ale. Leinenkugel’s® Summer Shandy® is a traditional wheat beer with refreshing natural lemonade flavor that makes it perfect for the sun-splashed summer months. For a fun and fresh food pairing, try it with lighter, brighter foods like shrimp quesadillas, pan-seared scallops with lemon vinaigrette, or lemon chicken with pasta.", PhotoPath = "productImages/16.jpg", Price = 5, CategoryId = Category.Beer },
                      new ProductDto { Id = 17, Name = "Mark West Pinot Noir", Description = "California", PhotoPath = "productImages/17.jpg", Price = 30, CategoryId = Category.Wine },
                      new ProductDto { Id = 18, Name = "Trinity Oaks Merlot", Description = "California", PhotoPath = "productImages/18.jpg", Price = 25, CategoryId = Category.Wine },
                      new ProductDto { Id = 19, Name = "Canyon Road Cabernet", Description = "California", PhotoPath = "productImages/19.jpg", Price = 35, CategoryId = Category.Wine }
                    };
        }
    }
}

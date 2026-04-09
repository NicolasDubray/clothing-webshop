using Microsoft.Extensions.DependencyInjection;

using ClothingWebstore.UIHelper;

﻿using EFCore;

using Entities;

using Services.Interfaces;

namespace ClothingWebstore
{
    public class CustomerProgram
    {
        public static async Task RunCustomer(IServiceProvider provider)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(Menu.ReturnCustomerMenu());
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GoToProductPage();
                        break;
                    case "2":
                        ViewCart();
                        break;
                    case "b":
                        GoBack();
                        break;

                    default:

                        Console.WriteLine("Invalid input!");
                        Thread.Sleep(1000);
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void GoToProductPage()
        {

        }

        private static void ViewCart()
        {

        }

        private static void GoBack()
        {

        }

        private static async Task PrintBestSellingProducts() //Behöver visas i boxar. 
        {
            //int amount = 3;
            //using var scope = _provider.CreateScope();
            //var service = scope.ServiceProvider.GetRequiredService<IProductService>();
            //var topProducts = await service.GetBestSellingProductsAsync(amount);

            //Console.WriteLine("Best selling products");
            //foreach (var product in topProducts)
            //{
            //    Console.WriteLine($"""
            //                        {product.Name}
            //                            {product.Price}
            //                            {product.ShortDescription}
            //                            {product.Brand.Name}

            //                        """);
            //}
        }
    }
}

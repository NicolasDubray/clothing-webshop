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
    }
}

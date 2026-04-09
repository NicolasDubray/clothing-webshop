using ClothingWebstore.UIHelper;
using Entities;
using EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;

namespace ClothingWebstore
{
    public class AdminProgram
    {
        private static IServiceProvider _provider;
        public static async Task RunAdmin(IServiceProvider provider)
        {
            _provider = provider;
            while (true)
            {
                Console.Clear();
                new Window("Choice", 0, 0, Menu.ReturnAdminStartList()).Draw();
                new Window("Navigation", 40, 0, Menu.ReturnInstructionList()).Draw();
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ManageProducts();
                        break;

                    case "2":
                        await ManageCategories();
                        break;

                    case "3":
                        await ManageOrAddCustomer();
                        break;

                    case "4":
                        await SeeStatistics();
                        break;

                    case "B":
                    case "b":
                        return;

                    default:
                        Message.InvalidInput();
                        break;
                }
            }
        }
        private static async Task ManageProducts()
        {

        }

        private static async Task ManageCategories()
        {

        }

        private static async Task ManageOrAddCustomer()
        {
            while (true)
            {
                Console.Clear();
                new Window("Choice", 0, 0, Menu.ReturnManageCustomerList()).Draw();
                new Window("Navigation", 40, 0, Menu.ReturnInstructionList()).Draw();

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        await ManageCustomers();
                        break;

                    case "2":
                        await AddNewCustomer();
                        break;

                    case "3":
                        await DeleteCustomer();
                        break;

                    case "4":
                        Console.Clear();
                        await ListAllCustomers();
                        Message.PressAnyKeyToContinue();
                        break;

                    case "B":
                    case "b":
                        return;

                    default:
                        Message.InvalidInput();
                        break;
                }
            }
        }

        private static async Task ManageCustomers()
        {
            while (true)
            {
                Console.Clear();
                new Window("Choice", 0, 0, Menu.ReturnSimpleTextList("Who would you like to manage?")).Draw();
                new Window("Navigation", 40, 0, Menu.ReturnInstructionList()).Draw();

                var customers = await ListAllCustomers();

                string? choice = Console.ReadLine();
                if (choice is null)
                {
                    Message.InvalidInput();
                    continue;
                }

                if (choice.Equals("B", StringComparison.OrdinalIgnoreCase))
                    return;
                if (int.TryParse(choice, out int id))
                {
                    var customer = customers.FirstOrDefault(c => c.Id == id);
                    if (customer is not null)
                    {
                        await ManageCustomer(customer);
                        return;
                    }
                    Message.InvalidInput();
                }
            }
        }

        private static async Task ManageCustomer(Customer customer)
        {
            using var scope = _provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
            var customerWithAddresses = await service.GetWithAddressesAsync(customer.Id);
            while (true)
            {
                Console.Clear();
                new Window("Manage customer", 0, 0, Menu.ReturnSimpleTextList("What would you like to change")).Draw();
                new Window("Navigation", 40, 0, Menu.ReturnInstructionList()).Draw();
                Console.WriteLine(Menu.ReturnCustomerDetailsMenu(customerWithAddresses!));

                string? input = Console.ReadLine();

                if (input is null)
                {
                    Message.InvalidInput();
                    continue;
                }

                if (input.Equals("B", StringComparison.OrdinalIgnoreCase))
                    return;

                switch (input)
                {
                    case "1":
                        await UpdateCustomerProperty(customerWithAddresses!,
                            "name",
                            ValidateInput.IsValidName,
                            (c, value) => c.Name = value);
                        break;

                    case "2":
                        await UpdateCustomerProperty(customerWithAddresses!,
                            "birth date (yyyy-MM-dd)",
                            ValidateInput.IsValidBirthDate,
                            (c, value) => c.BirthDate = DateTime.Parse(value));
                        break;

                    case "3":
                        await UpdateCustomerProperty(customerWithAddresses!,
                            "email",
                            ValidateInput.IsValidEmail,
                            (c, value) => c.Email = value);
                        break;

                    case "4":
                        await UpdateCustomerProperty(customerWithAddresses!,
                            "phone",
                            ValidateInput.IsValidPhone,
                            (c, value) => c.Phone = value);
                        break;

                    case "5":
                        await UpdateCustomerProperty(customerWithAddresses!,
                            "street",
                            ValidateInput.IsValidAddress,
                            (c, value) => c.Addresses.FirstOrDefault()!.Address.StreetAddress = value);
                        break;

                    case "6":
                        await UpdateCustomerProperty(customerWithAddresses!,
                            "city",
                            ValidateInput.IsValidName,
                            (c, value) => c.Addresses.FirstOrDefault()!.Address.City = value);
                        break;

                    case "7":
                        await UpdateCustomerProperty(customerWithAddresses!,
                            "country",
                            ValidateInput.IsValidName,
                            (c, value) => c.Addresses.FirstOrDefault()!.Address.Country = value);
                        break;

                    case "8":
                        await ListOrderHistory(customerWithAddresses!);
                        break;

                    default:
                        Message.InvalidInput();
                        break;
                }
            }
        }

        private static async Task UpdateCustomerProperty(Customer customer, string property, Func<string, bool> validate, Action<Customer, string> update)
        {
            while (true)
            {
                Console.Clear();
                Console.Write($"Enter new {property}:");
                string? input = Console.ReadLine();

                if (validate(input!))
                {
                    using var scope = _provider.CreateScope();
                    var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                    var context = scope.ServiceProvider.GetRequiredService<WebshopDbContext>();
                    update(customer, input!);
                    await service.UpdateAsync(customer);
                    return;
                }
                Message.InvalidInput();
            }
        }

        private static async Task<List<Customer>> ListAllCustomers()
        {
            using var scope = _provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
            var customers = await service.GetAllAsync();

            foreach (var c in customers)
            {
                Console.WriteLine($"[{c.Id}] {c.Name}");
            }
            return customers;
        }

        private static async Task ListOrderHistory(Customer customer)
        {
            using var scope = _provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
            var customerWithOrders = await service.GetWithOrdersAsync(customer.Id);

            Console.Clear();
            new Window("Order", 0, 0, Menu.ReturnSimpleTextList("Total order history")).Draw();
            new Window("Navigation", 40, 0, Menu.ReturnInstructionList()).Draw();

            if (customerWithOrders?.Orders.Count != 0)
            {
                foreach (var order in customerWithOrders!.Orders)
                {
                    Console.WriteLine($"Order: {order.OrderNumber}");

                    foreach (var op in order.OrderProducts)
                    {
                        Console.WriteLine($"  {op.Product.Name} x{op.ProductAmount} - {op.Product.Price:C}");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No orders found.");
            }
            if (Console.ReadLine()!.Equals("B", StringComparison.OrdinalIgnoreCase))
                return;
        }

        private static async Task AddNewCustomer()
        {
            string name = GetInputForNewCustomer("Name", ValidateInput.IsValidName);
            string birthDate = GetInputForNewCustomer("Birth date (yyyy-MM-dd)", ValidateInput.IsValidBirthDate);
            string email = GetInputForNewCustomer("Email", ValidateInput.IsValidEmail);
            string phone = GetInputForNewCustomer("Phone", ValidateInput.IsValidPhone);
            string street = GetInputForNewCustomer("Street", ValidateInput.IsValidAddress);
            string city = GetInputForNewCustomer("City", ValidateInput.IsValidName);
            string country = GetInputForNewCustomer("Country", ValidateInput.IsValidName);

            using var scope = _provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
            var context = scope.ServiceProvider.GetRequiredService<WebshopDbContext>();
            await service.AddAsync(new Customer
            {
                Name = name,
                BirthDate = DateTime.Parse(birthDate),
                Email = email,
                Phone = phone,
                Addresses = new List<AddressCustomer>
                {
                    new AddressCustomer
                    {
                        Address = new Address
                        {
                            StreetAddress = street,
                            City = city,
                            Country = country
                        }
                    }
                }
            });
        }

        private static string GetInputForNewCustomer(string property, Func<string, bool> validate)
        {
            while (true)
            {
                Console.Clear();
                Console.Write($"{property}: ");
                string? input = Console.ReadLine();

                if (validate(input!))
                    return input!;

                Message.InvalidInput();
            }
        }

        private static async Task DeleteCustomer()
        {
            while (true)
            {
                Console.Clear();
                new Window("Choice", 0, 0, Menu.ReturnSimpleTextList("Who would you like to delete?")).Draw();
                new Window("Navigation", 40, 0, Menu.ReturnInstructionList()).Draw();
                var customers = await ListAllCustomers();

                string? input = Console.ReadLine();

                if (input.Equals("B", StringComparison.OrdinalIgnoreCase))
                    return;

                int id = int.Parse(input);

                var customer = customers.FirstOrDefault(c => c.Id == id);

                if(ValidateInput.IsValidId(input, customers) && customer != null)
                {
                    Console.WriteLine($"Are you sure you want to delete: {customer.Name}?");
                    Console.WriteLine("Press Y/y + enter");
                    string? sure = Console.ReadLine();
                    if(sure?.Equals("Y", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        using var scope = _provider.CreateScope();
                        var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                        var context = scope.ServiceProvider.GetRequiredService<WebshopDbContext>();
                        await service.DeleteAsync(customer);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                Message.InvalidInput();
            }
        }

        private static async Task SeeStatistics()
        {
            while (true)
            {
                Console.Clear();
                new Window("Statistics", 0, 0, Menu.ReturnSimpleTextList("All statistics for shop")).Draw();
                new Window("Navigation", 60, 0, Menu.ReturnInstructionStatisticsList()).Draw();

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.B:
                        return;
                    case ConsoleKey.D1:
                        await PrintBestSellingProducts();
                        break;
                    case ConsoleKey.D2:
                        await PrintTotalRevenue();
                        break;
                    case ConsoleKey.D3:
                        await PrintTopBuyingCustomers();
                        break;
                    case ConsoleKey.D4:
                        await PrintBestSellingCategories();
                        break;
                    default:
                        Message.InvalidInput();
                        break;
                }
            }
        }

        private static async Task PrintBestSellingProducts()
        {
            int amount = 3;
            using var scope = _provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IProductService>();
            var topProducts = await service.GetBestSellingProductsAsync(amount);

            var rows = topProducts.Select(p => $"{p.Name} - {p.Price:C} ({p.Brand.Name})").ToList();
            new Window("Best Selling Products", 0, 5, rows).Draw();
            Message.PressAnyKeyToContinue();
        }

        private static async Task PrintTotalRevenue()
        {
            using var scope = _provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IProductService>();
            var total = await service.GetTotalRevenueAsync();

            new Window("Total revenue", 0, 5, Menu.ReturnSimpleTextList($"{total:C}")).Draw();
            Message.PressAnyKeyToContinue();
        }

        private static async Task PrintTopBuyingCustomers()
        {
            using var scope = _provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
            var topBuyingCustomers = await service.GetTopBuyingCustomersAsync(1);

            var rows = topBuyingCustomers.Select(c => c.Name).ToList();
            new Window("Top buying customers", 0, 5, rows).Draw();
            Message.PressAnyKeyToContinue();
        }

        private static async Task PrintBestSellingCategories()
        {
            using var scope = _provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICategoryService>();
            var categories = await service.GetBestSellingCategoriesAsync(3);

            var rows = categories.Select(c => c.Name).ToList();
            new Window("Top Categories", 0, 5, rows).Draw();
            Message.PressAnyKeyToContinue();
        }
    }
}

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
                        await ListAllCustomersNames();
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

                var customers = await ListAllCustomersWithId();

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
                    update(customer, input!);
                    await service.UpdateAsync(customer);
                    return;
                }
                Message.InvalidInput();
            }
        }

        private static async Task<List<Customer>> ListAllCustomersWithId()
        {
            using var scope = _provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
            var customers = await service.GetAllAsync();

            var rows = customers.Select(p => $"[{p.Id}] {p.Name}").ToList();
            new Window("Customers", 0, 5, rows).Draw();
            return customers;
        }

        private static async Task<List<Customer>> ListAllCustomersNames()
        {
            using var scope = _provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
            var customers = await service.GetAllAsync();

            var rows = customers.Select(p => $"{p.Name}").ToList();
            new Window("All customers", 0, 0, rows).Draw();
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
            string email = GetInputForNewCustomer("Email (xxx@xxx.xx)", ValidateInput.IsValidEmail);
            string phone = GetInputForNewCustomer("Phone (10 digits)", ValidateInput.IsValidPhone);
            string street = GetInputForNewCustomer("Street", ValidateInput.IsValidAddress);
            string city = GetInputForNewCustomer("City", ValidateInput.IsValidName);
            string country = GetInputForNewCustomer("Country", ValidateInput.IsValidName);

            using var scope = _provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
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
                var customers = await ListAllCustomersWithId();

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

        }
    }
}

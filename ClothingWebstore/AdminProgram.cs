using ClothingWebstore.UIHelper;
using Entities;
using EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services;

namespace ClothingWebstore;

public class AdminProgram
{
    private static IServiceProvider? _provider;
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

                case "5":
                    await ManageProductDeals();
                    break;

                case "B":
                case "b":
                    return;

                default:
                    Message.PrintInvalidInput();
                    break;
                
            }
        }
    }

    private static async Task ManageProducts()
    {
        while (true)
        {
            using var scope = _provider!.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IProductService>();
            var brandService = scope.ServiceProvider.GetRequiredService<IBrandService>();
            var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();

            Console.Clear();
            new Window("Choice", 0, 0, Menu.ReturnManageProductList()).Draw();
            new Window("Choice", 25, 0, Menu.ReturnInstructionList()).Draw();
            Console.WriteLine();

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await ShowProducts(service);
                    Message.PressAnyKeyToContinue();
                    break;

                case "2":
                    await AddProducts(service, brandService, categoryService);
                    break;

                case "3":
                    await RemoveProducts(service);
                    break;

                case "4":
                    await ChangeProducts();
                    break;

                default:
                    return;
            }
        }
    }

    private static async Task ManageCategories()
    {
        using var scope = _provider!.CreateScope();
        var serviceCategory = scope.ServiceProvider.GetRequiredService<ICategoryService>();
        var serviceProduct = scope.ServiceProvider.GetRequiredService<IProductService>();
        while (true)
        {
            Console.Clear();
            new Window("Choice", 0, 0, Menu.ReturnManageCategoriesList()).Draw();
            new Window("Navigation", 40, 0, Menu.ReturnInstructionList()).Draw();

            string? input = Console.ReadLine();

            Console.Clear();
            switch (input)
            {
                case "1":
                    await ShowCategories(serviceCategory);
                    break;

                case "2":
                    await AddCategory(serviceCategory);
                    break;

                case "3":
                    await RemoveCategory(serviceProduct, serviceCategory);
                    break;

                case "B":
                case "b":
                    return;

                default:
                    Message.PrintInvalidInput();
                    break;
            }
        }
    }

    private static async Task ShowCategories(ICategoryService service)
    {
        var categories = await service.GetAllAsync();

        var categoryList = categories.Select(c => $"{c.Id} - {c.Name}").ToList();

        new Window("Categories", 0, 0, categoryList).Draw();
        Message.PressAnyKeyToContinue();
    }

    private static async Task RemoveCategory(IProductService serviceProduct, ICategoryService serviceCategory)
    {
        while (true)
        {
            Console.Clear();
            new Window("Choice", 0, 0, Menu.ReturnSimpleTextList("What category would you like to delete?")).Draw();
            new Window("Navigation", 50, 0, Menu.ReturnInstructionList()).Draw();

            var categories = await serviceCategory.GetAllAsync();
            var rows = categories.Select(c => $"[{c.Id}] {c.Name}").ToList();
            new Window("All categories", 0, 3, rows).Draw();

            string? input = Console.ReadLine();

            if (input!.Equals("B", StringComparison.OrdinalIgnoreCase))
                return;

            if(ValidateInput.IsValidCategoryId(input, categories) && int.TryParse(input, out int id))
            {
                Console.Clear();
                var category = await serviceCategory.GetByIdAsync(id);
                if(category is null)
                    continue;

                bool hasProducts = await serviceProduct.CategoryHasProductsAsync(category.Id);
                if (hasProducts)
                {
                    Console.Clear();
                    List<string> messageRows = ["Can´t delete this category.", "It has products linked to it."];
                    new Window("Message", 0, 0, messageRows).Draw();
                    Message.PressAnyKeyToContinue();
                    continue;
                }
                else
                {
                    Console.Clear();
                    List<string> confirmeRows = [$"Are you sure you want to delete: {category.Name}", "Press Y/y + enter for yes"];
                    new Window("Confirm", 0, 0, confirmeRows).Draw();

                    string? approved = Console.ReadLine();
                    if (approved?.Equals("Y", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        await serviceCategory.DeleteAsync(category);
                        return;
                    }
                    else
                    {
                        Message.PrintMessage("Category was not removed. Returning.");
                        continue;
                    }
                }
            }
        }
    }

    private static async Task AddCategory(ICategoryService service)
    {
        while (true)
        {
            Console.Clear();
            List<string> rows = ["Enter the name for new category.", "[B] Back"];
            new Window("Choice", 0, 0, rows).Draw();
            Console.Write("Category name: ");

            string? input = Console.ReadLine();

            if (input!.Equals("B", StringComparison.OrdinalIgnoreCase))
                return;

            if (ValidateInput.IsValidName(input!))
            {
                var category = new Category{ Name = input! };
                await service.AddAsync(category);
                return;
            }
            Message.PrintInvalidInput();
        }
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
                    Message.PrintInvalidInput();
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
                Message.PrintInvalidInput();
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
                Message.PrintInvalidInput();
            }
            else
                Message.PrintInvalidInput();
        }
    }

    private static async Task ManageCustomer(Customer customer)
    {
        while (true)
        {
            using var scope = _provider!.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
            var customerWithAddress = await service.GetWithAddressesAsync(customer.Id);
            Console.Clear();
            new Window("Manage customer", 0, 0, Menu.ReturnSimpleTextList("What would you like to change")).Draw();
            new Window("Navigation", 40, 0, Menu.ReturnInstructionList()).Draw();

            new Window("Customer", 0, 3, Menu.ReturnCustomerDetailsList(customerWithAddress!)).Draw();

            string? input = Console.ReadLine();

            if (input is null)
            {
                Message.PrintInvalidInput();
                continue;
            }

            if (input.Equals("B", StringComparison.OrdinalIgnoreCase))
                return;

            switch (input)
            {
                case "1":
                    await UpdateCustomerProperty(customerWithAddress!,
                        "name",
                        ValidateInput.IsValidName,
                        (c, value) => c.Name = value);
                    break;

                case "2":
                    await UpdateCustomerProperty(customerWithAddress!,
                        "birth date (yyyy-MM-dd)",
                        ValidateInput.IsValidBirthDate,
                        (c, value) => c.BirthDate = DateTime.Parse(value));
                    break;

                case "3":
                    await UpdateCustomerProperty(customerWithAddress!,
                        "email",
                        ValidateInput.IsValidEmail,
                        (c, value) => c.Email = value);
                    break;

                case "4":
                    await UpdateCustomerProperty(customerWithAddress!,
                        "phone",
                        ValidateInput.IsValidPhone,
                        (c, value) => c.Phone = value);
                    break;

                case "5":
                    await UpdateCustomerProperty(customerWithAddress!,
                        "street",
                        ValidateInput.IsValidAddress,
                        (c, value) => c.Addresses.FirstOrDefault()!.Address.StreetAddress = value);
                    break;

                case "6":
                    await UpdateCustomerProperty(customerWithAddress!,
                        "city",
                        ValidateInput.IsValidName,
                        (c, value) => c.Addresses.FirstOrDefault()!.Address.City = value);
                    break;

                case "7":
                    await UpdateCustomerProperty(customerWithAddress!,
                        "country",
                        ValidateInput.IsValidName,
                        (c, value) => c.Addresses.FirstOrDefault()!.Address.Country = value);
                    break;

                case "8":
                    await ListOrderHistory(customerWithAddress!);
                    break;

                default:
                    Message.PrintInvalidInput();
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
                using var scope = _provider!.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                update(customer, input!);
                await service.UpdateAsync(customer);
                return;
            }
            Message.PrintInvalidInput();
        }
    }

    private static async Task<List<Customer>> ListAllCustomersWithId()
    {
        using var scope = _provider!.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
        var customers = await service.GetAllAsync();

        var rows = customers.Select(p => $"[{p.Id}] {p.Name}").ToList();
        new Window("Customers", 0, 3, rows).Draw();
        return customers;
    }

    private static async Task<List<Customer>> ListAllCustomersNames()
    {
        using var scope = _provider!.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
        var customers = await service.GetAllAsync();

        var rows = customers.Select(p => $"{p.Name}").ToList();
        new Window("All customers", 0, 0, rows).Draw();
        return customers;
    }

    private static async Task ListOrderHistory(Customer customer)
    {
        using var scope = _provider!.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
        var customerWithOrders = await service.GetWithOrdersAsync(customer.Id);

        Console.Clear();
        new Window("Order", 0, 0, Menu.ReturnSimpleTextList("Total order history")).Draw();
        new Window("Navigation", 40, 0, Menu.ReturnInstructionList()).Draw();

        if (customerWithOrders?.Orders.Count > 0)
        {
            List<string> rows = [];
            foreach (var order in customerWithOrders!.Orders)
            {
                rows.Add($"Order: {order.OrderNumber}");

                foreach (var op in order.OrderProducts)
                {
                    rows.Add($"  {op.Product.Name} x{op.ProductAmount} - {op.Product.Price:C}");
                }
                rows.Add("");
            }
            new Window("Orders", 0, 5, rows).Draw();
        }
        else
        {
            new Window("Orders", 0, 5, Menu.ReturnSimpleTextList("No orders found.")).Draw();
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

        using var scope = _provider!.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
        await service.AddAsync(new Customer
        {
            Name = name,
            BirthDate = DateTime.Parse(birthDate),
            Email = email,
            Phone = phone,
            Addresses =
            [
                new AddressCustomer
                {
                    Address = new Address
                    {
                        StreetAddress = street,
                        City = city,
                        Country = country
                    }
                }
            ]
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

            Message.PrintInvalidInput();
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

            if (input!.Equals("B", StringComparison.OrdinalIgnoreCase))
                return;

            if (ValidateInput.IsValidCustomerId(input, customers) && int.TryParse(input, out int id))
            {
                var customer = customers.FirstOrDefault(c => c.Id == id);
                if (customer is null)
                    continue;

                Console.Clear();
                List<string> rows = [$"Are you sure you want to delete: {customer.Name}", "Press Y/y + enter for yes"];
                new Window("Confirm", 0, 0, rows).Draw();

                string? approved = Console.ReadLine();
                if (approved?.Equals("Y", StringComparison.OrdinalIgnoreCase) == true)
                {
                    using var scope = _provider!.CreateScope();
                    var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                    await service.DeleteAsync(customer);
                    return;
                }
                else
                {
                    Message.PrintMessage("Customer was not removed. Returning.");
                    continue;
                }
            }
            Message.PrintInvalidInput();
        }
    }


    private static async Task ChangeProducts()
    {
        while (true)
        {
            Console.Clear();
            new Window("Choice", 0, 0, Menu.ReturnSimpleTextList("What would you like to change")).Draw();
            new Window("Navigation", 40, 0, Menu.ReturnInstructionList()).Draw();

            var products = await ListAllProducts();

            string? choice = Console.ReadLine();
            if (choice is null)
            {
                Message.PrintInvalidInput();
                continue;
            }

            if (choice.Equals("B", StringComparison.OrdinalIgnoreCase))
                return;
            if (int.TryParse(choice, out int id))
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product is not null)
                {
                    await ManageProduct(product);
                    return;
                }
                Message.PrintInvalidInput();
            }
        }
    }

    private static async Task ManageProduct(Product product)
    {
        while (true)
        {
            using var scope = _provider!.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IProductService>();
            var brandService = scope.ServiceProvider.GetRequiredService<IBrandService>();
            var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
            var productWithAllDetails = await service.GetAllDetailsAsync(product.Id);

            Console.Clear();
            Console.WriteLine(Menu.ReturnProductDetailsMenu(productWithAllDetails!));
            string? input = Console.ReadLine();

            if (input is null)
            {
                Message.PrintInvalidInput();
                continue;
            }

            
            if (input.Equals("B", StringComparison.OrdinalIgnoreCase))
                return;

            switch (input)
            {
                case "1":
                    await UpdateProductProperty(productWithAllDetails!,
                        "name",
                        ValidateInput.ProNameIsValid,
                        (p, value) => p.Name = value);
                    break;

                case "2":
                    await UpdateProductRelation(
                        service,
                        productWithAllDetails!,
                        async () =>
                        {
                            var brands = await brandService.GetAllAsync();

                            return brands.Select(b => (b.Id, b.Name)).ToList();
                        },
                        (p, id) => p.BrandId = id
                    );
                    break;

                case "3":
                    await UpdateProductProperty(productWithAllDetails!,
                        "price",
                        ValidateInput.ProPriceIsValid,
                        (p, value) => p.Price = double.Parse(value));
                    break;

                case "4":
                    await UpdateProductRelation(
                        service,
                        productWithAllDetails!,
                        async () =>
                        {
                            var categories = await categoryService.GetAllAsync();

                            return categories.Select(c => (c.Id, c.Name)).ToList();
                        },
                        (p, id) => p.CategoryId = id
                    );
                    break;

                case "5":
                    await UpdateProductProperty(productWithAllDetails!,
                        "shortDescription",
                        ValidateInput.ProShortDescriptionIsValid,
                        (p, value) => p.ShortDescription = value);
                    break;

                case "6":
                    await UpdateProductProperty(productWithAllDetails!,
                        "longDescription",
                        ValidateInput.ProLongDescriptionIsValid,
                        (p, value) => p.LongDescription = value);
                    break;

                default:
                    Message.PrintInvalidInput();
                    return;
            }
        }
    }



    private static async Task UpdateProductRelation(IProductService service, Product product, Func<Task<List<(int Id, string Name)>>> getOptions, Action<Product, int> update)
    {
        Console.Clear();

        var items = await getOptions();

        Console.WriteLine("Choose an option:");
        Console.WriteLine();

        foreach (var item in items)
        {
            Console.WriteLine($"[{item.Id}] {item.Name}");
        }

        while (true)
        {
            Console.Write("Enter ID: ");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int id) && items.Any(i => i.Id == id))
            {
                update(product!, id);
                await service.UpdateAsync(product!);

                break;
            }

            Console.WriteLine("Invalid choice...");
        }
    }

    private static async Task UpdateProductProperty(Product product, string property, Func<string, bool> validate, Action<Product, string> update)
    {
        while (true)
        {
            Console.Clear();
            Console.Write($"Enter new {property}: ");
            string? input = Console.ReadLine();

            if (validate(input!))
            {
                using var scope = _provider!.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IProductService>();

                update(product, input!);
                await service.UpdateAsync(product);
                return;
            }
            Message.PrintInvalidInput();
        }
    }

    private static async Task<List<Product>> ListAllProducts()
    {
        using var scope = _provider!.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IProductService>();
        var products = await service.GetAllAsync();

        var productList = products.Select(p => (p.Id, p.Name)).ToList();

        foreach (var p in products)
        {
            Console.WriteLine($"[{p.Id}] - {p.Name}");
        }
        return products;
    }

    private static async Task RemoveProducts(IProductService service)
    {
        while (true)
        {
            Console.Clear();
            new Window("Choice", 0, 0, Menu.ReturnSimpleTextList("What would you like to delete?")).Draw();
            new Window("Navigation", 40, 0, Menu.ReturnInstructionList()).Draw();

            var products = await ListAllProducts();
            Console.WriteLine();

            string? input = Console.ReadLine();

            if (input!.Equals("B", StringComparison.OrdinalIgnoreCase))
                return;

            int id = int.Parse(input);

            var product = products.FirstOrDefault(p => p.Id == id);

            if (ValidateInput.IsValidProId(input, products) && products != null)
            {
                Console.WriteLine($"Are you sure you want to delete: {product!.Name}?");
                Console.WriteLine("Press Y/y + enter");
                string? sure = Console.ReadLine();
                if (sure?.Equals("Y", StringComparison.OrdinalIgnoreCase) == true)
                {
                    await service.DeleteAsync(product);
                    return;
                }
                else
                {
                    return;
                }
            }
            Message.PrintInvalidInput();
        }
    }

    private static async Task AddProducts(IProductService service, IBrandService brandService, ICategoryService categoryService)
    {
        string name = GetInputForNewProduct("Name",
            ValidateInput.ProNameIsValid);
        string shortDescription = GetInputForNewProduct("Short Description",
            ValidateInput.ProShortDescriptionIsValid);
        string longDescription = GetInputForNewProduct("Long Description",
            ValidateInput.ProLongDescriptionIsValid);
        string price = GetInputForNewProduct("Price",
            ValidateInput.ProPriceIsValid);

        int brandId = await GetValidIdFromUser(
            "Brand",
            async () =>
            {
                var brands = await brandService.GetAllAsync();
                return brands.Select(b => (b.Id, b.Name)).ToList();
            });
        int categoryId = await GetValidIdFromUser(
            "Category",
            async () =>
            {
                var categories = await categoryService.GetAllAsync();
                return categories.Select(c => (c.Id, c.Name)).ToList();
            });
        
        


        await service.AddAsync(new Product
        {
            Name = name,
            BrandId = brandId,
            Price = double.Parse(price),
            CategoryId = categoryId,
            ShortDescription = shortDescription,
            LongDescription = longDescription
        });
    }

    private static async Task<int> GetValidIdFromUser(string title, Func<Task<List<(int Id, string Name)>>> getItems)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Choose {title}:");
            Console.WriteLine();

            var items = await getItems();

            foreach (var item in items)
            {
                Console.WriteLine($"[{item.Id}] - {item.Name}");
            }

            Console.Write("Enter ID: ");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int id) && items.Any(i => i.Id == id))
            {
                return id;
            }

            Console.WriteLine("Invalid Choice");
            Console.ReadLine();
        }
    }

    private static string GetInputForNewProduct(string property, Func<string, bool> validate)
    {
        while (true)
        {
            Console.Clear();
            Console.Write($"{property}: ");
            string? input = Console.ReadLine();

            if (validate(input!))
                return input!;

            Message.PrintInvalidInput();
        }
    }

    private static async Task ShowProducts(IProductService service)
    {
        Console.Clear();
        var products = await service.GetAllAsync();

        var productList = products.Select(p => $"[{p.Id}] - {p.Name} - {p.Price}Kr ______ {p.ShortDescription}").ToList();

        new Window("Products", 0, 0, productList).Draw();
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
                        Message.PrintInvalidInput();
                        break;
                }
            }
      }

    private static async Task PrintBestSellingProducts()
    {
        int amount = 3;
        using var scope = _provider!.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IProductService>();
        var topProducts = await service.GetBestSellingProductsAsync(amount);

        var rows = topProducts.Select(p => $"{p.Name} - {p.Price:C} ({p.Brand.Name})").ToList();
        new Window("Best Selling Products", 0, 5, rows).Draw();
        Message.PressAnyKeyToContinue();
    }

    private static async Task PrintTotalRevenue()
    {
        using var scope = _provider!.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IProductService>();
        var total = await service.GetTotalRevenueAsync();

        new Window("Total revenue", 0, 5, Menu.ReturnSimpleTextList($"{total}$")).Draw();
        Message.PressAnyKeyToContinue();
    }

    private static async Task PrintTopBuyingCustomers()
    {
        using var scope = _provider!.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
        var topBuyingCustomers = await service.GetTopBuyingCustomersAsync(3);

        var rows = topBuyingCustomers.Select(c => c.Name).ToList();
        new Window("Top buying customers", 0, 5, rows).Draw();
        Message.PressAnyKeyToContinue();
    }

    private static async Task PrintBestSellingCategories()
    {
        using var scope = _provider!.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<ICategoryService>();
        var categories = await service.GetBestSellingCategoriesAsync(3);

        var rows = categories.Select(c => c.Name).ToList();
        new Window("Top Categories", 0, 5, rows).Draw();
        Message.PressAnyKeyToContinue();
    }

    private static async Task ManageProductDeals()
    {
        using var scope = _provider!.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IProductService>();
            
        if (await RemoveProductDeal(service))
        {
            await AddProductDeal(service);
        }
    }

    private static async Task<bool> RemoveProductDeal(IProductService service)
    {
        var productsWithDeals = await service.GetProductsWithDealsAsync();

        while (true)
        {
            Console.Clear();
            new Window("Choice", 0, 0, Menu.ReturnSimpleTextList("Choose a product to remove deal")).Draw();
            new Window("Navigation", 50, 0, Menu.ReturnInstructionList()).Draw();

            var rows = productsWithDeals.Select(p => $"[{p.Id}] {p.Name} - Price: {p.Price}").ToList();
            new Window("Product deals", 0, 5, rows).Draw();

            string? input = Console.ReadLine();

            if (input!.Equals("B", StringComparison.OrdinalIgnoreCase))
                return false;

            if (ValidateInput.IsValidProductId(input!, productsWithDeals))
            {
                int id = int.Parse(input!);
                var product = productsWithDeals.FirstOrDefault(p => p.Id == id);
                product!.OnSale = false;
                product.Price += 3;
                await service.UpdateAsync(product);
                return true;
            }
            else
            {
                Message.PrintInvalidInput();
                continue;
            }
        }
    }

    private static async Task AddProductDeal(IProductService service)
    {
        var products = await service.GetAllAsync();

        var productsWithoutDeals = products
            .Where(p => p.OnSale == false)
            .ToList();

        while (true)
        {
            Console.Clear();
            new Window("Choice", 0, 0, Menu.ReturnSimpleTextList("Choose a product to add deal")).Draw();
            new Window("Navigation", 50, 0, Menu.ReturnSimpleTextList("Press key + enter")).Draw();

            var rows = productsWithoutDeals.Select(p => $"[{p.Id}] {p.Name} - Price: {p.Price}").ToList();
            new Window("Products", 0, 5, rows).Draw();

            string? input = Console.ReadLine();

            if (ValidateInput.IsValidProductId(input!, productsWithoutDeals))
            {
                int id = int.Parse(input!);
                var product = productsWithoutDeals.FirstOrDefault(p => p.Id == id);

                product!.OnSale = true;

                if(product.Price > 3)
                    product.Price -= 3; 

                await service.UpdateAsync(product);
                return;
            }
            Message.PrintInvalidInput();
        }
    }
}

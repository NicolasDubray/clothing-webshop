using ClothingWebstore.UIHelper;
using EFCore;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ClothingWebstore
{
    public class CustomerProgram
    {
        private static List<Product> cart = new List<Product>();
        private static IServiceProvider CustomerProvider;

        private static List<string>? _cachedWeather;
        private static DateTime _lastFetch = DateTime.MinValue;
        public static async Task RunCustomer(IServiceProvider provider)
        {
            CustomerProvider = provider;

            while (true)
            {
                Console.Clear();


                new Window("Webshop", 0, 0, Menu.ReturnCustomerMenuList()).Draw();
                new Window("Navigation", 35, 0, Menu.ReturnInstructionList()).Draw();
                new Window("Weather", 60, 0, await ReturnApiData()).Draw();

                await DisplayProductDeals();
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await GoToProductPage();
                        break;
                    case "2":
                        await ViewCart();
                        break;
                    case "b":
                    case "B":
                        await GoBack();
                        break;

                    default:
                        Message.PrintInvalidInput();
                        break;
                }
            }
        }

        private static async Task GoToProductPage()
        {
            using var scope = CustomerProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<WebshopDbContext>();

            string currentSearch = "";

            async Task<List<Product>> LoadProducts()
            {
                Console.Clear();
                IQueryable<Product> query = dbContext.Products;

                if (!string.IsNullOrWhiteSpace(currentSearch))
                {
                    query = query.Where(p =>
                        EF.Functions.Like(p.Name, $"%{currentSearch}%") ||
                        EF.Functions.Like(p.ShortDescription, $"%{currentSearch}%") ||
                        EF.Functions.Like(p.LongDescription, $"%{currentSearch}%"));
                }

                return await query.ToListAsync();
            }

            var products = await LoadProducts();


            Console.Clear();
            Lowest.LowestPosition = 0;

            int startLeft = 2;
            int startTop = 2;

            int boxWidth = 30;

            int itemsPerRow = 5;
            int spacingX = 5;
            int spacingY = 2;


            var rowsOfProducts = new List<List<Product>>();

            for (int i = 0; i < products.Count; i += itemsPerRow)
            {
                rowsOfProducts.Add(products.Skip(i).Take(itemsPerRow).ToList());
            }

            int selectedProductIndex = 0;
            ConsoleKey key;
            bool addtoCartMode = false;


            do
            {
                Console.Clear();

                Console.WriteLine("Use Arrowkeys to navigate | Press Enter to show more details | S = Search | R = Reset | B = Exit\n");

                int currentTop = startTop;
                int globalIndex = 0;

                foreach (var rowProducts in rowsOfProducts)
                {
                    int maxBoxHeight = 10;
                    var windows = new List<(Window window, int height, int left)>();

                    for (int i = 0; i < rowProducts.Count; i++)
                    {
                        var product = rowProducts[i];
                        var rows = new List<string>
                {

                    $"{(globalIndex == selectedProductIndex ? "> " : "  ")}{product.Name}",
                    $"Price: {product.Price} $",
                    $"Description:"
                };

                        rows.AddRange(WrapTextLimited(product.ShortDescription, boxWidth - 5, 4));

                        int boxHeight = rows.Count + 2;
                        int left = startLeft + i * (boxWidth + spacingX);

                        windows.Add((new Window(product.Id.ToString(), left, currentTop, rows), boxHeight, left));

                        globalIndex++;
                    }


                    foreach (var (window, _, _) in windows)
                        window.Draw();

                    currentTop += maxBoxHeight + spacingY;
                }


                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;


                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        if (selectedProductIndex < products.Count - 1) selectedProductIndex++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (selectedProductIndex > 0) selectedProductIndex--;
                        break;
                    case ConsoleKey.UpArrow:
                        if (selectedProductIndex - itemsPerRow >= 0) selectedProductIndex -= itemsPerRow;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedProductIndex + itemsPerRow < products.Count) selectedProductIndex += itemsPerRow;
                        break;

                    case ConsoleKey.V:
                        addtoCartMode = true;
                        break;

                    case ConsoleKey.S:
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.Write("Search: ");

                        currentSearch = Console.ReadLine() ?? "";

                        products = await LoadProducts();
                        Console.Clear();

                        Console.WriteLine($"Found {products.Count} products");

                        Thread.Sleep(2000);


                        rowsOfProducts.Clear();
                        for (int i = 0; i < products.Count; i += itemsPerRow)

                        {
                            rowsOfProducts.Add(products.Skip(i).Take(itemsPerRow).ToList());

                        }

                        selectedProductIndex = 0;


                        break;

                    case ConsoleKey.R:
                        currentSearch = "";
                        products = await LoadProducts();

                        rowsOfProducts.Clear();

                        for (int i = 0; i < products.Count; i += itemsPerRow)
                        {
                            rowsOfProducts.Add(products.Skip(i).Take(itemsPerRow).ToList());
                        }

                        selectedProductIndex = 0;

                        break;

                    case ConsoleKey.B:
                        await GoBack();
                        break;

                    case ConsoleKey.Enter:

                        var selectedProduct = products[selectedProductIndex];


                        if (addtoCartMode)
                        {
                            cart.Add(selectedProduct);


                            Console.WriteLine($"{selectedProduct.Name} added to cart!");
                            Console.ReadKey(true);

                        }
                        else
                        {
                            ShowProductDetails(selectedProduct, cart);
                        }
                        break;
                }

            } while (key != ConsoleKey.B);


            Console.SetCursorPosition(0, Lowest.LowestPosition + 5);

            Console.WriteLine("V  = Add to cart | B = Exit");
        }

        private static void ShowProductDetails(Product product, List<Product> cart)
        {
            ConsoleKey key;


            do
            {

                Console.Clear();
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Price: {product.Price} $");
                Console.WriteLine($"Description: {product.LongDescription}");
                Console.WriteLine(" V = Add to cart | B = Exit");

                var keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;

                switch (key)
                {
                    case ConsoleKey.V:
                        cart.Add(product);

                        Console.Clear();
                        Console.WriteLine($"{product.Name} added to cart!");
                        Message.PressAnyKeyToContinue();
                        break;

                }

            } while (key != ConsoleKey.B);

        }



        private static List<string> WrapTextLimited(string text, int maxWidth, int maxLines)
        {
            var lines = new List<string>();
            var words = text.Split(' ');
            string currentLine = "";

            foreach (var word in words)
            {
                if ((currentLine + " " + word).Trim().Length > maxWidth)
                {
                    lines.Add(currentLine);
                    currentLine = word;

                    if (lines.Count == maxLines)
                    {
                        lines[maxLines - 1] += "...";
                        break;
                    }
                }
                else
                {
                    currentLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
                }
            }

            if (!string.IsNullOrWhiteSpace(currentLine) && lines.Count < maxLines)
                lines.Add(currentLine);

            while (lines.Count < maxLines)
                lines.Add("");

            return lines;
        }

        private static async Task<List<string>> ReturnApiData()
        {
            if (_cachedWeather is not null && DateTime.Now - _lastFetch < TimeSpan.FromMinutes(5))
                return _cachedWeather;

            var data = await WeatherService.GetApiData();

            if (data is null)
                _cachedWeather = ["Wheater is unavailable."];
            else
                _cachedWeather = [$"Temperature today is {Math.Round(data.Main.Temp)}°C", $"and there is {data.Weather[0].MainDescription}."];
            _lastFetch = DateTime.Now;
            return _cachedWeather;
        }


        private static async Task ViewCart()
        {

        }

        private static async Task GoBack()
        {




        }

        private static async Task DisplayProductDeals()
        {
            using var scope = CustomerProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IProductService>();
            var productsWithDeals = await service.GetProductsWithDealsAsync();

            for (int i = 0; i < productsWithDeals.Count && i < 3; i++)
            {
                var product = productsWithDeals[i];
                List<string> productDetails = [$"{product.Name}", $"Price: {product.Price}", "Now on sale!"];
                new Window($"Offer {i + 1}", GetLeftPosition(i), 8, productDetails).Draw();
            }

            int GetLeftPosition(int i) => i switch
            {
                0 => 0,
                1 => 35,
                2 => 65,
                _ => 0
            };
        }
    }
}

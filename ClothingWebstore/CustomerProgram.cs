using ClothingWebstore.UIHelper;
using EFCore;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;

namespace ClothingWebstore
{
    public class CustomerProgram
    {
        private static IServiceProvider? CustomerProvider;

        private static readonly CheckoutSession _checkoutSession = new();

        private static readonly double _vatRate = 0.25;

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
                Console.SetCursorPosition(0, 13);
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
                        return;

                    default:
                        Message.PrintInvalidInput();
                        break;
                }
            }
        }

        private static async Task GoToProductPage()
        {
            using var scope = CustomerProvider!.CreateScope();
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
                        bool isSelected = globalIndex == selectedProductIndex;
                        var rows = new List<string>
                        {
                            FormatProductName(product, isSelected),
                            $"Price: {product.Price} $",
                            $"Description:"
                        };

                        string FormatProductName(Product product, bool isSelected)
                        {

                            string name = product.Name;
                            if (isSelected)
                            {
                                return $"> {product.Name}";

                            }

                            return $"{product.Name}";
                        }

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
                        return;

                    case ConsoleKey.Enter:
                        var selectedProduct = products[selectedProductIndex];

                        if (addtoCartMode)
                        {
                            _checkoutSession.Cart.AddItem(selectedProduct.Id);

                            Console.WriteLine($"{selectedProduct.Name} added to cart!");
                            Console.ReadKey(true);
                        }
                        else
                        {
                            ShowProductDetails(selectedProduct);
                        }
                        break;
                }

            } while (key != ConsoleKey.B);

            Console.SetCursorPosition(0, Lowest.LowestPosition + 5);

            Console.WriteLine("V  = Add to cart | B = Exit");
        }

        private static void ShowProductDetails(Product product)
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
                        _checkoutSession.Cart.AddItem(product.Id);

                        Console.Clear();
                        Console.WriteLine($"{product.Name} added to cart!");
                        Message.PressAnyKeyToContinue();
                        break;
                }

            } while (key != ConsoleKey.B);
        }



        private static List<string> WrapTextLimited(string text, int maxWidth, int maxLines = 1)
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
                _cachedWeather = ["Weather is unavailable."];
            else
                _cachedWeather = [$"Temperature today is {Math.Round(data.Main.Temp)}°C", $"and there is {data.Weather[0].MainDescription}."];
            _lastFetch = DateTime.Now;
            return _cachedWeather;
        }

        private static async Task ViewCart()
        {
            const int ListNumberColumnWidth = 4;
            const int NameColumnWidth = 32;
            const int QuantityColumnWidth = 8;
            const int PriceColumnWidth = 16;
            const int LineTotalColumnWidth = 16;

            const int TableWidth =
                ListNumberColumnWidth +
                NameColumnWidth +
                QuantityColumnWidth +
                PriceColumnWidth +
                LineTotalColumnWidth;

            const int TotalQuantityWidth =
                ListNumberColumnWidth +
                NameColumnWidth +
                QuantityColumnWidth;

            const int TotalTotalWidth =
                PriceColumnWidth +
                LineTotalColumnWidth;

            while (true)
            {
                var windowTextRows = new List<string>();

                windowTextRows.Add($"{"",TableWidth}");
                windowTextRows.Add(
                    $"{"",-ListNumberColumnWidth}" +
                    $"{"Product",-NameColumnWidth}" +
                    $"{"Quantity",QuantityColumnWidth}" +
                    $"{"Price",PriceColumnWidth}" +
                    $"{"Total",LineTotalColumnWidth}"
                );
                windowTextRows.Add($"{new string('\u2500', TableWidth)}");

                var cartLineItems = new List<string>();
                int cartLineItemNumber = 1;
                using var scope = CustomerProvider!.CreateScope();
                var productService = scope.ServiceProvider.GetRequiredService<IProductService>();

                int totalQuantity = 0;
                double totalTotal = 0;

                foreach (var cartLineItem in _checkoutSession.Cart.Items)
                {
                    Product? product = await productService.GetByIdAsync(cartLineItem.ProductId);
                    if (product != null)
                    {
                        string lineNumber = WrapTextLimited(cartLineItemNumber.ToString(), ListNumberColumnWidth)[0];
                        string lineProductName = WrapTextLimited(product.Name, NameColumnWidth)[0];
                        string lineQuantity = WrapTextLimited(cartLineItem.Quantity.ToString(), QuantityColumnWidth)[0];
                        totalQuantity += cartLineItem.Quantity;
                        string linePrice = WrapTextLimited(product.Price.ToString(), PriceColumnWidth)[0];
                        double total = product.Price * cartLineItem.Quantity;
                        string lineTotal = WrapTextLimited((product.Price * cartLineItem.Quantity).ToString(), LineTotalColumnWidth)[0];
                        totalTotal += total;

                        windowTextRows.Add(
                            $"{lineNumber,-ListNumberColumnWidth}" +
                            $"{lineProductName,-NameColumnWidth}" +
                            $"{lineQuantity,QuantityColumnWidth}" +
                            $"{linePrice,PriceColumnWidth}" +
                            $"{lineTotal,LineTotalColumnWidth}"
                        );

                        ++cartLineItemNumber;
                    }
                }

                windowTextRows.Add(
                    $"{"",-ListNumberColumnWidth}" +
                    $"{"",-NameColumnWidth}" +
                    $"{new string('\u2500', QuantityColumnWidth / 2),QuantityColumnWidth}" +
                    $"{"",PriceColumnWidth}" +
                    $"{new string('\u2500', LineTotalColumnWidth / 2),LineTotalColumnWidth}"
                );

                windowTextRows.Add(
                    $"{totalQuantity,TotalQuantityWidth}" +
                    $"{totalTotal,TotalTotalWidth}"
                );

                Console.Clear();
                Window window = new Window("Cart", 0, 0, windowTextRows);
                window.Draw();

                const int topOffset = 2;
                Console.SetCursorPosition(0, topOffset + windowTextRows.Count);

                Console.WriteLine();
                Console.WriteLine("[1] Continue to checkout");
                Console.WriteLine("[2] Edit item quantity");
                Console.WriteLine();
                Console.WriteLine("[b] Back");
                Console.WriteLine();
                Console.Write("Enter choice: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await HandleCheckoutStepFlow();
                        continue;

                    case "2":
                        if (_checkoutSession.Cart.Items.Count > 0)
                            ShowCartItemEditMenu();
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("The cart is empty.");
                            Message.PressAnyKeyToContinue();
                        }
                        continue;

                    case "b":
                    case "B":
                        return;

                    default:
                        Message.PrintInvalidInput();
                        continue;
                }
            }
        }

        private static void ShowCartItemEditMenu()
        {
            while (true)
            {
                Console.Write($"Specify item using list numbers (1-{_checkoutSession.Cart.Items.Count}): ");

                string? input = Console.ReadLine();

                int lineNumber;
                if (!int.TryParse(input, out lineNumber))
                {
                    Message.PrintInvalidInput();
                    continue;
                }

                if (lineNumber < 1 || lineNumber > _checkoutSession.Cart.Items.Count)
                {
                    Message.PrintInvalidInput();
                    continue;
                }

                Console.Write($"Set new quantity: ");
                input = Console.ReadLine();

                int quantity;
                if (!int.TryParse(input, out quantity) || quantity < 0)
                {
                    Message.PrintInvalidInput();
                    continue;
                }

                if (quantity == 0)
                    _checkoutSession.Cart.RemoveItemByIndex(lineNumber - 1);
                else
                    _checkoutSession.Cart.Items[lineNumber - 1].Quantity = quantity;

                break;
            }
        }

        private static async Task HandleCheckoutStepFlow()
        {
            var currentStep = CheckoutStep.Identification;

            while (true)
            {
                switch (currentStep)
                {
                    case CheckoutStep.Identification:
                        {
                            var result = await ShowCustomerIdentificationMenu();

                            switch (result)
                            {
                                case NavigationAction.Back:
                                    return;

                                case NavigationAction.Next:
                                    currentStep = CheckoutStep.Shipping;
                                    break;
                            }

                            break;
                        }
                    case CheckoutStep.Shipping:
                        {
                            var result = await ShowShippingOptionsMenu();

                            switch (result)
                            {
                                case NavigationAction.Back:
                                    currentStep = CheckoutStep.Identification;
                                    break;

                                case NavigationAction.Next:
                                    currentStep = CheckoutStep.Payment;
                                    break;
                            }

                            break;
                        }
                    case CheckoutStep.Payment:
                        {
                            var result = await ShowPaymentOptionsMenu();

                            switch (result)
                            {
                                case NavigationAction.Back:
                                    currentStep = CheckoutStep.Shipping;
                                    break;

                                case NavigationAction.Next:
                                    currentStep = CheckoutStep.Order;
                                    break;
                            }

                            break;
                        }
                    case CheckoutStep.Order:
                        {
                            await PlaceOrder();

                            return;
                        }
                }
            }
        }

        private static async Task<NavigationAction> ShowCustomerIdentificationMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[1] Log in");
                Console.WriteLine("[2] Register account to continue checkout process");
                Console.WriteLine("[b] Back");
                Console.Write("Enter choice: ");
                string? choice = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(choice))
                {
                    Message.PrintInvalidInput();
                    continue;
                }

                switch (choice)
                {
                    case "1":
                        bool loggedIn = await HandleLogin();

                        if (!loggedIn)
                            continue;

                        ShowCustomerAddress(_checkoutSession.Customer!);

                        Console.WriteLine();
                        Message.PressAnyKeyToContinue();

                        return NavigationAction.Next;

                    case "2":
                        EnterNewCustomerInfo();
                        return NavigationAction.Next;

                    case "b":
                    case "B":
                        return NavigationAction.Back;

                    default:
                        Message.PrintInvalidInput();
                        break;
                }
            }
        }
        private static async Task<List<Customer>> ListAllCustomersWithId()
        {
            using var scope = CustomerProvider!.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICustomerService>();
            var customers = await service.GetAllAsync();

            var rows = customers.Select(p => $"[{p.Id}] {p.Name}").ToList();
            new Window("Customers", 0, 0, rows).Draw();

            const int topOffset = 2;
            Console.SetCursorPosition(0, topOffset + rows.Count);

            return customers;
        }

        private static async Task<bool> HandleLogin()
        {
            using var scope = CustomerProvider!.CreateScope();
            var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

            while (true)
            {
                Console.Clear();
                await ListAllCustomersWithId();

                Console.Write("Enter customer ID or [b] for back: ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Message.PrintInvalidInput();
                    continue;
                }

                if (input.Equals("b", StringComparison.OrdinalIgnoreCase))
                    return false;

                if (!int.TryParse(input, out int customerId))
                {
                    Message.PrintInvalidInput();
                    continue;
                }

                var customer = await customerService.GetWithAddressesAsync(customerId);
                if (customer == null)
                {
                    Console.WriteLine("No customer with that ID exists.");
                    continue;
                }

                _checkoutSession.Customer = customer;
                var defaultAddress = customer.Addresses.FirstOrDefault();
                if (defaultAddress != null)
                {
                    _checkoutSession.Address = new ShippingAddress
                    {
                        Street = defaultAddress.Address.StreetAddress,
                        City = defaultAddress.Address.City,
                        Country = defaultAddress.Address.Country
                    };
                }

                return true;
            }
        }

        private static NavigationAction EnterNewCustomerInfo()
        {
            string name = "";
            string birthDate = "";
            string email = "";
            string phone = "";
            string street = "";
            string city = "";
            string country = "";

            var registrationFormFields = new List<(string FieldLabel, Func<string, bool> FieldInputValidation, Action<string> SetFieldValue)>
            {
                ("Name",        ValidateInput.IsValidName,      value => name = value),
                ("Birth date",  ValidateInput.IsValidBirthDate, value => birthDate = value),
                ("Email",       ValidateInput.IsValidEmail,     value => email = value),
                ("Phone",       ValidateInput.IsValidPhone,     value => phone = value),
                ("Street",      ValidateInput.IsValidAddress,   value => street = value),
                ("City",        ValidateInput.IsValidName,      value => city = value),
                ("Country",     ValidateInput.IsValidName,      value => country = value)
            };

            int index = 0;
            while (index < registrationFormFields.Count)
            {
                var registrationFormFieldRows = new List<string>
                {
                    $"Name:       {(string.IsNullOrWhiteSpace(name)         ? "_" : name)}",
                    $"Birth date: {(string.IsNullOrWhiteSpace(birthDate)    ? "_" : birthDate)}",
                    $"Email:      {(string.IsNullOrWhiteSpace(email)        ? "_" : email)}",
                    $"Phone:      {(string.IsNullOrWhiteSpace(phone)        ? "_" : phone)}",
                    $"Street:     {(string.IsNullOrWhiteSpace(street)       ? "_" : street)}",
                    $"City:       {(string.IsNullOrWhiteSpace(city)         ? "_" : city)}",
                    $"Country:    {(string.IsNullOrWhiteSpace(country)      ? "_" : country)}"
                };
                Console.Clear();
                new Window("Register Account", 0, 0, registrationFormFieldRows).Draw();

                var field = registrationFormFields[index];

                Console.SetCursorPosition(0, 10);
                string input = GetValidatedInput(field.FieldLabel, field.FieldInputValidation);
                field.SetFieldValue(input);
                ++index;
            }

            var customer = new Customer
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
            };

            _checkoutSession.Customer = customer;

            return NavigationAction.Next;
        }

        private static void ShowCustomerAddress(Customer customer)
        {
            var address = customer.Addresses?.FirstOrDefault()?.Address;
            var rows = new List<string>
            {
                $"Name:     {customer.Name          ?? "N/A"}",
                $"Street:   {address?.StreetAddress ?? "N/A"}",
                $"City:     {address?.City          ?? "N/A"}",
                $"Country:  {address?.Country       ?? "N/A"}"
            };

            Console.Clear();
            new Window("Shipping", 0, 0, rows).Draw();
        }

        private static async Task<NavigationAction> ShowShippingOptionsMenu()
        {
            const int ListNumberColumnWidth = 4;
            const int NameColumnWidth = 32;
            const int PriceColumnWidth = 16;
            const int DeliveryTimeColumnWidth = 24;
            const int TableWidth =
                ListNumberColumnWidth +
                NameColumnWidth +
                PriceColumnWidth +
                DeliveryTimeColumnWidth;

            var windowTextRows = new List<string>();

            windowTextRows.Add($"{"",TableWidth}");
            windowTextRows.Add(
                $"{"",-ListNumberColumnWidth}" +
                $"{"Name",-NameColumnWidth}" +
                $"{"Price",PriceColumnWidth}" +
                $"{"Delivery time",DeliveryTimeColumnWidth}"
            );
            windowTextRows.Add(new string('\u2500', TableWidth));

            using var scope = CustomerProvider!.CreateScope();
            var shippingService = scope.ServiceProvider.GetRequiredService<IShippingService>();

            var shippingOptions = await shippingService.GetAllAsync();

            int shippingOptionNumber = 1;

            foreach (var option in shippingOptions)
            {
                string name = WrapTextLimited(option.Name, NameColumnWidth)[0];
                string price = WrapTextLimited(option.Price.ToString(), PriceColumnWidth)[0];

                windowTextRows.Add(
                    $"{shippingOptionNumber,-ListNumberColumnWidth}" +
                    $"{name,-NameColumnWidth}" +
                    $"{price,PriceColumnWidth}"
                );

                ++shippingOptionNumber;
            }

            Console.Clear();

            new Window("Shipping Options", 0, 0, windowTextRows).Draw();

            while (true)
            {
                Console.SetCursorPosition(0, 7);

                Console.WriteLine();
                Console.WriteLine($"Enter number (1-{shippingOptions.Count}) to select shipping method");
                Console.WriteLine();
                Console.WriteLine("[b] Back");
                Console.WriteLine();
                Console.Write("Enter choice: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "b":
                    case "B":
                        return NavigationAction.Back;
                }

                if (int.TryParse(choice, out int index))
                {
                    if (index >= 1 && index <= shippingOptions.Count)
                    {
                        _checkoutSession.Shipping = shippingOptions[index - 1];
                        return NavigationAction.Next;
                    }
                }

                Message.PrintInvalidInput();
            }
        }

        private static async Task<NavigationAction> ShowPaymentOptionsMenu()
        {
            const int ListNumberColumnWidth = 4;
            const int NameColumnWidth = 32;
            const int QuantityColumnWidth = 8;
            const int PriceColumnWidth = 16;
            const int LineTotalColumnWidth = 16;
            const int TableWidth =
                ListNumberColumnWidth +
                NameColumnWidth +
                QuantityColumnWidth +
                PriceColumnWidth +
                LineTotalColumnWidth;

            var orderSummaryWindowTextRows = new List<string>();

            orderSummaryWindowTextRows.Add($"{"", TableWidth}");
            orderSummaryWindowTextRows.Add(
                $"{"", -ListNumberColumnWidth}" +
                $"{"Product", -NameColumnWidth}" +
                $"{"Quantity", QuantityColumnWidth}" +
                $"{"Price", PriceColumnWidth}" +
                $"{"Total", LineTotalColumnWidth}"
            );
            orderSummaryWindowTextRows.Add(new string('\u2500', TableWidth));

            using var scope = CustomerProvider!.CreateScope();
            var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

            double subtotal = 0;
            foreach (var item in _checkoutSession.Cart.Items)
            {
                var product = await productService.GetByIdAsync(item.ProductId);
                if (product == null)
                    continue;

                double lineTotal = product.Price * item.Quantity;
                subtotal += lineTotal;

                orderSummaryWindowTextRows.Add(
                    $"{"", -ListNumberColumnWidth}" +
                    $"{product.Name, -NameColumnWidth}" +
                    $"{item.Quantity, QuantityColumnWidth}" +
                    $"{"$" + product.Price, PriceColumnWidth}" +
                    $"{"$" + lineTotal, LineTotalColumnWidth}"
                );
            }
            orderSummaryWindowTextRows.Add(new string('\u2500', TableWidth));

            double shippingCost = _checkoutSession.Shipping!.Price;
            double vat = subtotal * _vatRate;
            double total = subtotal + vat + shippingCost;
            orderSummaryWindowTextRows.Add($"{$"Subtotal: ${subtotal}", TableWidth}");
            orderSummaryWindowTextRows.Add($"{$"Shipping: ${shippingCost}", TableWidth}");
            orderSummaryWindowTextRows.Add($"{$"VAT ({_vatRate * 100}%): ${vat}", TableWidth}");
            orderSummaryWindowTextRows.Add($"{$"Total (incl. VAT): ${total}", TableWidth}");

            Console.Clear();
            new Window("Cart", 0, 0, orderSummaryWindowTextRows).Draw();

            var paymentOptions = await paymentService.GetAllAsync();
            var paymentOptionsWindowTextRows = new List<string>();
            int paymentOptionNumber = 1;
            foreach (var option in paymentOptions)
            {
                paymentOptionsWindowTextRows.Add($"{paymentOptionNumber, -ListNumberColumnWidth}{option.Type}");

                paymentOptionNumber++;
            }

            int topOffset = orderSummaryWindowTextRows.Count + 2;
            new Window("Payment Options", 0, topOffset, paymentOptionsWindowTextRows).Draw();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"Enter number (1-{paymentOptions.Count}) to select payment option");
                Console.WriteLine();
                Console.WriteLine("[b] Back");
                Console.WriteLine();
                Console.Write("Enter choice: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "b":
                    case "B":
                        return NavigationAction.Back;
                }

                if (int.TryParse(choice, out int index))
                {
                    if (index >= 1 && index <= paymentOptions.Count)
                    {
                        _checkoutSession.Payment = paymentOptions[index - 1];
                        return NavigationAction.Next;
                    }
                }

                Message.PrintInvalidInput();
            }
        }

        private static async Task PlaceOrder()
        {
            using var scope = CustomerProvider!.CreateScope();
            var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

            if (_checkoutSession.Customer!.Id == 0)
                await customerService.AddAsync(_checkoutSession.Customer);

            var order = new Order
            {
                CustomerId = _checkoutSession.Customer.Id,
                ShippingId = _checkoutSession.Shipping!.Id,
                PaymentId = _checkoutSession.Payment!.Id
            };

            foreach (var cartLineItem in _checkoutSession.Cart.Items)
            {
                var orderProduct = new OrderProduct()
                {
                    ProductId = cartLineItem.ProductId,
                    ProductAmount = cartLineItem.Quantity
                };

                order.OrderProducts.Add(orderProduct);
            }

            order.OrderNumber = "0000000000";

            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
            await orderService.AddAsync(order);

            _checkoutSession.Cart.EmptyOut();
        }

        private static string GetValidatedInput(string property, Func<string, bool> validate)
        {
            while (true)
            {
                Console.Write($"{property}: ");
                string? input = Console.ReadLine();

                if (validate(input!))
                    return input!;

                Message.PrintInvalidInput();
            }
        }

        private static async Task DisplayProductDeals()
        {
            using var scope = CustomerProvider!.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IProductService>();
            var productsWithDeals = await service.GetProductsWithDealsAsync();

            int currentLeft = 0;
            int spacing = 2;

            for (int i = 0; i < productsWithDeals.Count && i < 3; i++)
            {
                var product = productsWithDeals[i];
                List<string> productDetails = [$"{product.Name}", $"Price: {product.Price}$", "Now on sale!"];

                int maxLength = productDetails.Max(s => s.Length);
                int boxWidth = maxLength + 4;

                new Window($"Offer {i + 1}", currentLeft, 8, productDetails).Draw();

                currentLeft += boxWidth + spacing;
            }
        }
    }
}

using ClothingWebstore.UIHelper;

using EFCore;
using EFCore.Repositories;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Services;
using Services.Interfaces;

namespace ClothingWebstore
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var services = new ServiceCollection();

            services.AddDbContext<WebshopDbContext>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBrandService, BrandService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            var provider = services.BuildServiceProvider();

            while (true)
            {
                Console.Clear();
                new Window("Role", 0, 0, Menu.ReturnGeneralStartMenuList()).Draw();
                new Window("Navigation", 40, 0, Menu.ReturnGeneralNavigationList()).Draw();
                ConsoleKeyInfo role = Console.ReadKey(true);

                switch (role.Key)
                {
                    case ConsoleKey.Enter:
                    case ConsoleKey.C:
                        await CustomerProgram.RunCustomer(provider);
                        break;

                    case ConsoleKey.A:
                        await AdminProgram.RunAdmin(provider);
                        break;

                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.Clear();
                        break;
                }
            }
        }
    }
}

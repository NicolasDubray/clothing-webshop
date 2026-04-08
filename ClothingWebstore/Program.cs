using ClothingWebstore.UIHelper;

namespace ClothingWebstore
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(Menu.ReturnGeneralStartMenu());
                ConsoleKeyInfo role = Console.ReadKey(true);

                switch (role.Key)
                {
                    case ConsoleKey.Enter:
                    case ConsoleKey.C:
                        CustomerProgram.RunCustomer();
                        break;

                    case ConsoleKey.A:
                        AdminProgram.RunAdmin();
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

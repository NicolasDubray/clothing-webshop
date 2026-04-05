using ClothingWebstore.UIHelper;

namespace ClothingWebstore
{
    public class AdminProgram
    {
        public static async Task RunAdmin(IServiceProvider provider)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(Menu.ReturnAdminStartMenu());
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageProducts();
                        break;

                    case "2":
                        ManageCategories();
                        break;

                    case "3":
                        ManageCustomers();
                        break;

                    case "4":
                        SeeStatistics();
                        break;

                    case "B":
                    case "b":
                        return;

                    default:
                        Console.WriteLine("Invalid.");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }
        private static void ManageProducts()
        {

        }

        private static void ManageCategories()
        {

        }

        private static void ManageCustomers()
        {

        }

        private static void SeeStatistics()
        {

        }
    }
}

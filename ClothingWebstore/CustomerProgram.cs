using ClothingWebstore.UIHelper;

namespace ClothingWebstore
{
    public class CustomerProgram
    {
        public static void RunCustomer()
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












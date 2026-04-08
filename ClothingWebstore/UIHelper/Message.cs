using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingWebstore.UIHelper
{
    internal class Message
    {
        internal static void InvalidInput()
        {
            Console.WriteLine("Invalid. Try again.");
            Thread.Sleep(1000);
        }

        internal static void PressAnyKeyToContinue()
        {
            Console.WriteLine($"{Environment.NewLine}Press any key to continue.{Environment.NewLine}");
            Console.ReadKey(true);
        }
    }
}

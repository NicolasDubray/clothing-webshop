using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingWebstore.UIHelper
{
    internal class Menu
    {
        public static string ReturnStartMenu()
        {
            return """
                Are you a customer / admin?
                [C] Customer
                [A] Admin
                [Q] Quit

                """;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingWebstore.UIHelper
{
    internal class Menu
    {
        public static string ReturnGeneralStartMenu()
        {
            return """
                Are you a customer / admin?
                [C] Customer
                [A] Admin
                [Q] Quit

                """;
        }
        public static string ReturnAdminStartMenu()
        {
            return """
                What would you like to manage?
                Press key + enter.
                [1] Products
                [2] Categories
                [3] Customer
                [4] Statistics

                [B] Back

                """;
        }
    }
}

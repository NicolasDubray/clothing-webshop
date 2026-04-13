using Entities;
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

        internal static List<string> ReturnGeneralStartMenuList()
        {
            return ["Are you a customer / admin?", "[C] Customer", "[A] Admin"];
        }

        internal static List<string> ReturnGeneralNavigationList()
        {
            return ["Press key", "[Q] Quit"];
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

        internal static List<string> ReturnAdminStartList()
        {
            return ["What would you like to manage?", "[1] Products", "[2] Categories", "[3] Customer", "[4] Statistics", "[5] Product deals"];
        }

        public static string ReturnCustomerMenu()
        {
            return """
               
                [1] Go to Products
                [2] View Cart

                [B] Back
                """;


        }

        internal static List<string> ReturnCustomerMenuList()
        {
            return ["Welcome to Stack over fits!", "", "What would you like to do?", "[1] View product page", "[2] View cart"];
        }

        internal static List<string> ReturnInstructionList()
        {
            return ["Press key + enter", "[B] Back"];
        }

        internal static List<string> ReturnManageCustomerList()
        {
            return ["What would you like to do?", "[1] Manage an existing customer", "[2] Add new customer", "[3] Remove a customer", "[4] List all existing customers"];
        }

        internal static List<string> ReturnInstructionStatisticsList()
        {
            return ["Press key", "[1] Best selling products", "[2] Total revenue", "[3] Top buying customers", "[4] Best selling categories", "[B] Back"];
        }

        internal static List<string> ReturnSimpleTextList(string text)
        {
            return [$"{text}"];
        }

        internal static string ReturnCustomerDetailsMenu(Customer customer)
        {
            var address = customer.Addresses.FirstOrDefault()?.Address;
            return ($"""
                    [1] Name: {customer.Name}
                    [2] Birth date: {customer.BirthDate:yyyy-MM-dd}
                    [3] Email: {customer.Email}
                    [4] Phone: {customer.Phone}
                    [5] Street: {address!.StreetAddress}
                    [6] City: {address!.City}
                    [7] Country: {address!.Country}

                    [8] See order history

                    """);
        }

        internal static List<string> ReturnCustomerDetailsList(Customer customer)
        {
            var address = customer.Addresses?.FirstOrDefault()?.Address;
            return [
                $"[1] Name: {customer.Name}", 
                $"[2] Birth date: {customer.BirthDate:yyyy-MM-dd}", 
                $"[3] Email: {customer.Email}",
                $"[4] Phone: {customer.Phone}",
                $"[5] Street: {address!.StreetAddress}",
                $"[6] City: {address!.City}",
                $"[7] Country: {address!.Country}",
                $"[8] See order history"
                ];
        }
    }
}

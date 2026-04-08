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

        public static string ReturnCustomerMenu()
        {
            return """
               
                [1] Go to Products
                [2] View Cart

                [B] Go Back
                """;


        }

        internal static List<string> ReturnInstructionList()
        {
            return ["Press digit + enter", "[B] Back"];
        }

        internal static List<string> ReturnManageCustomerList()
        {
            return ["What would you like to do?", "[1] Manage an existing customer", "[2] Add new customer", "[3] Remove a customer", "[4] List all existing customers"];
        }

        internal static List<string> ReturnManageQuestionList(string action)
        {
            return [$"Who would you like to {action}?"];
        }

        internal static string ReturnCustomerDetailsMenu(Customer customer)
        {
            var address = customer.Addresses.FirstOrDefault()?.Address;
            return ($"""
                    What would you like to change?
                    Press key + enter and write in new value.

                    [1] Name : {customer.Name}
                    [2] Birth date: {customer.BirthDate:yyyy-MM-dd}
                    [3] Email: {customer.Email}
                    [4] Phone: {customer.Phone}
                    [5] Street: {address!.StreetAddress}
                    [6] City: {address!.City}
                    [7] Country: {address!.Country}

                    [8] See order history

                    [B] Back

                    """);
        }
    }
}

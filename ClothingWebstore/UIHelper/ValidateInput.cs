using Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ClothingWebstore.UIHelper
{
    internal class ValidateInput
    {
        internal static bool IsValidCustomerId(string input, List<Customer> customers) =>
            int.TryParse(input, out int id) && customers.Any(c => c.Id == id);
        internal static bool IsValidProductId(string input, List<Product> products) =>
            int.TryParse(input, out int id) && products.Any(c => c.Id == id);
        internal static bool IsValidCategoryId(string input, List<Category> categories) =>
            int.TryParse(input, out int id) && categories.Any(c => c.Id == id);

        internal static bool IsValidName(string name) =>
            name.Length > 2 && name.Length < 64
            && !string.IsNullOrWhiteSpace(name)
            && name.All(c => char.IsLetter(c) || c == ' ');

        internal static bool IsValidAddress(string input) =>
            !string.IsNullOrWhiteSpace(input) && input.Length >= 3;

        internal static bool IsValidBirthDate(string birthDate) =>
            DateTime.TryParseExact(birthDate, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime date)
            && date <= DateTime.Today
            && date >= DateTime.Today.AddYears(-130);

        internal static bool IsValidEmail(string email) =>
            email.Length > 4 && email.Length <= 64
            && email.IndexOf('@') > 0
            && email.IndexOf('@') < email.Length - 1;

        internal static bool IsValidPhone(string phone) =>
            phone is not null && phone.Length == 10 && phone.All(char.IsDigit);


        // Product Side Down Here

        internal static bool IsValidProId(string input, List<Product> products) =>
            int.TryParse(input, out int id) && products.Any(p => p.Id == id);

        internal static bool ProNameIsValid(string name) =>
            name.Length > 2 && name.Length < 24
            && !string.IsNullOrWhiteSpace(name);

        internal static bool ProPriceIsValid(string inputPrice) =>
            inputPrice is not null && inputPrice.All(char.IsDigit) 
            && double.TryParse(inputPrice, out double price)
            && price > 0;

        internal static bool ProShortDescriptionIsValid(string shortDescr) =>
            shortDescr.Length > 0 && shortDescr.Length < 100
            && shortDescr is not null;

        internal static bool ProLongDescriptionIsValid(string longDescr) =>
            longDescr.Length >= 0 && longDescr.Length <= 500
            && longDescr is not null;
    }
}

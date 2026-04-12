using Entities;
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

        internal static bool IsValidName(string name) =>
            name.Length > 0 && name.Length < 64
            && name.All(c => char.IsLetter(c) || c == ' ');

        internal static bool IsValidAddress(string? input) =>
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
    }
}

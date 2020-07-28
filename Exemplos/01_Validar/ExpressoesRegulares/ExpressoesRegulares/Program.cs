using System;
using System.Text.RegularExpressions;

namespace ExpressoesRegulares
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] zipCodes = { "1234AB", "1234 AB", "1001 AB", "03345-001" };

            foreach (var zipCode in zipCodes)
            {
                Console.WriteLine(ValidateZipCode(zipCode));
                Console.WriteLine(ValidateZipCodeRegEx(zipCode));
            }

            RegexOptions options = RegexOptions.None; Regex regex = new Regex(@"[ ]{2,}", options);
            string input = "1       2          3  4       5";
            string result = regex.Replace(input, " ");
            Console.WriteLine(result); // 1 2 3 4 5 

            Console.ReadKey();
        }

        static bool ValidateZipCode(string zipCode)
        {
            // Valid zipcodes: 1234AB | 1234 AB | 1001 AB 
            if (zipCode.Length < 6) return false;
            string numberPart = zipCode.Substring(0, 4);
            int number;

            if (!int.TryParse(numberPart, out number))
                return false;

            string characterPart = zipCode.Substring(4);

            if (numberPart.StartsWith("0")) return false;
            if (characterPart.Trim().Length < 2) return false;
            if (characterPart.Length == 3 && characterPart.Trim().Length != 2)
                return false;

            return true;
        }

        static bool ValidateZipCodeRegEx(string zipCode)
        {
            Match match = Regex.Match(zipCode, @"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$", RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}

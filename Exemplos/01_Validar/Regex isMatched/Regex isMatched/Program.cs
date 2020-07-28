using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Regex_isMatched
{
    class Program
    {
        static void Main(string[] args)
        {
            //Pattern for Matching Pakistan's Phone Number
            string Phonepattern = @"\(\+92\)\s\d{3}-\d{3}-\d{4}";

            string inputPhone = "(+92) 336-071-7272";
            bool isMatched = Regex.IsMatch(inputPhone, Phonepattern);
            if (isMatched == true)
                Console.WriteLine("Pattern for phone number is matched with inputStr");
            else
                Console.WriteLine("Pattern for phone number is not matched with inputStr");

            //Pattern for Matching an email id
            string emailpattern = @"^\w+[a-zA-Z0-9]+([-._][a-z0-9]+)*@([a-z0-9]+)\.\w{2,4}";

            string inputemail = "imaliasad@outlook.com";
            isMatched = Regex.IsMatch(inputemail, emailpattern);

            if (isMatched == true)
                Console.WriteLine("Pattern for email id is matched with inputStr");
            else
                Console.WriteLine("Pattern for email isn't matched with inputStr");



            Console.ReadKey();

        }

        private static void Regex_Options()
        {
            string pattern0 = @"d \w+ \s";
            string input0 = "Dogs are decidedly good pets.";
            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;

            foreach (Match match in Regex.Matches(input0, pattern0, options))
                Console.WriteLine("'{0}// found at index {1}.", match.Value, match.Index);
            // The example displays the following output:
            //    'Dogs // found at index 0.
            //    'decidedly // found at index 9.


            string pattern1 = @"(?ix) d \w+ \s";
            string input1 = "Dogs are decidedly good pets.";

            foreach (Match match in Regex.Matches(input1, pattern1))
                Console.WriteLine("'{0}// found at index {1}.", match.Value, match.Index);
            // The example displays the following output:
            //    'Dogs // found at index 0.
            //    'decidedly // found at index 9.


            string pattern2 = @"\b(?ix: d \w+)\s";
            string input2 = "Dogs are decidedly good pets.";

            foreach (Match match in Regex.Matches(input2, pattern2))
                Console.WriteLine("'{0}// found at index {1}.", match.Value, match.Index);
            // The example displays the following output:
            //    'Dogs // found at index 0.
            //    'decidedly // found at index 9.



        }

        private static void ValidateColor(string txt, bool allowBlank, string pattern)
        {
            // Assume it's invalid.
            bool valid = false;
            var BackColor = new Color();
            var options = new RegexOptions();

            // If the text is blank, allow it.
            string text = txt;
            if (allowBlank && (text.Length == 0)) valid = true;
            // If the regular expression matches the text, allow it.
            if (Regex.IsMatch(text, pattern)) valid = true;
            // Display the appropriate background color.
            if (valid) BackColor = SystemColors.Window;
            else BackColor = Color.Yellow;
        }


        // Perform simple validation for a 7-digits US phone number.
        private void ValidatePhone(string phone7TextBox)
        {
            const string pattern = @"^\d{3}-\d{4}$";
            bool valid = false;
            var BackColor = new Color();

            string text = phone7TextBox;
            if (text.Length == 0) valid = true;
            if (Regex.IsMatch(text, pattern)) valid = true;
            if (valid) BackColor = SystemColors.Control;
            else BackColor = Color.Yellow;
        }
    }
}

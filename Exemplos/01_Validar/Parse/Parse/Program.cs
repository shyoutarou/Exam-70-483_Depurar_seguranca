using System;
using System.Globalization;

namespace Parse
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                string value = "true";
                bool b = bool.Parse(value);
                Console.WriteLine(b); //True

                string valuetry = "1";
                int result;
                bool success = int.TryParse(valuetry, out result);
                if (success)
                {
                    Console.WriteLine("value is a valid integer");
                }
                else
                {
                    Console.WriteLine("value is not a valid integer");
                }

                CultureInfo english = new CultureInfo("En");
                CultureInfo holandês = new CultureInfo("Nl");
                string valor = "€19,95";
                decimal d = decimal.Parse(valor, NumberStyles.Currency, holandês);
                Console.WriteLine(d.ToString(english)); // Exibe 19,95

                valor = "-19,95";
                decimal cost;
                if (!decimal.TryParse(valor, NumberStyles.Currency,
                        CultureInfo.CurrentCulture, out cost))
                {
                    // Display an error message.
                    Console.WriteLine(d.ToString("Cost is not a valid currency value"));
                }

                Console.ReadKey();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

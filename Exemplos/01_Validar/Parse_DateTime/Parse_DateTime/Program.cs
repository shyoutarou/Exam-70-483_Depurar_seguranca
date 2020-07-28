using System;
using System.Globalization;

namespace Parse_DateTime
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define string representations of a date to be parsed.
            string[] dateStrings = { "01/10/2009 7:34 PM", "10.01.2009 19:34", "10-1-2009 19:34" };

            Parse_IFormatProvider(dateStrings);

            foreach (var data in dateStrings)
            {
                Parse_String(data);
                Parse_DateTimeStyles(data);
            }

            Console.ReadKey();
        }

        public static void Parse_String(string dateString)
        {
            // Suponha que a cultura atual seja fr-FR.(igual pt-BR)
            // A data é 16 de fevereiro de 2008, 12 horas, 15 minutos e 12 segundos.
            // Use o valor padrão de data e hora nos fr-FR.(igual pt-BR)
            DateTime dateValue;

            try
            {
                dateValue = DateTime.Parse(dateString);
                Console.WriteLine("Data c/tempo fr-FR: '{0}' convertido em {1}.", dateString, dateValue);

                // Inverta mês e dia para se adaptar à cultura en-US.
                dateString = "2/16/2008 12:15:12";
                dateValue = DateTime.Parse(dateString);
                Console.WriteLine("Data c/tempo en-US: '{0}' convertido em {1}.", dateString, dateValue);

                // Chame outra sobrecarga do Parse para converter a string com êxito
                // formatado de acordo com as convenções da cultura en-US
                dateValue = DateTime.Parse(dateString, new CultureInfo("en-US", false));
                Console.WriteLine("Data c/tempo en-US: '{0}' convertido em {1}.", dateString, dateValue);

                // Analisar string com data, mas sem componente de hora.
                dateString = "16/02/2008";
                dateValue = DateTime.Parse(dateString);
                Console.WriteLine("Data s/tempo: '{0}' convertido em {1}.", dateString, dateValue);
            }
            catch (FormatException)
            {
                Console.WriteLine("Não foi possível converter '{0}'.", dateString);
            }
        }

        public static void Parse_IFormatProvider(string[] dateStrings)
        {
            // Define cultures to be used to parse dates.
            CultureInfo[] cultures = {CultureInfo.CreateSpecificCulture("en-US"),
                    CultureInfo.CreateSpecificCulture("fr-FR"),
                    CultureInfo.CreateSpecificCulture("de-DE"),
                    CultureInfo.CreateSpecificCulture("pt-BR")};

            // Parse dates using each culture.
            foreach (CultureInfo culture in cultures)
            {
                DateTime dateValue;
                Console.WriteLine("Attempted conversions using {0} culture.", culture.Name);
                foreach (string dateString in dateStrings)
                {
                    try
                    {
                        dateValue = DateTime.Parse(dateString, culture);
                        Console.WriteLine("Converted '{0}' to {1}.",
                                    dateString, dateValue.ToString("f", culture));
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Unable to convert '{0}' for culture {1}.",
                                    dateString, culture.Name);
                    }
                }
                Console.WriteLine();
            }
        }

        public static void Parse_DateTimeStyles(string dateString)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTimeStyles styles = DateTimeStyles.None;
            DateTime result;

            try
            {
                result = DateTime.Parse(dateString, culture, styles);
                Console.WriteLine("{0} converted to {1} {2}.",
                            dateString, result, result.Kind.ToString());

                // Parse the same date and time with the AssumeLocal style.
                styles = DateTimeStyles.AssumeLocal;
                result = DateTime.Parse(dateString, culture, styles);
                Console.WriteLine("{0} converted to {1} {2}.",
                            dateString, result, result.Kind.ToString());

                // Parse a date and time that is assumed to be local.
                // This time is five hours behind UTC. The local system's time zone is 
                // eight hours behind UTC.
                dateString = "2009/03/01T10:00:00-5:00";
                styles = DateTimeStyles.AssumeLocal;
                result = DateTime.Parse(dateString, culture, styles);
                Console.WriteLine("{0} converted to {1} {2}.",
                            dateString, result, result.Kind.ToString());

                // Attempt to convert a string in improper ISO 8601 format.
                dateString = "03/01/2009T10:00:00-5:00";
                result = DateTime.Parse(dateString, culture, styles);
                Console.WriteLine("{0} converted to {1} {2}.",
                            dateString, result, result.Kind.ToString());

                // Assume a date and time string formatted for the fr-FR culture is the local 
                // time and convert it to UTC.
                dateString = "2008-03-01 10:00";
                culture = CultureInfo.CreateSpecificCulture("fr-FR");
                styles = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal;

                result = DateTime.Parse(dateString, culture, styles);
                Console.WriteLine("{0} converted to {1} {2}.",
                            dateString, result, result.Kind.ToString());
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to convert {0} to a date and time.", dateString);
            }
        }
    }
}



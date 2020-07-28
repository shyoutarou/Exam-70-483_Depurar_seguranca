using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace JSON_e_XML
{
    class Program
    {
        private class Teacher
        {
            private int id { get; set; }
            public string name { get; set; }
            public long salary { get; set; }
        }

        static void Main(string[] args)
        {
            // Criou a instância e inicializou
            Teacher professor = new Teacher()
            {
                name = "Raimundo Nonato",
                salary = 1000,
            };

            JavaScriptSerializer dataContract = new JavaScriptSerializer();
            string serializedDataInStringFormat = dataContract.Serialize(professor);
            Console.WriteLine("A serialização JavaScript foi concluída!");
            //serializedDataInStringFormat = "ddd";
            IsJson(serializedDataInStringFormat);

            ValidateXML();

            Console.ReadKey();
        }

        public static bool IsJson(string json)
        {
            try
            {
                json = json.Trim();
                var result = json.StartsWith("{") && json.EndsWith("}") ||
                                json.StartsWith("[") && json.EndsWith("]");

                Console.WriteLine(result);

                var serializer = new JavaScriptSerializer();
                var deserialize = serializer.Deserialize<Dictionary<string, object>>(json);

                Console.WriteLine(deserialize);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void ValidateXML()
        {
            string xsdPath = "person.xsd";
            string xmlPath = "person.xml";
            XmlReader reader = XmlReader.Create(xmlPath);
            XmlDocument document = new XmlDocument();
            document.Schemas.Add("", xsdPath);
            document.Load(reader);
            ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
            document.Validate(eventHandler);
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    break;
            }
        }
    }
}

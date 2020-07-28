using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace XMLWriter_Tracelistener
{
    class Program
    {
        static void Main(string[] args)
        {
            string ne_fileName = "NotEscaped.xml";
            string es_fileName = "Escaped.xml";
            string event_name = "xmlwriter_listener";
            string testString = "<Test><InnerElement Val=\"1\" /><InnerElement Val=\"Data\"/><AnotherElement>11</AnotherElement></Test>";

            File.Delete(ne_fileName);
            File.Delete(es_fileName);

            //specify the XmlWriter trace listener
            using (var xmlwriter_listener = new XmlWriterTraceListener(ne_fileName, event_name))
            {
                TraceSource ts = new TraceSource("TestSource");

                ts.Listeners.Add(xmlwriter_listener);
                ts.Switch.Level = SourceLevels.All;

                XmlTextReader myXml = new XmlTextReader(new StringReader(testString));
                XPathDocument xDoc = new XPathDocument(myXml);
                XPathNavigator myNav = xDoc.CreateNavigator();
                ts.TraceData(TraceEventType.Error, 38, myNav);

                ts.Flush();
                ts.Close();
            }

            using (var xmlwriter_listener = new XmlWriterTraceListener(es_fileName, event_name))
            {
                TraceSource ts2 = new TraceSource("TestSource2", SourceLevels.All);
                ts2.Listeners.Add(xmlwriter_listener);
                ts2.TraceData(TraceEventType.Error, 38, testString);

                ts2.Flush();
                ts2.Close();
            }

            Console.ReadLine();
        }
    }
}

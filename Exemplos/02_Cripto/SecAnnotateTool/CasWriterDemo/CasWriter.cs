using System;
using System.Reflection;
using System.Security;
using System.Text;

[assembly: AllowPartiallyTrustedCallers()]
namespace CasWriterDemo
{
    //[SecurityCritical()]
    public class CasWriter
    {
        /// <summary>
        /// Write a sentence to console
        /// </summary>
        /// <param id="text""></param>
        [SecurityCritical()]
        public virtual void WriteCustomSentence(string text)
        {
            Console.WriteLine(text + "\n");
        }
        /// <summary>
        /// Write a sentence to console
        /// </summary>
        /// <param id="text""></param>
        [SecuritySafeCritical()]
        public void WriteDefaultSentence(int index)
        {
            switch (index)
            {
                case 0:
                    WriteCustomSentence("homo homini lupus");
                    break;
                case 1:
                    WriteCustomSentence("melius abundare quam deficere");
                    break;
                case 2:
                    WriteCustomSentence("audaces fortuna iuvat");
                    break;
            }
        }
        /// <summary>
        /// Get the Security status of each method developed
        /// </summary>
        public string GetMethodsSecurityStatus()
        {
            //get the MethodInfo of each method
            MethodInfo[] infos = GetType().GetMethods();
            StringBuilder sb = new StringBuilder();
            foreach (MethodInfo m in infos)
            {
                if (m.ReturnType != typeof(void)) continue;
                sb.Append("\n");
                sb.Append(m.Name + ": ");
                if (m.IsSecurityCritical)
                {
                    sb.AppendFormat("Method IsSecurityCritical: {0} \n", m.IsSecurityCritical);
                }
                else if (m.IsSecuritySafeCritical)
                {
                    sb.AppendFormat("Method IsSecuritySafeCritical: {0} \n", m.IsSecuritySafeCritical);
                }
                else if (m.IsSecurityTransparent)
                {
                    sb.AppendFormat("Method IsSecurityTransparent: {0} \n", m.IsSecurityTransparent);
                }
            }
            return sb.ToString();
        }
    }
}


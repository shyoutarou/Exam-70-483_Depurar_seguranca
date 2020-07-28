using System;
using System.Reflection;
using System.Security;
using System.Text;


[assembly: SecurityRules(SecurityRuleSet.Level2)]
//[assembly: AllowPartiallyTrustedCallers()]
//[assembly: SecurityTransparent()] 
namespace CAS_01
{
    public class AssemblyInfo : MarshalByRefObject
    {
        /// <summary>
        /// Write to the console the security settings of the assembly
        /// </summary>
        public string GetCasSecurityAttributes()
        {
            //gets the reference to the current assembly
            Assembly a = Assembly.GetExecutingAssembly();

            StringBuilder sb = new StringBuilder();

            //show the transparence level
            sb.AppendFormat("Security Rule Set: {0} \n\n", a.SecurityRuleSet);

            //show if it is full trusted
            sb.AppendFormat("Is Fully Trusted: {0} \n\n", a.IsFullyTrusted);

            //get the type for the main class of the assembly
            Type t = a.GetType("CAS_01.AssemblyInfo");

            //show if the class is Critical,Transparent or SafeCritical
            sb.AppendFormat("Class IsSecurityCritical: {0} \n", t.IsSecurityCritical);
            sb.AppendFormat("Class IsSecuritySafeCritical: {0} \n", t.IsSecuritySafeCritical);
            sb.AppendFormat("Class IsSecurityTransparent: {0} \n", t.IsSecurityTransparent);

            try
            {
                sb.AppendFormat("\nPermissions Count: {0} \n", a.PermissionSet.Count);
            }
            catch (Exception ex)
            {
                sb.AppendFormat("\nError while trying to get the Permission Count: {0} \n", ex.Message);
            }

            return sb.ToString();

        }
    }
}



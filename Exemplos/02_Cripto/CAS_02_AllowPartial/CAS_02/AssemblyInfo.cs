using System;
using System.Reflection;
using System.Security;
using System.Text;

[assembly: AllowPartiallyTrustedCallers()]
//[assembly: SecurityTransparent()]
[assembly: SecurityRules(SecurityRuleSet.Level2)]
namespace CAS_02
{
    /// <summary>
    /// Demo class
    /// </summary>
    public class AssemblyInfo
    {
        /// <summary>
        /// Write to the console the security settings of the assembly
        /// </summary>
        [SecurityCritical()]
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
            Type t = a.GetType("CAS_02.AssemblyInfo");
            //show if the class is Critical, Transparent or SafeCritical
            sb.AppendFormat("Class IsSecurityCritical: {0} \n", t.IsSecurityCritical);
            sb.AppendFormat("Class IsSecuritySafeCritical: {0} \n",
            t.IsSecuritySafeCritical);
            sb.AppendFormat("Class IsSecurityTransparent: {0} \n",
            t.IsSecurityTransparent);
            //get the MethodInfo object of the current method              
            MethodInfo m = t.GetMethod("GetCasSecurityAttributes");
            //show if the current method is Critical, Transparent or SafeCritical
            sb.AppendFormat("Method IsSecurityCritical: {0} \n", m.IsSecurityCritical);
            sb.AppendFormat("Method IsSecuritySafeCritical: {0} \n", m.IsSecuritySafeCritical);
            sb.AppendFormat("Method IsSecurityTransparent: {0} \n", m.IsSecurityTransparent);
            try
            {
                sb.AppendFormat("\nPermissions Count: {0} \n", a.PermissionSet.Count);
            }
            catch (Exception ex)
            {
                sb.AppendFormat("\nError while trying to get the Permission Count:{0} \n", ex.Message);
            }
            return sb.ToString();
        }
    }
}

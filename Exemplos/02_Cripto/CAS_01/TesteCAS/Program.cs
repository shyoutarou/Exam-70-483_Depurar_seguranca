//using CAS_01;
//using CasAssemblyInfo;
using CAS_01;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Policy;
using System.Text;

//[assembly: SecurityRules(SecurityRuleSet.Level1)]
//[assembly: FileIOPermissionAttribute(SecurityAction.RequestMinimum, Read = @"C:\bootfile.ini")]

namespace TesteCAS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TesteNoGAC();
                //TesteSecurityTransparent();
                //TesteInicial();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
        }

        //[SecurityCritical]
        public static void TesteNoGAC()
        {
            //create the AppDomainSetup
            AppDomainSetup info = new AppDomainSetup();
            //set the path to the assembly to load. 
            info.ApplicationBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Assembly a = Assembly.LoadFile(Path.Combine(info.ApplicationBase, "CAS_01.dll"));
            StrongName sName = a.Evidence.GetHostEvidence<StrongName>();
            //StrongName fullTrustAssembly = typeof(AssemblyInfo).Assembly.Evidence.GetHostEvidence<StrongName>();
            //create the domain
            AppDomain domain = AppDomain.CreateDomain(
                "CasHostDemo", null, info, GetPermissionSet(), new StrongName[] { sName });

            //create an instance of the AseemblyInfo class
            Type t = typeof(AssemblyInfo);
            ObjectHandle handle = Activator.CreateInstanceFrom(
                domain,
                t.Assembly.ManifestModule.FullyQualifiedName,
                t.FullName);
            AssemblyInfo ai = (AssemblyInfo)handle.Unwrap();

            Console.WriteLine("DOMAIN INFO:\n");
            //get the domain info
            Console.WriteLine(GetDomainInfo(domain));

            Console.WriteLine("ASSEMBLY INFO:\n");
            //get the assembly info form the sandboxed assembly
            Console.WriteLine(ai.GetCasSecurityAttributes());
        }

        public static void TesteSecurityTransparent()
        {
            //create  the AppDomainSetup
            AppDomainSetup info = new AppDomainSetup();
            //set the path to the assembly to load. 
            info.ApplicationBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //create the domain
            AppDomain domain = AppDomain.CreateDomain(
                "CasHostDemo", null, info, GetPermissionSet());
            //create an instance of the AssemblyInfo class
            Type t = typeof(AssemblyInfo);
            ObjectHandle handle = Activator.CreateInstanceFrom(
                domain,
                t.Assembly.ManifestModule.FullyQualifiedName,
                t.FullName);
            AssemblyInfo ai = (AssemblyInfo)handle.Unwrap();

            Console.WriteLine("DOMAIN INFO:\n");
            //get the domain info
            Console.WriteLine(GetDomainInfo(domain));

            Console.WriteLine("ASSEMBLY INFO:\n");
            //get the assembly info form the sandboxed assembly
            Console.WriteLine(ai.GetCasSecurityAttributes());
        }

        public static void TesteInicial()
        {
            try
            {
                //get the assembly zone evidence
                Zone z = Assembly.GetExecutingAssembly().Evidence.GetHostEvidence<Zone>();
                Console.WriteLine("Zone Evidence: " + z.SecurityZone.ToString() + "\n");
                Console.WriteLine(new AssemblyInfo().GetCasSecurityAttributes());
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.ToString());
            }
        }

        /// <summary>
        /// create a permission set
        /// </summary>
        public static PermissionSet GetPermissionSet()
        {
            //create an evidence of type zone
            Evidence ev = new Evidence();
            ev.AddHostEvidence(new Zone(SecurityZone.MyComputer));

            //return the PermissionSets specific to the type of zone
            return SecurityManager.GetStandardSandbox(ev);
        }

        /// <summary>
        /// Get the Domain security info
        /// </summary>
        public static string GetDomainInfo(AppDomain domain)
        {
            StringBuilder sb = new StringBuilder();
            //check the domain trust
            sb.AppendFormat("Domain Is Full Trusted: {0} \n", domain.IsFullyTrusted);
            //show the number of the permission granted to the assembly
            sb.AppendFormat("\nPermissions Count: {0} \n", domain.PermissionSet.Count);
            return sb.ToString();
        }
    }
}

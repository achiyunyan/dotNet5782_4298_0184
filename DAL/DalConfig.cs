using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalApi
{
    class DalConfig
    {
        internal static string DalName;
        internal static Dictionary<string, string> DalPackages;
        private static string solutionDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName).FullName;

        private static string filePath = Path.Combine(solutionDirectory, "xml", "dal-config.xml");
        static DalConfig()
        {
            XElement dalConfig = XElement.Load(filePath);
            DalName = dalConfig.Element("dal").Value;
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                           select pkg
                          ).ToDictionary(p => "" + p.Name, p => p.Value);
        }
    }
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }
}

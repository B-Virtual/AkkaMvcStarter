using System.IO;
using Akka.Configuration;

namespace BVirtual.FaTi.Common.Config
{
    public static class HoconLoader
    {
        public static Akka.Configuration.Config FromFile(string hoconConfigFile)
        {
            return ConfigurationFactory.ParseString(File.ReadAllText(hoconConfigFile));
        }
    }
}

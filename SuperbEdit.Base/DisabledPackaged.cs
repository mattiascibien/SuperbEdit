using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    public static class DisabledPackaged
    {
        static List<string> disabledPackages;

        static DisabledPackaged()
        {
            Config config = new Config();

            disabledPackages = config.RetrieveConfigValue<List<string>>("disabled_packages", new List<string>());

            config.Dispose();
        }

        public static bool IsDisabled(string packageName)
        {
            return disabledPackages.Contains(packageName);
        }
    }
}

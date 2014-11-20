using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    public static class DisabledPackaged
    {
        static List<object> disabledPackages;

        static DisabledPackaged()
        {
            Config config = new Config();

            disabledPackages = config.RetrieveConfigValue<List<object>>("disabled_packages", new List<object>());

            config.Dispose();
        }

        public static bool IsDisabled(string packageName)
        {
            return disabledPackages.Contains(packageName);
        }
    }
}

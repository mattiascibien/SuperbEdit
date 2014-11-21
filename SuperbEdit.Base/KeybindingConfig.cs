using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    internal static class KeybindingConfig
    {
        //Mattias: internal for running tests only.
        internal static Config config;

        static readonly string defaultKeyBindings = Path.Combine(Folders.ProgramFolder, "key_bindings.json");
        static readonly string userKeyBindings = Path.Combine(Folders.UserFolder, "key_bindings.json");

        static KeybindingConfig()
        {
            config = new Config(defaultKeyBindings, userKeyBindings);
        }


        internal static string RetrieveKeyBind(string keybinding)
        {
            return config.RetrieveConfigValue<string>(keybinding);
        }
    }
}

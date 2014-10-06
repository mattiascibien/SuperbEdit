using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
#if !PORTABLE_BUILD
    [Export(typeof(IActionItem))]
    [ExportActionMetadata(Menu = "Preferences", Order = 0, Owner = "Shell", RegisterInCommandWindow = true)]
    public class UserSettingsAction : ActionItem
    {

        [Import] private Lazy<IShell> shell;


        public UserSettingsAction()
            : base("Settings - User", "Opens current user settings.")
        {
            
        }

        public override void Execute()
        {
            shell.Value.OpenUserConfig();
        }
    }
#endif
}

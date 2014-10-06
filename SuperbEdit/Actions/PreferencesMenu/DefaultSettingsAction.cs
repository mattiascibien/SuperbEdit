using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [Export(typeof(IActionItem))]
    [ExportActionMetadata(Menu = "Preferences", Order = 1, Owner = "Shell", RegisterInCommandWindow = true)]
    public class DefaultSettingsAction : ActionItem
    {

        [Import] private Lazy<IShell> shell;


        public DefaultSettingsAction()
            : base("Settings - Default", "Opens program settings.")
        {
            
        }

        public override void Execute()
        {
            shell.Value.OpenDefaultConfig();
        }
    }
}

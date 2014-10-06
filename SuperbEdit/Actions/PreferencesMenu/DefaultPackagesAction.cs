using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [Export(typeof(IActionItem))]
    [ExportActionMetadata(Menu = "Preferences", Order = 4, Owner = "Shell", RegisterInCommandWindow = true)]
    public class DefaultPackagesAction : ActionItem
    {

        [Import] private Lazy<IShell> shell;


        public DefaultPackagesAction()
            : base("Packages - Default", "Opens program default packages.")
        {
            
        }

        public override void Execute()
        {
            Process.Start(Folders.DefaultPackagesFolder);
        }
    }
}

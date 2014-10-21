using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
#if !PORTABLE_BUILD
    [Export(typeof (IActionItem))]
    [ExportActionMetadata(Menu = "Preferences", Order = 3, Owner = "Shell", RegisterInCommandWindow = true)]
    public class UserPackagesAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;


        public UserPackagesAction()
            : base("Packages - User", "Opens current user Packages.")
        {
        }

        public override void Execute()
        {
            Process.Start(Folders.UserPackagesFolder);
        }
    }
#endif
}
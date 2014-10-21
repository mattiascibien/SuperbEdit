using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.Shell.Actions
{
    [Export(typeof (IActionItem))]
    [ExportActionMetadata(Menu = "File", Order = 6, Owner = "Shell", RegisterInCommandWindow = true)]
    public class CloseAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public CloseAction() : base("Close", "Closes the active item")
        {
        }

        public override void Execute()
        {
            shell.Value.ActiveItem.TryClose();
        }
    }
}
using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "File", Order = 6, Owner = "Shell", RegisterInCommandWindow = true)]
    public class CloseAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public CloseAction() : base("Close", "Closes the active item", "File.Close")
        {
        }

        public override void Execute()
        {
            if (shell.Value.ActiveItem != null)
                shell.Value.ActiveItem.TryClose();
        }
    }
}
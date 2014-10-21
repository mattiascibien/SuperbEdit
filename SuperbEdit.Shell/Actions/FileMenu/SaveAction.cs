using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [Export(typeof (IActionItem))]
    [ExportActionMetadata(Menu = "File", Order = 3, Owner = "Shell", RegisterInCommandWindow = true)]
    public class SaveAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public SaveAction() : base("Save", "Saves the current file.")
        {
        }

        public override void Execute()
        {
            if (shell.Value.ActiveItem != null)
                shell.Value.ActiveItem.Save();
        }
    }
}
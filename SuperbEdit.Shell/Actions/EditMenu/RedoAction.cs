using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.Shell.Actions
{
    [Export(typeof (IActionItem))]
    [ExportActionMetadata(Menu = "Edit", Order = 1, Owner = "Shell", RegisterInCommandWindow = true)]
    public class RedoAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public RedoAction()
            : base("Redo", "Redoes the last action in active editor")
        {
        }

        public override void Execute()
        {
            shell.Value.ActiveItem.Redo();
        }
    }
}
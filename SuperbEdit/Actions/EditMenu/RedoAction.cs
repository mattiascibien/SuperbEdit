using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "Edit", Order = 1, Owner = "Shell", RegisterInCommandWindow = true)]
    public class RedoAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public RedoAction()
            : base("Redo", "Redoes the last action in active editor", "Edit.Redo")
        {
        }

        public override void Execute()
        {
            if (shell.Value.ActiveItem != null)
                shell.Value.ActiveItem.Redo();
        }
    }
}
using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "Edit", Order = 5, Owner = "Shell", RegisterInCommandWindow = true)]
    public class PasteAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public PasteAction()
            : base("Paste", "Paste clipboard text in active editor", "Edit.Paste")
        {
        }

        public override void Execute()
        {
            if (shell.Value.ActiveItem != null)
                shell.Value.ActiveItem.Paste();
        }
    }
}
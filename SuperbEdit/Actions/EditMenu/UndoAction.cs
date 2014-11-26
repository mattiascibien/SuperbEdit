using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "Edit", Order = 0, Owner = "Shell", RegisterInCommandWindow = true)]
    public class UndoAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public UndoAction() : base("Undo", "Undo last action in active editor", "Edit.Undo")
        {
        }

        public override void Execute()
        {
            if (shell.Value.ActiveItem != null)
                shell.Value.ActiveItem.Undo();
        }
    }
}
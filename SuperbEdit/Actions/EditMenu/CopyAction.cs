using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "Edit", Order = 4, Owner = "Shell", RegisterInCommandWindow = true)]
    public class CopyAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public CopyAction()
            : base("Copy", "Cuts selected text in active editor", "Edit.Copy")
        {
        }

        public override void Execute()
        {
            if(shell.Value.ActiveItem != null)
                shell.Value.ActiveItem.Copy();
        }
    }
}
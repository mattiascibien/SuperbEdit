using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "Edit", Order = 3, Owner = "Shell", RegisterInCommandWindow = true)]
    public class CutAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public CutAction()
            : base("Cut", "Cuts selected text in active editor", "Edit.Cut")
        {
        }

        public override void Execute()
        {
            shell.Value.ActiveItem.Cut();
        }
    }
}
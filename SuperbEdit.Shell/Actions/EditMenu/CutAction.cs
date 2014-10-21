using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.Shell.Actions
{
    [Export(typeof (IActionItem))]
    [ExportActionMetadata(Menu = "Edit", Order = 3, Owner = "Shell", RegisterInCommandWindow = true)]
    public class CutAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public CutAction()
            : base("Cut", "Cuts selected text in active editor")
        {
        }

        public override void Execute()
        {
            shell.Value.ActiveItem.Cut();
        }
    }
}
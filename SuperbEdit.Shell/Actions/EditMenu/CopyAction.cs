using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.Shell.Actions
{
    [Export(typeof (IActionItem))]
    [ExportActionMetadata(Menu = "Edit", Order = 4, Owner = "Shell", RegisterInCommandWindow = true)]
    public class CopyAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public CopyAction()
            : base("Copy", "Cuts selected text in active editor")
        {
        }

        public override void Execute()
        {
            shell.Value.ActiveItem.Copy();
        }
    }
}
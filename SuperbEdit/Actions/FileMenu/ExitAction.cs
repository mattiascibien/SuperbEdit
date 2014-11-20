using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "File", Order = 7, Owner = "Shell", RegisterInCommandWindow = true)]
    public class ExitAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public ExitAction() : base("Exit", "Closes the application.", "File.Exit")
        {
        }

        public override void Execute()
        {
            shell.Value.Exit();
        }
    }
}
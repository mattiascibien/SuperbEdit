using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "File", Order = 4, Owner = "Shell", RegisterInCommandWindow = true)]
    public class SaveAsAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public SaveAsAction() : base("Save As", "Saves the current file with a different name.", "File.SaveAs")
        {
        }


        public override void Execute()
        {
            if (shell.Value.ActiveItem != null)
                shell.Value.ActiveItem.SaveAs();
        }
    }
}
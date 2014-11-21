using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "File", Order = 5, Owner = "Shell", RegisterInCommandWindow = true)]
    public class SaveAllAction : ActionItem
    {
        [Import] public Lazy<IShell> shell;

        public SaveAllAction() : base("Save All", "Save all currently opened files.", "File.SaveAll")
        {
        }

        public override void Execute()
        {
            foreach (ITab item in shell.Value.Items)
            {
                item.Save();
            }
        }
    }
}
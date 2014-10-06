using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{

    [Export(typeof(IActionItem))]
    [ExportActionMetadata(Menu = "File", Order = 5, Owner = "Shell", RegisterInCommandWindow = true)]
    public class SaveAllAction : ActionItem
    {
        [Import] public Lazy<IShell> shell;

        public SaveAllAction() : base("Save All", "Save all currently opened files.")
        {
            
        }

        public override void Execute()
        {
            foreach (var item in shell.Value.Items)
            {
                item.Save();
            }
        }
    }
}

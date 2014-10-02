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
    [ExportActionMetadata(Menu = "Edit", Order = 5, Owner = "Shell", RegisterInCommandWindow = true)]
    public class PasteAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public PasteAction()
            : base("Paste", "Paste clipboard text in active editor")
        {
            
        }

        public override void Execute()
        {
           shell.Value.Paste();
        }
    }
}

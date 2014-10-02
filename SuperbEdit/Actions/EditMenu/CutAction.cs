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
           shell.Value.Cut();
        }
    }
}

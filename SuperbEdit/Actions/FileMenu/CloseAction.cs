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
    [ExportActionMetadata(Menu = "File", Order = 6, Owner = "Shell", RegisterInCommandWindow = true)]
    public class CloseAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public CloseAction() : base("Close", "Closes the active item")
        {
            
        }

        public override void Execute()
        {
            shell.Value.CloseActiveItem();
        }
    }
}

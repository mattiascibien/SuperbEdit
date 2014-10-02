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
    [ExportActionMetadata(Menu = "File", Order = 7, Owner = "Shell", RegisterInCommandWindow = true)]
    public class ExitAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public ExitAction() : base("Exit", "Closes the application.")
        {
            
        }

        public override void Execute()
        {
            shell.Value.Exit();
        }
    }
}

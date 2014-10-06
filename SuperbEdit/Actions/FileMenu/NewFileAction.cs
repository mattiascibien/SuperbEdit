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
    [ExportActionMetadata(Menu = "File", Order = 0, Owner = "Shell", RegisterInCommandWindow = true)]
    public class NewFileAction : ActionItem
    {

        [Import] private Lazy<IShell> shell;
        [Import] private TabService tabService; 


        public NewFileAction() : base("New File", "Creates a new file")
        {
            
        }

        public override void Execute()
        {
            ITab item = tabService.RequestDefaultTab();
            shell.Value.OpenTab(item);
        }
    }
}

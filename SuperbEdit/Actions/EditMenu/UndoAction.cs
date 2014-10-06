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
    [ExportActionMetadata(Menu = "Edit", Order = 0, Owner = "Shell", RegisterInCommandWindow = true)]
    public class UndoAction : ActionItem
    {
        [Import] private Lazy<IShell> shell; 

        public UndoAction() : base("Undo", "Undo last action in active editor")
        {
            
        }

        public override void Execute()
        {
           shell.Value.ActiveItem.Undo();
        }
    }
}

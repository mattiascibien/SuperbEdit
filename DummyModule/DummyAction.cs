using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperbEdit.Base;

namespace DummyModule
{
    [Export(typeof(IActionItem))]
    [ExportActionMetadata(Menu = "", Order = 0, Owner = "Dummy", RegisterInCommandWindow = true)]
    public class DummyAction : ActionItem
    {
        public override string Name
        {
            get { return "Dummy"; }
        }

        public override string Description
        {
            get { return "I do not do anything. I am here only for the developers :)"; }
        }

        public override void Execute()
        {
            
        }
    }
}

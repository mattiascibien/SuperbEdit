using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace DummyModule
{
    [Export(typeof (IActionItem))]
    [ExportActionMetadata(Menu = "", Order = 0, Owner = "Dummy", RegisterInCommandWindow = true)]
    public class DummyAction : ActionItem
    {
        public DummyAction() : base("Dummy", "Just a developer helper.")
        {
        }

        public override void Execute()
        {
        }
    }
}
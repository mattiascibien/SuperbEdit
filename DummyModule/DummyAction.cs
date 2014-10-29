using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace DummyModule
{
    [ExportAction(Menu = "", Order = 0, Owner = "Dummy", RegisterInCommandWindow = true)]
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
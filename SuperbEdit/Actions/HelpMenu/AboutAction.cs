using System.ComponentModel.Composition;
using Caliburn.Micro;
using SuperbEdit.Base;
using SuperbEdit.ViewModels;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "Help", Order = 1, Owner = "Shell", RegisterInCommandWindow = true)]
    public class AboutAction : ActionItem
    {
        [Import] private IWindowManager _windowManager;


        public AboutAction()
            : base("About", "About the program")
        {
        }

        public override void Execute()
        {
            _windowManager.ShowDialog(new AboutViewModel());
        }
    }
}
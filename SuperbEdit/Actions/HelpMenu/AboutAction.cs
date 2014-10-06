using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SuperbEdit.Base;
using SuperbEdit.ViewModels;

namespace SuperbEdit.Actions
{
    [Export(typeof(IActionItem))]
    [ExportActionMetadata(Menu = "About", Order = 0, Owner = "Shell", RegisterInCommandWindow = true)]
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

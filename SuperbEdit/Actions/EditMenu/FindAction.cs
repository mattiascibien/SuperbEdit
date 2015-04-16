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
    [ExportAction(Menu = "Edit", Order = 6, Owner = "Shell", RegisterInCommandWindow = true)]
    public class FindAction : ActionItem
    {
        [Import]
        private IWindowManager _windowManager;

        public FindAction()
            :base("Find and Replace...", "Opens the find and replace dialog...", "Edit.FindReplace")
        { }

        public override void Execute()
        {
            //TODO: should not create one view every time 
            _windowManager.ShowWindow(new FindReplaceViewModel());
        }
    }
}

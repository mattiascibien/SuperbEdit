using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SuperbEdit.Base;

namespace SuperbEdit.ViewModels
{
    public class FindReplaceViewModel : Screen
    {
        [Import] 
        private Lazy<IShell> _shell;

        public FindViewModel FindViewModel { get; set; }

        public ReplaceViewModel ReplaceViewModel { get; set; }

        public FindReplaceViewModel()
        {
            DisplayName = "Find & Replace";

            FindViewModel = new FindViewModel();
            ReplaceViewModel = new ReplaceViewModel();
        }
    }
}

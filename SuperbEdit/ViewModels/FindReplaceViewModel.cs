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

        public FindReplaceViewModel()
        {
            DisplayName = "Find & Replace";
        }
    }
}

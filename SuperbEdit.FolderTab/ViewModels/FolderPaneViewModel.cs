using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperbEdit.Base;

namespace SuperbEdit.FolderTab.ViewModels
{
    [Export(typeof (ILeftPane))]
    public class FolderPaneViewModel : ILeftPane
    {
    }
}

using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.FolderTab.ViewModels
{
    [Export(typeof (ILeftPane))]
    public class FolderPaneViewModel : ILeftPane
    {
    }
}
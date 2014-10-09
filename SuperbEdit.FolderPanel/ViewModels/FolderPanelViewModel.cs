using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.FolderPanel.ViewModels
{
    [Export(typeof (IPanel))]
    [ExportPanelMetadata(DefaultPosition = PanelPosition.Left)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FolderPanelViewModel : Panel
    {
    }
}
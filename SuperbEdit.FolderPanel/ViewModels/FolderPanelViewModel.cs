using System.ComponentModel.Composition;
using SuperbEdit.Base;

namespace SuperbEdit.FolderPanel.ViewModels
{
    [Export(typeof (IPanel))]
    [Export]
    [ExportPanelMetadata(DefaultPosition = PanelPosition.Left)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FolderPanelViewModel : Panel
    {
        private string _currentFolder;
        public string CurrentFolder
        {
            get { return _currentFolder; }

            set
            {
                if (_currentFolder != value)
                {
                    _currentFolder = value;
                    NotifyOfPropertyChange(() => CurrentFolder);
                }
            }
        }
    }
}
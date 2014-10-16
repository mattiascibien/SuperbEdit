using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;
using SuperbEdit.FolderPanel.ViewModels;

namespace SuperbEdit.FolderPanel.Actions
{
    [Export(typeof (IActionItem))]
    [ExportActionMetadata(Menu = "View", Order = 1, Owner = "FolderPanel", RegisterInCommandWindow = false)]
    public class FolderPanelAction : ActionItem
    {
        [Import] private FolderPanelViewModel folderPanel;

        [Import]
        private Lazy<IShell> shell;

        public FolderPanelAction()
            : base("Folder Panel", "Shows or hides the folder panel")
        {
        }

        public override void Execute()
        {
            shell.Value.ShowHidePanel(folderPanel);
        }
    }
}
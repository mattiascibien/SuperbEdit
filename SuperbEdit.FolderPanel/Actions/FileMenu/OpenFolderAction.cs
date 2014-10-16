using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;

using Microsoft.WindowsAPICodePack.Dialogs;
using SuperbEdit.FolderPanel.ViewModels;

namespace SuperbEdit.FolderPanel.Actions
{
    [Export(typeof(IActionItem))]
    [ExportActionMetadata(Menu = "File", Order = 2, Owner = "FolderPanel", RegisterInCommandWindow = true)]
    public class OpenFolderAction : ActionItem
    {
        [Import]
        private Lazy<IShell> shell;
        [Import]
        private TabService tabService;

        [Import] 
        private FolderPanelViewModel _folderPanel;

        public OpenFolderAction()
            : base("Open Folder...", "Opens a folder in the folder panel.")
        {
        }

        public override void Execute()
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false,
                ShowPlacesList = true
            };


            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var folder = dialog.FileName;
                _folderPanel.CurrentFolder = folder;
            }
        }
    }
}
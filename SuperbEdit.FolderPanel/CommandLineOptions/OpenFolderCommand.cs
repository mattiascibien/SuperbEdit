using SuperbEdit.Base;
using SuperbEdit.FolderPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.FolderPanel.CommandLineOptions
{
    [ExportCommandLineOption]
    public class OpenFolderCommand : ICommandLineOption
    {
        [Import]
        private FolderPanelViewModel _folderPanel;

        public string ShortCommand
        {
            get { return "d"; }
        }

        public string Command
        {
            get { return "dir="; }
        }

        public Action<string> Action
        {
            get {
                return new Action<string>((str) =>
                {
                    _folderPanel.CurrentFolder = str;
                });
            }
        }


        public string HelpString
        {
            get { return "Opens the specified folder in the Folder Panel"; }
        }
    }
}

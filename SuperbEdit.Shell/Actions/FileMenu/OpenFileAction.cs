using System;
using System.ComponentModel.Composition;
using Microsoft.Win32;
using SuperbEdit.Base;

namespace SuperbEdit.Shell.Actions
{
    [Export(typeof (IActionItem))]
    [ExportActionMetadata(Menu = "File", Order = 1, Owner = "Shell", RegisterInCommandWindow = true)]
    public class OpenFileAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;
        [Import] private TabService tabService;

        public OpenFileAction() : base("Open", "Opens a file from the filesystem.")
        {
        }

        public override void Execute()
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog().Value)
            {
                ITab fileTabViewModel = tabService.RequestDefaultTab();
                fileTabViewModel.SetFile(dialog.FileName);
                shell.Value.OpenTab(fileTabViewModel);
            }
        }
    }
}
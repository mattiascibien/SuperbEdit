using System;
using System.ComponentModel.Composition;
using System.IO;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
#if !PORTABLE_BUILD
    [ExportAction(Menu = "Preferences", Order = 0, Owner = "Shell", RegisterInCommandWindow = true)]
    public class UserSettingsAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;
        [Import] private TabService tabService;

        public UserSettingsAction()
            : base("Settings - User", "Opens current user settings.")
        {
        }

        public override void Execute()
        {
            ITab fileTabViewModel = tabService.RequestDefaultTab();
            fileTabViewModel.SetFile(Path.Combine(Folders.UserFolder, "config.json"));
            shell.Value.OpenTab(fileTabViewModel);
        }
    }
#endif
}
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using SuperbEdit.Base;
using System.IO;

namespace SuperbEdit.Actions
{
#if !PORTABLE_BUILD
    [ExportAction(Menu = "Preferences", Order = 6, Owner = "Shell", RegisterInCommandWindow = true)]
    public class UserKeybindingsAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;
        [Import] private TabService tabService;


        public UserKeybindingsAction()
            : base("Keybindings - User", "Opens the program Keybindings.")
        {
        }

        public override void Execute()
        {
            ITab fileTabViewModel = tabService.RequestDefaultTab();
            fileTabViewModel.SetFile(Path.Combine(Folders.UserFolder, "key_bindings.json"));
            shell.Value.OpenTab(fileTabViewModel);
        }
    }
#endif
}
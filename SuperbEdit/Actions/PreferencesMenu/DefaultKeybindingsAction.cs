using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using SuperbEdit.Base;
using System.IO;

namespace SuperbEdit.Actions
{
#if !PORTABLE_BUILD
    [ExportAction(Menu = "Preferences", Order = 5, Owner = "Shell", RegisterInCommandWindow = true)]
    public class DefaultKeybindingsAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;
        [Import] private TabService tabService;


        public DefaultKeybindingsAction()
            : base("Keybindings - Default", "Opens the user Keybindings.")
        {
        }

        public override void Execute()
        {
            ITab fileTabViewModel = tabService.RequestDefaultTab();
            fileTabViewModel.SetFile(Path.Combine(Folders.ProgramFolder, "key_bindings.json"));
            shell.Value.OpenTab(fileTabViewModel);
        }
    }
#endif
}
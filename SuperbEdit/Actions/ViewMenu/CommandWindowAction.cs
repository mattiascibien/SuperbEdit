using System;
using System.ComponentModel.Composition;
using SuperbEdit.Base;
using SuperbEdit.ViewModels;

namespace SuperbEdit.Actions
{
    [Export(typeof (IActionItem))]
    [ExportActionMetadata(Menu = "View", Order = 0, Owner = "Shell", RegisterInCommandWindow = false)]
    public class CommandWindowAction : ActionItem
    {
        [Import] private Lazy<ShellViewModel> shell;
        [Import] private TabService tabService;


        public CommandWindowAction()
            : base("Command Window", "Shows or hides the command window")
        {
        }

        public override void Execute()
        {
            ITab item = tabService.RequestDefaultTab();
            shell.Value.ToggleCommandWindow();
        }
    }
}
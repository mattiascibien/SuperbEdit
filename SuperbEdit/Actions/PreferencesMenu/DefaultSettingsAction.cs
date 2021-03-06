﻿using System;
using System.ComponentModel.Composition;
using System.IO;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "Preferences", Order = 1, Owner = "Shell", RegisterInCommandWindow = true)]
    public class DefaultSettingsAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;
        [Import] private TabService tabService;


        public DefaultSettingsAction()
            : base("Settings - Default", "Opens program settings.")
        {
        }

        public override void Execute()
        {
            ITab fileTabViewModel = tabService.RequestDefaultTab();
            fileTabViewModel.SetFile(Path.Combine(Folders.ProgramFolder, "config.json"));
            shell.Value.OpenTab(fileTabViewModel);
        }
    }
}
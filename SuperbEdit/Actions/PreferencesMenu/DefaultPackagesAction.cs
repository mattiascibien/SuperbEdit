﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "Preferences", Order = 4, Owner = "Shell", RegisterInCommandWindow = true)]
    public class DefaultPackagesAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;


        public DefaultPackagesAction()
            : base("Packages - Default", "Opens program default packages.")
        {
        }

        public override void Execute()
        {
            Process.Start(Folders.DefaultPackagesFolder);
        }
    }
}
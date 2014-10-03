﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperbEdit.Base;

namespace SuperbEdit.Actions
{
    [Export(typeof(IActionItem))]
    [ExportActionMetadata(Menu = "File", Order = 4, Owner = "Shell", RegisterInCommandWindow = true)]
    public class SaveAsAction : ActionItem
    {
        [Import] private Lazy<IShell> shell;

        public SaveAsAction() : base("Save As", "Saves the current file with a different name.")
        {
            
        }


        public override void Execute()
        {
            shell.Value.SaveAs();
        }
    }
}
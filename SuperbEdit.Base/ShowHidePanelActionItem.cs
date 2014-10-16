using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    public class ShowHidePanelActionItem<T> : ActionItem where T : IPanel
    {
        [Import] private T panel;

        [Import]
        private Lazy<IShell> shell;

        public ShowHidePanelActionItem(string name, string description)
            : base(name, description)
        {
        }

        public override void Execute()
        {
            shell.Value.ShowHidePanel(panel);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    /// <summary>
    /// Convenience action for hiding/showing a panel
    /// </summary>
    /// <typeparam name="T">type of the panel to be hidden</typeparam>
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
            panel.ShowHide();
        }
    }
}

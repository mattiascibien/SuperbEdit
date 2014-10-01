using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SuperbEdit.Base;

namespace SuperbEdit.ViewModels
{
    [Export]
    public class CommandWindowViewModel : Screen
    {
        private ObservableCollection<IActionItem> _actions;

        public ObservableCollection<IActionItem> Actions
        {
            get { return _actions; }
            set
            {
                if (_actions != value)
                {
                    _actions = value;
                    NotifyOfPropertyChange(() => Actions);
                }
            }
        }

        [ImportingConstructor]
        public CommandWindowViewModel([ImportMany] IEnumerable<Lazy<IActionItem, IActionItemMetadata>> actions)
        {
            Actions = new ObservableCollection<IActionItem>();

            foreach (var action in actions)
            {
                if (action.Metadata.RegisterInCommandWindow)
                {
                    Actions.Add(action.Value);
                }
            }
        }
    }
}

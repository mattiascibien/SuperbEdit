using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using SuperbEdit.Base;

namespace SuperbEdit.ViewModels
{
    [Export]
    internal class CommandWindowViewModel : Screen
    {
        private List<IActionItem> _actions;


        private string _filter = "";

        [ImportingConstructor]
        public CommandWindowViewModel([ImportMany] IEnumerable<Lazy<IActionItem, IActionItemMetadata>> actions)
        {
            Actions = new List<IActionItem>();

            foreach (var action in actions)
            {
                if (action.Metadata.RegisterInCommandWindow)
                {
                    Actions.Add(action.Value);
                }
            }
        }

        public List<IActionItem> Actions
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

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    NotifyOfPropertyChange(() => Filter);
                    NotifyOfPropertyChange(() => FilteredActions);
                }
            }
        }

        public IEnumerable<IActionItem> FilteredActions
        {
            //TODO: should split inserted words and check for some of them in the name and description?
            get
            {
                return Actions.Where(a => a.Name.ToUpper().Contains(Filter.ToUpper())
                                          || a.Description.ToUpper().Contains(Filter.ToUpper()));
            }
        }
    }
}
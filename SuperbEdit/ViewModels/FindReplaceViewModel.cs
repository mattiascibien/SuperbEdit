using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using SuperbEdit.Base;

namespace SuperbEdit.ViewModels
{
    public class FindReplaceViewModel : Screen
    {
        public FindReplacOptions Options { get; set; }

        #region Texts
        private string _findText;
        public string FindText
        {
            get { return _findText; }
            set
            {
                if (_findText != value)
                {
                    _findText = value;
                    NotifyOfPropertyChange(() => FindText);
                }
            }
        }


        private string _replaceText;
        public string ReplaceText
        {
            get { return _replaceText; }
            set
            {
                if (_replaceText != value)
                {
                    _replaceText = value;
                    NotifyOfPropertyChange(() => ReplaceText);
                }
            }
        }

        #endregion


        //bool to determaine if we should use search options
        private bool _useOptions = false;
        public bool UseOptions
        {
            get { return _useOptions; }
            set
            {
                if (_useOptions != value)
                {
                    _useOptions = value;
                    NotifyOfPropertyChange(() => UseOptions);
                }
            }
        }

        //bool to determaine if we should use search options
        private bool _allOpenTabs = false;
        public bool AllOpenTabs
        {
            get
            {
                return _allOpenTabs;
            }
            set
            {
                if (_allOpenTabs != value)
                {
                    _allOpenTabs = value;
                    NotifyOfPropertyChange(() => AllOpenTabs);
                }
            }
        }

        private IShell _shell;

        public FindReplaceViewModel(IShell shell)
        {
            _shell = shell;
            DisplayName = "Find & Replace";
            Options = new FindReplacOptions();
        }

        #region Concrete implementations
        private void DoAction(Action<ITab> action)
        {
            if (AllOpenTabs)
            {
                foreach (var tab in _shell.Items)
                {
                    action(tab);
                }
            }
            else
            {
                if (_shell.ActiveItem != null)
                    action(_shell.ActiveItem);
            }
        }

        private void Replace(ITab tab)
        {
            tab.Replace(FindText, ReplaceText, Options);
        }

        private void ReplaceAll(ITab tab)
        {
            tab.ReplaceAll(FindText, ReplaceText, Options);
        }

        private void FindNext(ITab tab)
        {
            tab.FindNext(FindText, Options);
        }
        #endregion

        public void FindNext()
        {
            DoAction(FindNext);
        }

        public void Replace()
        {
            DoAction(Replace);
        }

        public void ReplaceAll()
        {
            if (MessageBox.Show("Are you sure you want to Replace All occurences of \"" +
                                FindText + "\" with \"" + ReplaceText + "\"?",
                "Replace All", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                DoAction(ReplaceAll);
            }
        }
    }
}

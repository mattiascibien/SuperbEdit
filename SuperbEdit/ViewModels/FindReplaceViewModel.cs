using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SuperbEdit.Base;

namespace SuperbEdit.ViewModels
{
    public class FindReplaceViewModel : Screen
    {
        #region Options

        private bool _caseSensitive = true;
        public bool CaseSensitive
        {
            get
            {
                return _caseSensitive;
            }
            set
            {
                if (_caseSensitive != value)
                {
                    _caseSensitive = value;
                    NotifyOfPropertyChange(() => CaseSensitive);
                }
            }
        }

        private bool _wholeWord = true;
        public bool WholeWord
        {
            get
            {
                return _wholeWord;
            }
            set
            {
                if (_wholeWord != value)
                {
                    _wholeWord = value;
                    NotifyOfPropertyChange(() => WholeWord);
                }
            }
        }

        private bool _regex;
        public bool Regex
        {
            get
            {
                return _regex;
            }
            set
            {
                if (_regex != value)
                {
                    _regex = value;
                    NotifyOfPropertyChange(() => Regex);
                }
            }
        }

        private bool _wildCards;
        public bool Wildcards
        {
            get
            {
                return _wildCards;
            }
            set
            {
                if (_wildCards != value)
                {
                    _wildCards = value;
                    NotifyOfPropertyChange(() => Wildcards);
                }
            }
        }

        private bool _searchUp;
        public bool SearchUp
        {
            get
            {
                return _wildCards;
            }
            set
            {
                if (_searchUp != value)
                {
                    _searchUp = value;
                    NotifyOfPropertyChange(() => SearchUp);
                }
            }
        }

        #endregion

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
            get
            {
                return _useOptions;
            }
            set
            {
                if (_useOptions != value)
                {
                    _useOptions = value;
                    NotifyOfPropertyChange(() => UseOptions);
                }
            }
        }

        [Import] 
        private Lazy<IShell> _shell;

        public FindReplaceViewModel()
        {
            DisplayName = "Find & Replace";
        }

        public void FindNext()
        {
            
        }

        public void Replace()
        {
            
        }

        public void ReplaceAll()
        {
            
        }
    }
}

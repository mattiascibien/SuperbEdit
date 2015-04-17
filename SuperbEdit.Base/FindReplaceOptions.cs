using System.Text.RegularExpressions;
using Caliburn.Micro;

namespace SuperbEdit.Base
{
    public class FindReplacOptions : PropertyChangedBase
    {
        private bool _caseSensitive = true;

        public bool CaseSensitive
        {
            get { return _caseSensitive; }
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
            get { return _wholeWord; }
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
            get { return _regex; }
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
            get { return _wildCards; }
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
            get { return _wildCards; }
            set
            {
                if (_searchUp != value)
                {
                    _searchUp = value;
                    NotifyOfPropertyChange(() => SearchUp);
                }
            }
        }


        public Regex GetRegEx(string textToFind, bool leftToRight = false)
        {
            RegexOptions options = RegexOptions.None;
            if (SearchUp && !leftToRight)
                options |= RegexOptions.RightToLeft;
            if (CaseSensitive == false)
                options |= RegexOptions.IgnoreCase;

            if (Regex == true)
            {
                return new Regex(textToFind, options);
            }
            else
            {
                string pattern = System.Text.RegularExpressions.Regex.Escape(textToFind);
                if (Wildcards == true)
                    pattern = pattern.Replace("\\*", ".*").Replace("\\?", ".");
                if (WholeWord == true)
                    pattern = "\\b" + pattern + "\\b";
                return new Regex(pattern, options);
            }
        }
    }
}

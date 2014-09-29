using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    public abstract class Tab : Screen, ITab
    {
        private bool _hasChanges;
        public bool HasChanges
        {
            get
            {
                return _hasChanges;
            }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    NotifyOfPropertyChange(() => HasChanges);
                }
            }
        }

        public abstract bool Save();
        public abstract bool SaveAs();
        public abstract void Undo();
        public abstract void Redo();
        public abstract void Cut();
        public abstract void Copy();
        public abstract void Paste();
        public abstract void SetFile(string filePath);

        protected Tab()
        {
            HasChanges = false;
        }
    }

}

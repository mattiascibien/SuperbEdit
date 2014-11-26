using Caliburn.Micro;
using System;

namespace SuperbEdit.Base
{
    /// <summary>
    /// Abstract class providing basic functionality
    /// for Tabs
    /// </summary>
    public abstract class Tab : Screen, ITab
    {


        private bool _hasChanges;

        protected Tab(IConfig config)
        {
            HasChanges = false;

            if(config != null)
            {
                config.ConfigChanged += config_ConfigChanged;
                ReloadConfig(config);
            }
        }


        void config_ConfigChanged(object sender, EventArgs e)
        {
            ReloadConfig(sender as IConfig);
        }


        protected virtual void ReloadConfig(IConfig config)
        {

        }

        public bool HasChanges
        {
            get { return _hasChanges; }
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
        public abstract void RegisterCommands();

        public abstract string FileContent
        {
            get;
            set;
        }

        public void CloseItem(Tab item)
        {
            item.TryClose();
        }

        public void DetachItem(Tab item)
        {
            var shell = item.Parent as IShell;
            shell.DetachItem(item);
        }
    }
}
using System;
using System.IO;
using System.Reflection;
using Caliburn.Micro;
using Microsoft.Win32;
using SuperbEdit.Base;

namespace SuperbEdit.ViewModels
{
    public sealed class ShellViewModel : Conductor<Tab>.Collection.OneActive, IShell
    {
        private readonly ShellViewModel _parentViewModel;

        private readonly IWindowManager _windowManager;

        private bool _isSecondaryWindow;
        public bool IsSecondaryWindow
        {
            get
            {
                return _isSecondaryWindow;
            }
            set
            {
                if(_isSecondaryWindow != value)
                {
                    _isSecondaryWindow = value;
                    NotifyOfPropertyChange(() => IsSecondaryWindow);
                }
            }
        }


        public ShellViewModel(IWindowManager windowManager, ShellViewModel parent, bool secondaryWindow)
        {
            _windowManager = windowManager;
            IsSecondaryWindow = secondaryWindow;
            DisplayName = "SuperbEdit";
            _parentViewModel = parent;


            //HACK: to initialize view.
            //Items.Add(new FileTabViewModel());
        }


        public ShellViewModel(IWindowManager windowManager) : this(windowManager, null, false)
        {

        }


        public void NewWindow()
        {
            _windowManager.ShowWindow(new ShellViewModel(_windowManager, _parentViewModel ?? this, true));
        }

        public void NewFile()
        {
            var item = new FileTabViewModel();
            OpenTab(item);
        }

        public void OpenFile()
        {
            var dialog = new OpenFileDialog();

            if(dialog.ShowDialog().Value)
            {
                var fileTabViewModel = new FileTabViewModel(dialog.FileName);
                OpenTab(fileTabViewModel);
            }
        }

        private void OpenTab(Tab tab)
        {
            Items.Add(tab);
            ActivateItem(tab);
        }

        public void  AttachBack()
        {
            _parentViewModel.Items.AddRange(this.Items);
            this.TryClose();
        }

        public void About()
        {
            _windowManager.ShowDialog(new AboutViewModel());
        }


        public void Save()
        {
            if (ActiveItem != null) ActiveItem.Save();
        }

        public void SaveAs()
        {
            if (ActiveItem != null) ActiveItem.SaveAs();
        }

        public void SaveAll()
        {
            foreach (var tab in Items)
            {
                tab.Save();
            }
        }

        public void Undo()
        {
            if (ActiveItem != null) ActiveItem.Undo();
        }

        public void Redo()
        {
            if (ActiveItem != null) ActiveItem.Redo();
        }

        public void Exit()
        {
            TryClose();
        }

        public void OpenDefaultConfig()
        {
           OpenTab(new FileTabViewModel(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.json")));
        }

        public void OpenUserConfig()
        {
            OpenTab(new FileTabViewModel(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".superbedit", "config.json")));
        }


        public void CloseActiveItem()
        {
            if (ActiveItem != null) CloseItem(ActiveItem);
        }

        private void CloseItem(Tab item)
        {
            item.TryClose();
        }
    }
}
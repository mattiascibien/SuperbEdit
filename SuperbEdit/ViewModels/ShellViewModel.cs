using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using AurelienRibon.Ui.SyntaxHighlightBox;
using Caliburn.Micro;
using Microsoft.Win32;
using SuperbEdit.Base;

namespace SuperbEdit.ViewModels
{
    [Export(typeof(IShell))]
    public sealed class ShellViewModel : Conductor<Tab>.Collection.OneActive, IShell
    {
        private readonly ShellViewModel _parentViewModel;

        private readonly IWindowManager _windowManager;


        [Import]
        private IFolders folders;

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

        public ICollection<IHighlighter> Highlighters
        {
            get { return HighlighterManager.Instance.Highlighters.Values; }
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

        [ImportingConstructor]
        public ShellViewModel(IWindowManager windowManager) : this(windowManager, null, false)
        {

        }


        public ShellViewModel NewWindow()
        {
            var shellViewModel = new ShellViewModel(_windowManager, _parentViewModel ?? this, true);
            _windowManager.ShowWindow(shellViewModel);
            return shellViewModel;
        }

        public void NewFile()
        {
            var item = new TextEditorViewModel();
            OpenTab(item);
        }

        public void OpenFile()
        {
            var dialog = new OpenFileDialog();

            if(dialog.ShowDialog().Value)
            {
                var fileTabViewModel = new TextEditorViewModel(dialog.FileName);
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


        public void Cut()
        {
            if (ActiveItem != null) ActiveItem.Cut();
        }

        public void Copy()
        {
            if (ActiveItem != null) ActiveItem.Copy();
        }

        public void Paste()
        {
            if (ActiveItem != null) ActiveItem.Paste();
        }

        public void Exit()
        {
            TryClose();
        }

        public void OpenDefaultConfig()
        {
           OpenTab(new TextEditorViewModel(Path.Combine(folders.ProgramFolder, "config.json")));
        }

        public void OpenUserConfig()
        {
            OpenTab(new TextEditorViewModel(Path.Combine(folders.UserFolder, "config.json")));
        }


        public void CloseActiveItem()
        {
            if (ActiveItem != null) CloseItem(ActiveItem);
        }

        private void CloseItem(Tab item)
        {
            item.TryClose();
        }



        public void DetachItem(Tab item)
        {
            var shellViewModel = NewWindow();
            shellViewModel.Items.Add(item);
            shellViewModel.ActivateItem(item);
            Items.Remove(item);
        }

        public void SetHighlighter(RoutedEventArgs eventArgs)
        {
            
            if(ActiveItem != null) (ActiveItem as TextEditorViewModel).SetHighlighter(
                HighlighterManager.Instance.Highlighters[
                (eventArgs.OriginalSource as MenuItem).Header.ToString()]
                );
        }
    }
}
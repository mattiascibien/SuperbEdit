using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Win32;
using SuperbEdit.Base;
using SuperbEdit.Views;

namespace SuperbEdit.ViewModels
{
    [Export(typeof (IShell))]
    public sealed class ShellViewModel : Conductor<ITab>.Collection.OneActive, IShell
    {
        public IEnumerable<IActionItem> FileMenuItems { get; set; }
        public IEnumerable<IActionItem> EditMenuItems { get; set; }
        public IEnumerable<IActionItem> PreferencesMenuItems { get; set; }
        public IEnumerable<IActionItem> AboutMenuItems { get; set; }


        private CommandWindowViewModel _commandWindow;

        [Import]
        public CommandWindowViewModel CommandWindow
        {
            get { return _commandWindow; }
            set
            {
                if (_commandWindow != value)
                {
                    _commandWindow = value;
                    NotifyOfPropertyChange(() => CommandWindow);
                }
            }
        }

        private bool isFullScreen = false;

        private readonly ShellViewModel _parentViewModel;

        private readonly IWindowManager _windowManager;

        private bool _isSecondaryWindow;

        [Import] private IConfig config;

        [Import] private TabService tabService;

        private ILeftPane _leftPanel;

        [Import]
        public ILeftPane LeftPanel
        {
            get { return _leftPanel; }
            set
            {
                if (_leftPanel != value)
                {
                    _leftPanel = value;
                    NotifyOfPropertyChange(() => LeftPanel);
                }
            }
        }

        public bool _leftPanelVisible;
        public bool LeftPanelVisible
        {
            get { return _leftPanelVisible; }
            set
            {
                if (_leftPanelVisible != value)
                {
                    _leftPanelVisible = value;
                    NotifyOfPropertyChange(() => LeftPanelVisible);
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

        [ImportingConstructor]
        public ShellViewModel([ImportMany] IEnumerable<Lazy<IActionItem, IActionItemMetadata>> actions, IWindowManager windowManager) : this(windowManager, null, false)
        {
            var enumeratedActions = actions as IList<Lazy<IActionItem, IActionItemMetadata>> ?? actions.ToList();
            FileMenuItems = enumeratedActions.Where(action => action.Metadata.Menu == "File")
                .OrderBy(action => action.Metadata.Order)
                .Select(action => action.Value);

            EditMenuItems = enumeratedActions.Where(action => action.Metadata.Menu == "Edit")
                .OrderBy(action => action.Metadata.Order)
                .Select(action => action.Value);

            PreferencesMenuItems = enumeratedActions.Where(action => action.Metadata.Menu == "Preferences")
                .OrderBy(action => action.Metadata.Order)
                .Select(action => action.Value);

            AboutMenuItems = enumeratedActions.Where(action => action.Metadata.Menu == "About")
                .OrderBy(action => action.Metadata.Order)
                .Select(action => action.Value);
        }

        public bool IsSecondaryWindow
        {
            get { return _isSecondaryWindow; }
            set
            {
                if (_isSecondaryWindow != value)
                {
                    _isSecondaryWindow = value;
                    NotifyOfPropertyChange(() => IsSecondaryWindow);
                }
            }
        }

        public void DetachItem(ITab item)
        {
            ShellViewModel shellViewModel = NewWindow();
            shellViewModel.Items.Add(item);
            shellViewModel.ActivateItem(item);
            Items.Remove(item);
        }


        public ShellViewModel NewWindow()
        {
            var shellViewModel = new ShellViewModel(_windowManager, _parentViewModel ?? this, true);
            _windowManager.ShowWindow(shellViewModel);
            return shellViewModel;
        }

        public void NewFile()
        {
            ITab item = tabService.RequestDefaultTab();
            OpenTab(item);
        }

        public void OpenFile()
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog().Value)
            {
                ITab fileTabViewModel = tabService.RequestDefaultTab();
                fileTabViewModel.SetFile(dialog.FileName);
                OpenTab(fileTabViewModel);
            }
        }

        private void OpenTab(ITab tab)
        {
            Items.Add(tab);
            ActivateItem(tab);
        }

        public void AttachBack()
        {
            _parentViewModel.Items.AddRange(Items);
            Items.Clear();
            TryClose();
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
            foreach (ITab tab in Items)
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
            ITab fileTabViewModel = tabService.RequestDefaultTab();
            fileTabViewModel.SetFile(Path.Combine(Folders.ProgramFolder, "config.json"));
            OpenTab(fileTabViewModel);
        }

        public void OpenUserConfig()
        {
            ITab fileTabViewModel = tabService.RequestDefaultTab();
            fileTabViewModel.SetFile(Path.Combine(Folders.UserFolder, "config.json"));
            OpenTab(fileTabViewModel);
        }


        public void CloseActiveItem()
        {
            if (ActiveItem != null) CloseItem(ActiveItem);
        }

        private void CloseItem(ITab item)
        {
            item.TryClose();
        }


        public void OpenDefaultPackages()
        {
            Process.Start(Folders.DefaultPackagesFolder);
        }

        public void OpenUserPackages()
        {
            Process.Start(Folders.UserPackagesFolder);
        }

        public void SetHighlighter(RoutedEventArgs eventArgs)
        {
            //if(ActiveItem != null) (ActiveItem as TextEditorViewModel).SetHighlighter(
            //    HighlighterManager.Instance.Highlighters[
            //    (eventArgs.OriginalSource as MenuItem).Header.ToString()]
            //    );
        }

        public void ToggleFullscreen()
        {
            var view = GetView() as Window;
            if (isFullScreen)
            {
                view.WindowState = WindowState.Normal;
                view.WindowStyle = WindowStyle.SingleBorderWindow;
                view.ResizeMode = ResizeMode.CanResize;
                view.Topmost = false;
                isFullScreen = false;
            }
            else
            {
                view.WindowState = WindowState.Maximized;
                view.WindowStyle = WindowStyle.None;
                view.ResizeMode = ResizeMode.NoResize;
                view.Topmost = true;
                isFullScreen = true;
            }

        }

        public void ToggleCommandWindow()
        {
            var view = this.GetView() as ShellView;
            if (view.CommandWindow.Visibility == Visibility.Collapsed)
            {
                view.CommandWindow.Visibility = Visibility.Visible;
            }
            else
            {
               view.CommandWindow.Visibility = Visibility.Collapsed;
            }
            
        }

        public void ToggleLeftPanel()
        {
            if (LeftPanelVisible)
            {
                LeftPanelVisible = false;
            }
            else
            {
                LeftPanelVisible = true;
            }

        }
    }
}
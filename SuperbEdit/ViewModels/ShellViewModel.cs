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

        public void OpenTab(ITab tab)
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

        public void Exit()
        {
            TryClose();
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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using SuperbEdit.Base;
using SuperbEdit.Shell.Views;

namespace SuperbEdit.Shell.ViewModels
{
    [Export(typeof (IShell))]
    [Export] // HACK: temporary hack to show and hide command window from actions
    internal sealed class ShellViewModel : Conductor<ITab>.Collection.OneActive, IShell
    {
        public IPanel LeftPanel
        {
            get; set;
        }

        private readonly ShellViewModel _parentViewModel;

        private readonly IWindowManager _windowManager;
        private CommandWindowViewModel _commandWindow;

        private bool _isSecondaryWindow;

        [Import] private IConfig config;
        private bool isFullScreen;

        public ShellViewModel(IWindowManager windowManager, ShellViewModel parent, bool secondaryWindow)
        {
            _windowManager = windowManager;
            IsSecondaryWindow = secondaryWindow;
            DisplayName = "SuperbEdit";
            _parentViewModel = parent;

        }

        [ImportingConstructor]
        public ShellViewModel(
            //TODO: one panel at the moment
            [Import] IPanel panel,
            [ImportMany] IEnumerable<Lazy<IActionItem, IActionItemMetadata>> actions,
            IWindowManager windowManager) : this(windowManager, null, false)
        {
            LeftPanel = panel;

            IList<Lazy<IActionItem, IActionItemMetadata>> enumeratedActions =
                actions as IList<Lazy<IActionItem, IActionItemMetadata>> ?? actions.ToList();
            FileMenuItems = PopulateMenu(enumeratedActions, "File");

            EditMenuItems = PopulateMenu(enumeratedActions, "Edit");

            ViewMenuItems = PopulateMenu(enumeratedActions, "View");

            PreferencesMenuItems = PopulateMenu(enumeratedActions, "Preferences");

            AboutMenuItems = PopulateMenu(enumeratedActions, "About");
        }

        public IEnumerable<IActionItem> FileMenuItems { get; set; }
        public IEnumerable<IActionItem> EditMenuItems { get; set; }
        public IEnumerable<IActionItem> ViewMenuItems { get; set; }
        public IEnumerable<IActionItem> PreferencesMenuItems { get; set; }
        public IEnumerable<IActionItem> AboutMenuItems { get; set; }

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


        public void OpenTab(ITab tab)
        {
            Items.Add(tab);
            ActivateItem(tab);
        }

        public void Exit()
        {
            TryClose();
        }

        private static IEnumerable<IActionItem> PopulateMenu(
            IEnumerable<Lazy<IActionItem, IActionItemMetadata>> enumeratedActions, string menu)
        {
            //TODO: insert separator in missing places.
            return enumeratedActions.Where(action => action.Metadata.Menu == menu)
                .OrderBy(action => action.Metadata.Order)
                .Select(action => action.Value);
        }

        public ShellViewModel NewWindow()
        {
            var shellViewModel = new ShellViewModel(_windowManager, _parentViewModel ?? this, true);
            _windowManager.ShowWindow(shellViewModel);
            return shellViewModel;
        }

        public void AttachBack()
        {
            _parentViewModel.Items.AddRange(Items);
            Items.Clear();
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
            var view = GetView() as ShellView;
            if (view.CommandWindow.Visibility == Visibility.Collapsed)
            {
                view.CommandWindow.Visibility = Visibility.Visible;
            }
            else
            {
                view.CommandWindow.Visibility = Visibility.Collapsed;
            }
        }


        public void ShowHidePanel(IPanel panel)
        {
            //TODO: actually should hide the correct panel
            var view = GetView() as ShellView;

            if (view.LeftPanel.Visibility == Visibility.Collapsed)
            {
                view.LeftPanel.Visibility = Visibility.Visible;
            }
            else
            {
                view.LeftPanel.Visibility = Visibility.Collapsed;
            } 
        }
    }
}
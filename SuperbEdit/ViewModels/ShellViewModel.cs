using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using SuperbEdit.Base;
using SuperbEdit.Views;
using System.Windows.Input;
using SuperbEdit.Converters;
using System.Threading.Tasks;
using System.Threading;

namespace SuperbEdit.ViewModels
{
    [Export(typeof(IShell))]
    [Export] // HACK: temporary hack to show and hide command window from actions
    public sealed class ShellViewModel : Conductor<ITab>.Collection.OneActive, IShell
    {
        private string _lastMessage;
        public string LastMessage
        {
            get { return _lastMessage; }
            private set
            {
                _lastMessage = value;
                NotifyOfPropertyChange(() => LastMessage);
            }
        }

        public IEnumerable<IPanel> Panels
		{
            get;
            set;
        }

        private readonly ShellViewModel _parentViewModel;

        private readonly IWindowManager _windowManager;
        private CommandWindowViewModel _commandWindow;

        private bool _isSecondaryWindow;

        [Import]
        private IConfig config;


        [Import]
        private CommandLineReader cmdLineReader;
        private bool isFullScreen;

        public ShellViewModel(IWindowManager windowManager, ShellViewModel parent, bool secondaryWindow)
        {
            _windowManager = windowManager;
            IsSecondaryWindow = secondaryWindow;
            DisplayName = "SuperbEdit";
            _parentViewModel = parent;

        }

        protected override void OnActivate()
        {
            base.OnActivate();
            cmdLineReader.ExecuteCommandLine();
        }

        [ImportingConstructor]
        public ShellViewModel(
            [ImportMany] IEnumerable<IPanel> panels,
            [ImportMany] IEnumerable<Lazy<IActionItem, IActionItemMetadata>> actions,
            IWindowManager windowManager)
            : this(windowManager, null, false)
        {
            Panels = panels;

            IList<Lazy<IActionItem, IActionItemMetadata>> enumeratedActions =
                actions as IList<Lazy<IActionItem, IActionItemMetadata>> ?? actions.ToList();
            MenuItems = enumeratedActions.Where(action => action.Metadata.Menu == "Root")
                .OrderBy(action => action.Metadata.Order)
                .Select(action => action.Value);

            StringToKeyBinding converter = new StringToKeyBinding();

            InputBindingCollection inputBindings = new InputBindingCollection();
            foreach (var action in enumeratedActions)
            {
                if (!string.IsNullOrEmpty(action.Value.Shortcut))
                {
                    KeyGestureConverter keyConv = new KeyGestureConverter();
                    KeyGesture gesture = (KeyGesture)keyConv.ConvertFromString(action.Value.Shortcut);
                    InputBinding binding = new InputBinding(action.Value, gesture);

                    inputBindings.Add(binding);
                }
            }

            GlobalInputBindings = inputBindings;

            EchoMessage("SuperbEdit has been loaded...");
        }


        public InputBindingCollection GlobalInputBindings
        {
            get;
            set;
        }

        public IEnumerable<IActionItem> MenuItems { get; set; }

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


        }


        public async void EchoMessage(string message)
        {
            LastMessage = message;
            await Task.Run(() => { Thread.Sleep(5000); });
            LastMessage = "Ready";
        }
        //Helper method for closing the active document
        public void Close(ITab tab)
        {
            bool? result = false;
            tab.TryClose(result);
        }
    }
}
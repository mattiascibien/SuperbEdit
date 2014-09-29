using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Win32;
using SuperbEdit.Base;

namespace SuperbEdit.ViewModels
{
    [Export(typeof (IShell))]
    public sealed class ShellViewModel : Conductor<ITab>.Collection.OneActive, IShell
    {
        private readonly ShellViewModel _parentViewModel;

        private readonly IWindowManager _windowManager;

        private bool _isSecondaryWindow;

        [Import] private ExportFactory<ITab> tabFactory;

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
            ITab item = tabFactory.CreateExport().Value;
            OpenTab(item);
        }

        public void OpenFile()
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog().Value)
            {
                ITab fileTabViewModel = tabFactory.CreateExport().Value;
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
            TryClose();
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
            ITab fileTabViewModel = tabFactory.CreateExport().Value;
            fileTabViewModel.SetFile(Path.Combine(Folders.ProgramFolder, "config.json"));
            OpenTab(fileTabViewModel);
        }

        public void OpenUserConfig()
        {
            ITab fileTabViewModel = tabFactory.CreateExport().Value;
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


        public void SetHighlighter(RoutedEventArgs eventArgs)
        {
            //if(ActiveItem != null) (ActiveItem as TextEditorViewModel).SetHighlighter(
            //    HighlighterManager.Instance.Highlighters[
            //    (eventArgs.OriginalSource as MenuItem).Header.ToString()]
            //    );
        }
    }
}
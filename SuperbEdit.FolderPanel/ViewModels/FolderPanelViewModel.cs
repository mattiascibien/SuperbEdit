using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using SuperbEdit.Base;
using SuperbEdit.FolderPanel.Model;

namespace SuperbEdit.FolderPanel.ViewModels
{
    [ExportPanel]
    [Export]
    public class FolderPanelViewModel : Panel
    {
        [Import]
        private Lazy<IShell> shell;
        [Import]
        private TabService tabService;

        public FolderPanelViewModel()
        {
            DisplayName = "Folder Panel";
        }

        private ObservableCollection<TreeItemModel> _rootItems;
        public ObservableCollection<TreeItemModel> RootItems
        {
            get { return _rootItems; }
            set
            {
                if (_rootItems != value)
                {
                    _rootItems = value;
                    NotifyOfPropertyChange(() => RootItems);
                }
            }
        }

        private string _currentFolder;
        public string CurrentFolder
        {
            get { return _currentFolder; }

            set
            {
                if (_currentFolder != value)
                {
                    _currentFolder = value;
                    PopulateTree();
                    NotifyOfPropertyChange(() => CurrentFolder);
                }
            }
        }

        private void PopulateTree()
        {
            RootItems = new ObservableCollection<TreeItemModel>();
            string[] subdirs = Directory.GetDirectories(CurrentFolder);
            foreach (var dir in subdirs)
            {
                TreeItemModel rootItem = new TreeItemModel()
                {
                    Text = Path.GetFileName(dir),
                    FullPath = dir
                };
                PopulateChildren(rootItem, dir);
                RootItems.Add(rootItem);
            }

            string[] files = Directory.GetFiles(CurrentFolder);
            foreach (var file in files)
            {
                TreeItemModel rootItem = new TreeItemModel()
                {
                    Text = Path.GetFileName(file),
                    FullPath = file,
                    IsFile = true
                };

                RootItems.Add(rootItem);
            }
        }


        private void PopulateChildren(TreeItemModel item, string dir)
        {
            try
            {


                string[] subdirs = Directory.GetDirectories(dir);
                foreach (var subdir in subdirs)
                {
                    TreeItemModel childItem = new TreeItemModel()
                    {
                        Text = Path.GetFileName(subdir),
                        FullPath = subdir
                    };
                    PopulateChildren(childItem, subdir);
                    item.Children.Add(childItem);
                }

                string[] files = Directory.GetFiles(dir);
                foreach (var file in files)
                {
                    TreeItemModel childItem = new TreeItemModel()
                    {
                        Text = Path.GetFileName(file),
                        FullPath = file,
                        IsFile = true
                    };

                    item.Children.Add(childItem);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "SuperbEdit", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        public void OpenItem(TreeItemModel item)
        {
            if (item.IsFile)
            {
                ITab fileTabViewModel = tabService.RequestDefaultTab();
                fileTabViewModel.SetFile(item.FullPath);
                shell.Value.OpenTab(fileTabViewModel);
            }
        }

        public override double PreferredWidth
        {
            get { return 150.0; }
        }

        public override double PreferredHeight
        {
            get { return 150.0; }
        }
    }
}
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Win32;
using SuperbEdit.Base;
using SuperbEdit.TextEditor.Views;

namespace SuperbEdit.TextEditor.ViewModels
{
    [ExportTab(Name="TextEditor")]
    public sealed class TextEditorViewModel : Tab
    {
        private string _fileContent;
        private string _filePath;

        private string _originalFileContent = "";


        public TextEditorViewModel()
        {
            DisplayName = "New File";

            _originalFileContent = "";
            FileContent = _originalFileContent;
            FilePath = "";
        }

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    NotifyOfPropertyChange(() => FilePath);
                }
            }
        }

        public string FileContent
        {
            get { return _fileContent; }
            set
            {
                if (_fileContent != value)
                {
                    _fileContent = value;
                    HasChanges = _originalFileContent != _fileContent;
                    NotifyOfPropertyChange(() => FileContent);
                }
            }
        }

        public override bool Save()
        {
            if (FilePath != "")
            {
                File.WriteAllText(FilePath, FileContent);
                _originalFileContent = FileContent;
                HasChanges = false;
                DisplayName = Path.GetFileName(FilePath);
                return true;
            }
            return SaveAs();
        }

        public override bool SaveAs()
        {
            var dialog = new SaveFileDialog();

            if (dialog.ShowDialog().Value)
            {
                FilePath = dialog.FileName;
                _originalFileContent = FileContent;
                File.WriteAllText(FilePath, FileContent);
                HasChanges = false;
                DisplayName = Path.GetFileName(FilePath);
                return true;
            }
            return false;
        }

        public override void Undo()
        {
            var view = GetView() as TextEditorView;

            view.ModernTextEditor.Undo();
        }

        public override void Redo()
        {
            var view = GetView() as TextEditorView;
            view.ModernTextEditor.Redo();
        }

        public override void Cut()
        {
            var view = GetView() as TextEditorView;
            view.ModernTextEditor.Cut();
        }

        public override void Copy()
        {
            var view = GetView() as TextEditorView;
            view.ModernTextEditor.Copy();
        }

        public override void Paste()
        {
            var view = GetView() as TextEditorView;
            view.ModernTextEditor.Paste();
        }

        public override void SetFile(string filePath)
        {
            if (filePath == "")
            {
                DisplayName = "New File";

                _originalFileContent = "";
                FileContent = _originalFileContent;
                FilePath = "";
            }
            else
            {
                FilePath = filePath;
                DisplayName = Path.GetFileName(filePath);

                if (!File.Exists(FilePath))
                {
                    string directoryName = Path.GetDirectoryName(FilePath);
                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    File.Create(FilePath).Dispose();
                }
                string fileContents = File.ReadAllText(FilePath);
                _originalFileContent = fileContents;
                FileContent = _originalFileContent;
            }
        }

        public override void RegisterCommands()
        {
        }

        public override void CanClose(Action<bool> callback)
        {
            if (HasChanges)
            {
                switch (
                    MessageBox.Show("Save Changes to " + DisplayName + "?", "SuperbEdit", MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        callback(Save());
                        break;
                    case MessageBoxResult.No:
                        callback(true);
                        break;
                    case MessageBoxResult.Cancel:
                        callback(false);
                        break;
                }
            }
            else
            {
                callback(true);
            }
        }

        public void DetachItem(Tab item)
        {
            var shell = item.Parent as IShell;
            shell.DetachItem(item);
        }

        public void CloseItem(Tab item)
        {
            item.TryClose();
        }
    }
}
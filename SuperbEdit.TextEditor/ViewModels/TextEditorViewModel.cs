using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using Microsoft.Win32;
using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperbEdit.TextEditor.Views;
using AurelienRibon.Ui.SyntaxHighlightBox;

namespace SuperbEdit.TextEditor.ViewModels
{
    [Export(typeof(ITab))]
    public sealed class TextEditorViewModel : Tab
    {
        private string _filePath;
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    NotifyOfPropertyChange(() => FilePath);
                }
            }
        }

        private string _originalFileContent = "";

        private string _fileContent;
        public string FileContent
        {
            get
            {
                return _fileContent;
            }
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


        public TextEditorViewModel()
        {
            DisplayName = "New File";

            _originalFileContent = "";
            FileContent = _originalFileContent;
            FilePath = "";
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
            SaveFileDialog dialog = new SaveFileDialog();

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
            var view = this.GetView() as TextEditorView;

            (view.FileContent as TextBox).Undo();
        }

        public override void Redo()
        {
            var view = this.GetView() as TextEditorView;
            (view.FileContent as TextBox).Redo();
        }

        public override void Cut()
        {
            var view = this.GetView() as TextEditorView;
            (view.FileContent as TextBox).Cut();
        }

        public override void Copy()
        {
            var view = this.GetView() as TextEditorView;
            (view.FileContent as TextBox).Copy();
        }

        public override void Paste()
        {
            var view = this.GetView() as TextEditorView;
            (view.FileContent as TextBox).Paste();
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


        public void CloseItem(Tab item)
        {
           ScreenExtensions.CloseItem((IConductor)item.Parent, item);
        }


        public void SetHighlighter(IHighlighter highlighter)
        {
            var view = this.GetView() as TextEditorView;
            (view.FileContent as SyntaxHighlightBox).CurrentHighlighter = highlighter;
        }
    }
}

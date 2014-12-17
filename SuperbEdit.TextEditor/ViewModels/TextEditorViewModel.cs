using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Win32;
using SuperbEdit.Base;
using SuperbEdit.TextEditor.Views;
using System.Windows.Media;

namespace SuperbEdit.TextEditor.ViewModels
{
    [ExportTab(Name="TextEditor")]
    public sealed class TextEditorViewModel : Tab
    {
        private string _fileContent;
        private string _filePath;

        private string _originalFileContent = "";

        [ImportingConstructor]
        public TextEditorViewModel(IConfig config) : base(config)
        {
            DisplayName = "New File";
            _originalFileContent = "";
            FileContent = _originalFileContent;
            FilePath = "";
        }


        protected override void ReloadConfig(IConfig config)
        {
            ShowLineNumbers = config.RetrieveConfigValue<bool>("text_editor.show_line_numbers", true);
            WordWrap = config.RetrieveConfigValue<bool>("text_editor.wrapping", false);
            FontFamily = new FontFamily(config.RetrieveConfigValue<string>("text_editor.font_family", "Consolas"));
            FontSize = (double)new FontSizeConverter().ConvertFrom(config.RetrieveConfigValue<string>("text_editor.font_size", "10pt"));
        }


        private bool _showLineNumbers = true;
        public bool ShowLineNumbers
        {
            get
            {
                return _showLineNumbers;
            }
            set
            {
                if(_showLineNumbers != value)
                {
                    _showLineNumbers = value;
                    NotifyOfPropertyChange(() => ShowLineNumbers);
                }
            }
        }

        private bool _wordWrap = true;
        public bool WordWrap
        {
            get
            {
                return _wordWrap;
            }
            set
            {
                if (_wordWrap != value)
                {
                    _wordWrap = value;
                    NotifyOfPropertyChange(() => WordWrap);
                }
            }
        }

        private FontFamily _fontFamily;
        public FontFamily FontFamily
        {
            get
            {
                return _fontFamily;
            }
            set
            {
                if (_fontFamily != value)
                {
                    _fontFamily = value;
                    NotifyOfPropertyChange(() => FontFamily);
                }
            }
        }

        private double _fontSize;
        public double FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    NotifyOfPropertyChange(() => FontSize);
                }
            }
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

        public override string FileContent
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

                FileInfo fileInfo = new FileInfo(FilePath);
                IsReadOnly = fileInfo.IsReadOnly;
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

        private bool _isReadOnly = false;
        public override bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
            set
            {
                _isReadOnly = value;
                NotifyOfPropertyChange(() => IsReadOnly);
            }
        }
    }
}
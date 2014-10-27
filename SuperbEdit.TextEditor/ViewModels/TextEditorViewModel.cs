using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Microsoft.Win32;
using SuperbEdit.Base;
using SuperbEdit.TextEditor.Views;

namespace SuperbEdit.TextEditor.ViewModels
{
    [Export(typeof(ITab))]
    [ExportMetadata("IsFallback", true)]
    [ExportMetadata("Name", "TextEditor")]
    public sealed class TextEditorViewModel : Tab
    {
        private IConfig _config;

        private string _fileContent;
        private string _filePath;

        private string _originalFileContent = "";

        private FontFamily _fontFamilyConfig;
        public FontFamily FontFamilyConfig
        {
            get { return _fontFamilyConfig; }
            set
            {
                if (_fontFamilyConfig != value)
                {
                    _fontFamilyConfig = value;
                    NotifyOfPropertyChange(() => FontFamilyConfig);
                }
            }
        }

        private double _fontSize;
        public double FontSizeConfig
        {
            get { return _fontSize; }
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    NotifyOfPropertyChange(() => FontSizeConfig);
                }
            }
        }


        private bool _wrapping;
        public bool Wrapping
        {
            get { return _wrapping; }
            set
            {
                if (_wrapping != value)
                {
                    _wrapping = value;
                    NotifyOfPropertyChange(() => Wrapping);
                }
            }
        }

        [ImportingConstructor]
        public TextEditorViewModel([Import] IConfig config)
        {
            _config = config;
            DisplayName = "New File";

            _originalFileContent = "";
            FileContent = _originalFileContent;
            FilePath = "";

            FontFamilyConfig = new FontFamily(_config.RetrieveConfigValue<string>("text_editor.font_family", "Consolas"));
            FontSizeConfig = _config.RetrieveConfigValue<double>("text_editor.font_size", 12.0);
            Wrapping = _config.RetrieveConfigValue<bool>("text_editor.wrapping", false);
            _config.ChangeConfig += ConfigOnChangeConfig;
        }

        private void ConfigOnChangeConfig(object sender, EventArgs eventArgs)
        {
            FontFamilyConfig = new FontFamily(_config.RetrieveConfigValue<string>("text_editor.font_family", "Consolas"));
            FontSizeConfig = _config.RetrieveConfigValue<double>("text_editor.font_size", 12.0);
            Wrapping = _config.RetrieveConfigValue<bool>("text_editor.wrapping", false);
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

            view.FileContent.Undo();
        }

        public override void Redo()
        {
            var view = GetView() as TextEditorView;
            view.FileContent.Redo();
        }

        public override void Cut()
        {
            var view = GetView() as TextEditorView;
            view.FileContent.Cut();
        }

        public override void Copy()
        {
            var view = GetView() as TextEditorView;
            view.FileContent.Copy();
        }

        public override void Paste()
        {
            var view = GetView() as TextEditorView;
            view.FileContent.Paste();
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
using Caliburn.Micro;
using Microsoft.Win32;
using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.ViewModels
{
    public sealed class FileTabViewModel : Tab
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

        public FileTabViewModel()
        {
            DisplayName = "New File";

            _originalFileContent = "";
            FileContent = _originalFileContent;
            FilePath = "";
        }

        public FileTabViewModel(string filePath)
        {
            FilePath = filePath;
            DisplayName = Path.GetFileName(filePath);

            string fileContents = File.ReadAllText(FilePath);
            _originalFileContent = fileContents;
            FileContent = _originalFileContent;
            
        }

        public override void Save()
        {
            if (FilePath != "")
            {
                File.WriteAllText(FilePath, FileContent);
                _originalFileContent = FileContent;
                HasChanges = false;
            }
            else
            {
                SaveAs();
            }

            DisplayName = Path.GetFileName(FilePath);
        }

        public override void SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();

            if (dialog.ShowDialog().Value)
            {
                FilePath = dialog.SafeFileName;
                _originalFileContent = FileContent;
                File.WriteAllText(FilePath, FileContent);
                HasChanges = false;
            }

            DisplayName = Path.GetFileName(FilePath);
        }

        public override void Undo()
        {
            
        }

        public override void Redo()
        {
           
        }

    }
}

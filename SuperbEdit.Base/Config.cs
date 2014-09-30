using System;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.IO;
using Caliburn.Micro;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SuperbEdit.Base
{
    [Export(typeof (IConfig))]
    public class Config : PropertyChangedBase, IConfig, IDisposable
    {
        private FileSystemWatcher _defaultConfigWatcher;
        private FileSystemWatcher _userConfigWatcher;

        private dynamic _userConfigExpandoObject;
        public dynamic UserConfig
        {
            get { return _userConfigExpandoObject; }
            private set
            {
                if (_userConfigExpandoObject != value)
                {
                    _userConfigExpandoObject = value;
                    NotifyOfPropertyChange(() => UserConfig);
                }
            }
        }

        private dynamic _defaultConfigExpandoObject;
        public dynamic DefaultConfig
        {
            get { return _defaultConfigExpandoObject; }
            private set
            {
                if (_defaultConfigExpandoObject != value)
                {
                    _defaultConfigExpandoObject = value;
                    NotifyOfPropertyChange(() => DefaultConfig);
                }
            }
        }

        [ImportingConstructor]
        public Config()
        {
            ReloadConfig(_userConfigExpandoObject, Path.Combine(Folders.UserFolder, "config.json"));
            ReloadConfig(_defaultConfigExpandoObject,  Path.Combine(Folders.ProgramFolder, "config.json"));

            _defaultConfigWatcher = new FileSystemWatcher(Folders.ProgramFolder);
            _defaultConfigWatcher.Filter = "config.json";
            _userConfigWatcher = new FileSystemWatcher(Folders.UserFolder);
            _userConfigWatcher.Filter = "config.json";

            _defaultConfigWatcher.EnableRaisingEvents = true;
            _userConfigWatcher.EnableRaisingEvents = true;

            _defaultConfigWatcher.Changed += ConfigChanged;
            _userConfigWatcher.Changed += ConfigChanged;
        }

        private void ConfigChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            ReloadConfig(_userConfigExpandoObject, fileSystemEventArgs.FullPath);
        }

        private void ReloadConfig(dynamic configObject, string fullPath)
        {
            string jsonString = File.ReadAllText(fullPath);

            var converter = new ExpandoObjectConverter();
            configObject = JsonConvert.DeserializeObject<ExpandoObject>(jsonString, converter);
        }


        public void Dispose()
        {
            _defaultConfigWatcher.Changed -= ConfigChanged;
            _userConfigWatcher.Changed -= ConfigChanged;
        }
    }
}
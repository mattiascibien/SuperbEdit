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
        private readonly FileSystemWatcher _defaultConfigWatcher;
        private readonly FileSystemWatcher _userConfigWatcher;

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
            ReloadConfig(false, Path.Combine(Folders.UserFolder, "config.json"));
            ReloadConfig(true,  Path.Combine(Folders.ProgramFolder, "config.json"));

            _defaultConfigWatcher = new FileSystemWatcher(Folders.ProgramFolder) {Filter = "config.json"};
            _userConfigWatcher = new FileSystemWatcher(Folders.UserFolder) {Filter = "config.json"};

            _defaultConfigWatcher.EnableRaisingEvents = true;
            _userConfigWatcher.EnableRaisingEvents = true;

            _defaultConfigWatcher.Changed += DefaultConfigChanged;
            _userConfigWatcher.Changed += UserConfigChanged;
        }

        private void DefaultConfigChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            ReloadConfig(true, fileSystemEventArgs.FullPath);
        }

        private void UserConfigChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            ReloadConfig(false, fileSystemEventArgs.FullPath);
        }

        private void ReloadConfig(bool defaultConfig, string fullPath)
        {
            string jsonString = File.ReadAllText(fullPath);

            var converter = new ExpandoObjectConverter();
            if (defaultConfig)
                DefaultConfig = JsonConvert.DeserializeObject<ExpandoObject>(jsonString, converter);
            else
                UserConfig = JsonConvert.DeserializeObject<ExpandoObject>(jsonString, converter); 
        }


        public void Dispose()
        {
            _defaultConfigWatcher.Changed -= DefaultConfigChanged;
            _userConfigWatcher.Changed -= UserConfigChanged;
        }
    }
}
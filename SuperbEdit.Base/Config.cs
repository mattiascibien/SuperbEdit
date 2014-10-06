using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.IO;
using System.Linq;
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
        private dynamic _defaultConfigExpandoObject;

        private dynamic _userConfigExpandoObject;

        [ImportingConstructor]
        public Config()
        {
            ReloadConfig(false, Path.Combine(Folders.UserFolder, "config.json"));
            ReloadConfig(true, Path.Combine(Folders.ProgramFolder, "config.json"));

            _defaultConfigWatcher = new FileSystemWatcher(Folders.ProgramFolder) {Filter = "config.json"};
            _userConfigWatcher = new FileSystemWatcher(Folders.UserFolder) {Filter = "config.json"};

            _defaultConfigWatcher.EnableRaisingEvents = true;
            _userConfigWatcher.EnableRaisingEvents = true;

            _defaultConfigWatcher.Changed += DefaultConfigChanged;
            _userConfigWatcher.Changed += UserConfigChanged;
        }

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

        public T RetrieveConfigValue<T>(string path)
        {
            List<string> properties = path.Split(new[] {'.'}).ToList();

            T userConfigValue = TraverseConfig<T>(UserConfig, properties, 0);

            if (userConfigValue == null || userConfigValue.Equals(default(T)))
            {
                return TraverseConfig<T>(DefaultConfig, properties, 0);
            }

            return userConfigValue;
        }

        public void Dispose()
        {
            _defaultConfigWatcher.Changed -= DefaultConfigChanged;
            _userConfigWatcher.Changed -= UserConfigChanged;
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
            if (File.Exists(fullPath))
            {
                string jsonString = File.ReadAllText(fullPath);

                var converter = new ExpandoObjectConverter();
                if (defaultConfig)
                    DefaultConfig = JsonConvert.DeserializeObject<ExpandoObject>(jsonString, converter);
                else
                    UserConfig = JsonConvert.DeserializeObject<ExpandoObject>(jsonString, converter);
            }
        }


        /// <summary>
        ///     Method to traverse the config properties to the bottom
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        private T TraverseConfig<T>(dynamic config, List<string> properties, int index)
        {
            if (index == properties.Count - 1)
            {
                if (((IDictionary<string, Object>) config).Keys.Contains(properties[index]))
                {
                    return (T) (((IDictionary<string, Object>) config)[properties[index]]);
                }
            }

            if (((IDictionary<string, Object>) config).Keys.Contains(properties[index]))
            {
                return TraverseConfig<T>(((IDictionary<string, Object>) config)[properties[index]], properties, ++index);
            }

            return default(T);
        }
    }
}
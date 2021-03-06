using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SuperbEdit.Base
{
    [Export(typeof (IConfig))]
    internal class Config : PropertyChangedBase, IConfig, IDisposable
    {
        private readonly FileSystemWatcher _defaultConfigWatcher;
        private readonly FileSystemWatcher _userConfigWatcher;
        private dynamic _defaultConfigExpandoObject;

        private dynamic _userConfigExpandoObject;

        //Used only for test
        internal Config(string defaultConfig, string userConfig)
        {
            CreateUserFolderIfNotExsist(userConfig);

            ReloadConfig(false, userConfig);
            ReloadConfig(true, defaultConfig);

            _defaultConfigWatcher = new FileSystemWatcher(Path.GetDirectoryName(defaultConfig)) {Filter = Path.GetFileName(defaultConfig)};

            _userConfigWatcher = new FileSystemWatcher(Path.GetDirectoryName(userConfig)) { Filter = Path.GetFileName(userConfig) };

            _defaultConfigWatcher.EnableRaisingEvents = true;
            _userConfigWatcher.EnableRaisingEvents = true;

            _defaultConfigWatcher.Changed += DefaultConfigChanged;
            _userConfigWatcher.Changed += UserConfigChanged;
        }

        private void CreateUserFolderIfNotExsist(string userConfig)
        {
            if (!Directory.Exists(Path.GetDirectoryName(userConfig)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(userConfig));
            }
        }

        [ImportingConstructor]
        public Config()
            : this(Path.Combine(Folders.ProgramFolder, "config.json"),
                Path.Combine(Folders.UserFolder, "config.json"))
        {
            
        }

        public dynamic UserConfig
        {
            get { return _userConfigExpandoObject; }
            private set
            {
                if (_userConfigExpandoObject != value)
                {
                    _userConfigExpandoObject = value;
                    if (ConfigChanged != null)
                        ConfigChanged(this, new EventArgs());
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
                    if (ConfigChanged != null)
                        ConfigChanged(this, new EventArgs());
                    NotifyOfPropertyChange(() => DefaultConfig);
                }
            }
        }

        public T RetrieveConfigValue<T>(string path)
        {
            List<string> properties = path.Split(new[] {'.'}).ToList();
            if (UserConfig != null)
            {
                T userConfigValue = TraverseConfig<T>(UserConfig, properties, 0);
                if (userConfigValue != null)
                {
                    return userConfigValue;
                }
                
            }
            return TraverseConfig<T>(DefaultConfig, properties, 0);
        }

        public T RetrieveConfigValue<T>(string path, T defaultValue)
        {
            T configValue = RetrieveConfigValue<T>(path);

            if (configValue == null)
            {
                configValue = defaultValue;
            }

            return configValue;
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

        public event EventHandler ConfigChanged;
    }
}
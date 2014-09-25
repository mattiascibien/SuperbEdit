using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit
{
    public class Config : IConfig
    {
        private FileSystemWatcher _defaultConfigWatcher;
        private FileSystemWatcher _userConfigWatcher;


        public Config()
        {
            _defaultConfigWatcher = new FileSystemWatcher(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location));
            _defaultConfigWatcher.Filter = "config.json";
            _userConfigWatcher = new FileSystemWatcher(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".superbedit"));
            _userConfigWatcher.Filter = "config.json";

            _defaultConfigWatcher.EnableRaisingEvents = true;
            _userConfigWatcher.EnableRaisingEvents = true;

            _defaultConfigWatcher.Changed += DefaultConfigWatcherOnChanged;
            _userConfigWatcher.Changed += UserConfigWatcherOnChanged;
        }

        private void UserConfigWatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            
        }

        private void DefaultConfigWatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            
        }
    }
}

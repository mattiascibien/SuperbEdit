using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    [Export(typeof(IConfig))]
    public class Config : IConfig
    {
        private FileSystemWatcher _defaultConfigWatcher;
        private FileSystemWatcher _userConfigWatcher;


        private IFolders _folders;


        [ImportingConstructor]
        public Config(IFolders folders)
        {
            _folders = folders;
            _defaultConfigWatcher = new FileSystemWatcher(folders.ProgramFolder);
            _defaultConfigWatcher.Filter = "config.json";
            _userConfigWatcher = new FileSystemWatcher(folders.UserFolder);
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

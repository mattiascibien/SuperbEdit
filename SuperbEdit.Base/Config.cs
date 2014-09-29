using System.ComponentModel.Composition;
using System.IO;

namespace SuperbEdit.Base
{
    [Export(typeof (IConfig))]
    public class Config : IConfig
    {
        private FileSystemWatcher _defaultConfigWatcher;
        private FileSystemWatcher _userConfigWatcher;


        [ImportingConstructor]
        public Config()
        {
            _defaultConfigWatcher = new FileSystemWatcher(Folders.ProgramFolder);
            _defaultConfigWatcher.Filter = "config.json";
            _userConfigWatcher = new FileSystemWatcher(Folders.UserFolder);
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
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Squirrel;
using SuperbEdit.Base;

namespace SuperbEdit.Update.Actions
{
    [ExportAction(Menu = "Help", Order = 1, Owner = "Updater", RegisterInCommandWindow = true)]
    public class CheckUpdatesAction : ActionItem
    {
        [Import] 
        private Lazy<IShell> _shell;

        [Import] 
        private Lazy<IConfig> _config; 

        public CheckUpdatesAction()
            :base("Check for Updates", "Check on the Internet For Updates")
        {
            
        }
        public override async void Execute()
        {
            string channel = _config.Value.RetrieveConfigValue<string>("update.channel");
            _shell.Value.EchoMessage("Checking for updates");
            using (var mgr = new UpdateManager(string.Format("https://superbedit.azurewebsites.net/{0}", channel), "SuperbEdit", FrameworkVersion.Net45))
            {
                var updateInfo = await mgr.CheckForUpdate();

                if (updateInfo.CurrentlyInstalledVersion.Version < updateInfo.FutureReleaseEntry.Version)
                {
                    if (MessageBox.Show("A new update (version: {0}) is available.\nDO you wish to install it now?",
                        "SueperbEdit Update", MessageBoxButton.YesNo, MessageBoxImage.Information) ==
                        MessageBoxResult.Yes)
                    {
                        await mgr.DownloadReleases(updateInfo.ReleasesToApply);
                        await mgr.ApplyReleases(updateInfo);
                    }
                }
            }
        }
    }
}

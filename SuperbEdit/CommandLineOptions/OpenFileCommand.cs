using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.CommandLineOptions
{
    [ExportCommandLineOption]
    public class OpenFileCommand : ICommandLineOption
    {
        [Import]
        Lazy<IShell> shell;

        [Import]
        TabService tabService;

        public string ShortCommand
        {
            get { return "f"; }
        }

        public string Command
        {
            get { return "file="; }
        }

        public Action<string> Action
        {
            get
            {
                return new Action<string>((str) =>
                {
                    ITab tab = tabService.RequestDefaultTab();
                    tab.SetFile(str);
                    shell.Value.OpenTab(tab);
                });
            }
        }
    }
}

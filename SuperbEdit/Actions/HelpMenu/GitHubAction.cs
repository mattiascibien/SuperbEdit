using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "Additional Resources", Order = 1, Owner = "Shell", RegisterInCommandWindow = false)]
    public class GitHubAction : ActionItem
    {
        public GitHubAction()
            : base("GitHub", "Opens the official GitHub Repository")
        {
        }

        public override void Execute()
        {
            Process.Start("http://github.com/superbedit");
        }
    }
}

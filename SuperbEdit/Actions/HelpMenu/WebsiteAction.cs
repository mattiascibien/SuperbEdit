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
    [ExportAction(Menu = "Additional Resources", Order = 0, Owner = "Shell", RegisterInCommandWindow = false)]
    public class WebsiteAction : ActionItem
    {
        public WebsiteAction()
            : base("Official Website", "Opens the official website")
        {
        }

        public override void Execute()
        {
            Process.Start("http://superbedit.github.io");
        }
    }
}

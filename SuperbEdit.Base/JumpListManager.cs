using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;

namespace SuperbEdit.Base
{
    [Export]
    internal class JumpListManager
    {
        [ImportingConstructor]
        internal JumpListManager([ImportMany] IEnumerable<IJumpListItem> items)
        {
            JumpList jumpList = new JumpList();

            foreach (var item in items)
            {
                JumpTask task = new JumpTask();
                task.ApplicationPath = Assembly.GetEntryAssembly().Location;
                task.Arguments = item.CommandLineArguments;
                task.Description = item.Description;
                task.Title = item.Title;

                jumpList.JumpItems.Add(task);
            }

            JumpList.SetJumpList(Application.Current, jumpList);
        }

    }
}

using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.JumpLists
{
    [ExportJumpListItem]
    public class NewFileJumpList : JumpListItem
    {
        public NewFileJumpList()
            : base("New File", "Open A New File", "/newfile")
        {

        }
    }
}

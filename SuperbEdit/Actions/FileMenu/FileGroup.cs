using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "Root", Order = 0, Owner = "Shell", RegisterInCommandWindow = false)]
    public class FileGroup : GroupItem
    {
        [ImportingConstructor]
        public FileGroup([ImportMany] IEnumerable<Lazy<IActionItem, IActionItemMetadata>> possibilechildren)
            : base(possibilechildren, "File")
        {

        }
    }
}

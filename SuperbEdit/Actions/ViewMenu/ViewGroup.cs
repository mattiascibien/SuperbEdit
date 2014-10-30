using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Actions
{
    [ExportAction(Menu = "Root", Order = 2, Owner = "Shell", RegisterInCommandWindow = false)]
    public class ViewGroup : GroupItem
    {
        [ImportingConstructor]
        public ViewGroup([ImportMany] IEnumerable<Lazy<IActionItem, IActionItemMetadata>> possibilechildren)
            : base(possibilechildren, "View")
        {

        }
    }
}

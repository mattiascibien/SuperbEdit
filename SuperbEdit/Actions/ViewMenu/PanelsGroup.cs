using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Actions.ViewMenu
{
    [ExportAction(Menu = "View", Order = 2, Owner = "Shell", RegisterInCommandWindow = false)]
    public class PanelsGroup : GroupItem
    {
        [ImportingConstructor]
        public PanelsGroup([ImportMany] IEnumerable<Lazy<IActionItem, IActionItemMetadata>> possibilechildren)
            : base(possibilechildren, "Panels")
        {

        }
    }
}

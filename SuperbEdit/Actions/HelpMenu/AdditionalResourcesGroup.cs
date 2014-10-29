using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Actions
{
    [Export(typeof(IActionItem))]
    [ExportActionMetadata(Menu = "Help", Order = 0, Owner = "Shell", RegisterInCommandWindow = false)]
    public class AdditionalResourcesGroup : GroupItem
    {
        [ImportingConstructor]
        public AdditionalResourcesGroup([ImportMany] IEnumerable<Lazy<IActionItem, IActionItemMetadata>> possibilechildren)
            : base(possibilechildren, "Additional Resources")
        {

        }
    }
}

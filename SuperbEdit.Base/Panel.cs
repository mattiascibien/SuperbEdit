using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    public abstract class Panel : IPanel
    {
        public PanelPosition DefaultPosition { get; private set; }

        public PanelPosition Position
        {
            get; set;
        }
    }
}

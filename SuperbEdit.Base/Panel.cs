using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace SuperbEdit.Base
{
    public abstract class Panel : Screen, IPanel
    {
        public PanelPosition DefaultPosition { get; private set; }

        public PanelPosition Position
        {
            get; set;
        }
    }
}

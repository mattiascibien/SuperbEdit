using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperbEdit.Base
{
    public interface IActionItem
    {
        string Name { get; }
        string Description { get; }
        bool IsSeparator { get; }

        void Execute();
    }
}

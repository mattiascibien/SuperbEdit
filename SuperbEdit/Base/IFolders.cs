using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperbEdit.Base
{
    public interface IFolders
    {
        string ProgramFolder { get; }

        string DefaultPackagesFolder { get; }

        string UserFolder { get; }

        string UserPackagesFolder { get; }

        string HighlighterFolder { get; }

        string DocumentationFolder { get; }
    }
}

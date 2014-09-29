using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SuperbEdit.Base
{
    public interface ITabMetadata
    {
        bool IsDefault { get; }
    }
}

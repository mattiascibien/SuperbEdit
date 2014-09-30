using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    public class SuperbCommand
    {
        public string CommandName
        {
            get;
            set;
        }

        public string CommandDescription
        {
            get;
            set;
        }

        public Action Action
        {
            get; set;
        }
    }
}

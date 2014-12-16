using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base.Scripting
{
    [Serializable]
    public class ScriptCompilerException : Exception
    {
        public ScriptCompilerException() { }
        public ScriptCompilerException(string message) : base(message) { }
        public ScriptCompilerException(string message, Exception inner) : base(message, inner) { }
        protected ScriptCompilerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

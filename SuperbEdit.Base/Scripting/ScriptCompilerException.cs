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
        public List<string> Errors
        {
            get;
            set;
        }
        public ScriptCompilerException(string fileName, List<string> errors)
            : base(string.Format("Cannot compile package '{0}' due to {1} errors", fileName, errors.Count)) 
        {
            Errors = errors;
        }
        protected ScriptCompilerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

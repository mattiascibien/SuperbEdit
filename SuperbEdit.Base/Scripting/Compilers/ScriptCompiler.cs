using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base.Scripting.Compilers
{
    /// <summary>
    /// Base abstract class for implementing a script compiler
    /// </summary>
    public abstract class ScriptCompiler
    {

        /// <summary>
        /// Compiles a single file (resides in Package
        /// root directory) package 
        /// </summary>
        /// <param name="fileName">The file</param>
        /// <returns>Compiled assembly</returns>
        public abstract Assembly Compile(string fileName);

        /// <summary>
        /// Compiles a package folder
        /// </summary>
        /// <param name="folderPath">The folder</param>
        /// <returns>Compiled assembly</returns>
        public abstract Assembly CompileFolder(string folderPath);
    }
}

using Boo.Lang.Compiler;
using Boo.Lang.Compiler.IO;
using Boo.Lang.Compiler.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base.Scripting
{
    public class BooScriptCompiler
    {
        static BooCompiler compiler;

        static BooScriptCompiler()
        {
            compiler = new BooCompiler();
            foreach (var assembly in AssemblyListComposer.GetAssemblyList())
            {
                compiler.Parameters.AddAssembly(assembly);
            }
            compiler.Parameters.Pipeline = new CompileToMemory();
            compiler.Parameters.Ducky = true;
        }

        public static Assembly Compile(string fileName)
        {
            compiler.Parameters.Input.Add(new FileInput(fileName));

            var context = compiler.Run();

            compiler.Parameters.Input.Clear();
            if (context.GeneratedAssembly != null)
            {
                throw new ScriptCompilerException();
            }
            else
            {
                return context.GeneratedAssembly;
            }
        }
    }
}

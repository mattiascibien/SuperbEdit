using Boo.Lang.Compiler;
using Boo.Lang.Compiler.IO;
using Boo.Lang.Compiler.Pipelines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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
            compiler.Parameters.Pipeline = new CompileToFile();

            compiler.Parameters.Ducky = true;
        }

        public static Assembly Compile(string fileName)
        {
            string output = Path.Combine(Folders.PackageCacheFolders, Path.GetFileName(fileName.Replace(".boo", ".cache")));

            FileInfo inputInfo = new FileInfo(fileName);
            FileInfo outputInfo = null;

            if (File.Exists(output))
            {
                outputInfo = new FileInfo(output);
            }

            if (outputInfo == null || inputInfo.LastWriteTime > outputInfo.LastWriteTime)
            {
                compiler.Parameters.Input.Add(new FileInput(fileName));
                compiler.Parameters.OutputAssembly = output;

                var context = compiler.Run();

                compiler.Parameters.Input.Clear();
                if (context.GeneratedAssembly == null)
                {
                    throw new ScriptCompilerException();
                }
            }
            
            return Assembly.LoadFile(output);
        }
    }
}

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

namespace SuperbEdit.Base.Scripting.Compilers
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
            //TODO: does it make sense to make this customizable?
            compiler.Parameters.Debug = false;
            compiler.Parameters.Ducky = true;
        }

        public Assembly Compile(string fileName)
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
                    throw new ScriptCompilerException(fileName, context.Errors.Select(e => e.ToString()).ToList());
                }
            }
            
            return Assembly.LoadFile(output);
        }


        public Assembly CompileFolder(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath, "*.boo", SearchOption.AllDirectories);

            string output = Path.Combine(Folders.PackageCacheFolders, Path.GetFileName(folderPath) + ".cache");

            FileInfo outputInfo = null;

            if (File.Exists(output))
            {
                outputInfo = new FileInfo(output);
            }
         

            if (outputInfo == null || CheckModifiedFiles(files, outputInfo))
            {
                foreach (var file in files)
                {
                    compiler.Parameters.Input.Add(new FileInput(file));
                }
                compiler.Parameters.OutputAssembly = output;

                var context = compiler.Run();

                compiler.Parameters.Input.Clear();
                if (context.GeneratedAssembly == null)
                {
                    throw new ScriptCompilerException(folderPath, context.Errors.Select(e => e.ToString()).ToList());
                }
            }



            return Assembly.LoadFile(output);
        }

        private bool CheckModifiedFiles(string[] files, FileInfo outputInfo)
        {
            return files.Any(f => new FileInfo(f).LastWriteTime > outputInfo.LastWriteTime);
        }
    }
}

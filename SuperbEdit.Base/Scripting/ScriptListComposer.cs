using SuperbEdit.Base.Scripting.Compilers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base.Scripting
{
    public static class ScriptListComposer
    {
        public static bool HasErrors = false;

        public static List<ScriptCompilerException> Exceptions = new List<ScriptCompilerException>();

        public static List<Assembly> loadedAssemblies;

        /// <summary>
        ///     Loads the assemblies from Packages folders
        /// </summary>
        /// <param name="pure">If 'true' loads packages only from program folders</param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetAssemblyList(bool pure = false)
        {
            loadedAssemblies = new List<Assembly>();
            GetScriptsInFolder(loadedAssemblies, Folders.DefaultPackagesFolder);

#if !PORTABLE_BUILD
            if (!pure)
            {
                if (!Directory.Exists(Folders.UserFolder))
                    Directory.CreateDirectory(Folders.UserFolder);

                if (!Directory.Exists(Folders.UserPackagesFolder))
                    Directory.CreateDirectory(Folders.UserPackagesFolder);

                GetScriptsInFolder(loadedAssemblies, Folders.UserPackagesFolder);
            }
#endif

            return loadedAssemblies;
        }

        private static void GetScriptsInFolder(List<Assembly> assemblies, string folder)
        {
            var scriptCompiler = new BooScriptCompiler();

            foreach (string script in Directory.GetFiles(folder, "*.boo"))
            {
                try
                {
                    if (!DisabledPackages.IsDisabled(Path.GetFileNameWithoutExtension(script)))
                        assemblies.Add(scriptCompiler.Compile(script));
                }
                catch(ScriptCompilerException ex)
                {
                    HasErrors = true;
                    Exceptions.Add(ex);
                }
            }

            foreach (string subDir in Directory.GetDirectories(folder))
            {
                if (!subDir.EndsWith("x86") && !subDir.EndsWith("x64"))
                {
                    try
                    {
                        if (!DisabledPackages.IsDisabled(Path.GetFileName(Path.GetFileName(subDir))))
                            if (Directory.GetFiles(subDir, "*.boo", SearchOption.AllDirectories).Length > 0)
                                assemblies.Add(scriptCompiler.CompileFolder(subDir));
                    }
                    catch(ScriptCompilerException ex)
                    {
                        HasErrors = true;
                        Exceptions.Add(ex);
                    }
                }
                else
                {
                    //Ignore the folder since it contains native DLLs
                }
            }
        }
    }
}

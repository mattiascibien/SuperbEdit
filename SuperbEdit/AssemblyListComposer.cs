using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SuperbEdit.Base;

namespace SuperbEdit
{
    public static class AssemblyListComposer
    {
        public static List<Assembly> loadedAssemblies;

        /// <summary>
        ///     Loads the assemblies from Packages folders
        /// </summary>
        /// <param name="pure">If 'true' loads packages only from program folders</param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetAssemblyList(bool pure = false)
        {
            loadedAssemblies = new List<Assembly>
            {
                Assembly.GetExecutingAssembly(),
                Assembly.LoadFrom("SuperbEdit.Base.dll")
            };

            GetAssembliesInFolder(loadedAssemblies, Folders.DefaultPackagesFolder);

#if !PORTABLE_BUILD
            if (!pure)
            {
                if (!Directory.Exists(Folders.UserFolder))
                    Directory.CreateDirectory(Folders.UserFolder);

                if (!Directory.Exists(Folders.UserPackagesFolder))
                    Directory.CreateDirectory(Folders.UserPackagesFolder);

                GetAssembliesInFolder(loadedAssemblies, Folders.UserPackagesFolder);
            }
#endif

            return loadedAssemblies;
        }

        private static void GetAssembliesInFolder(List<Assembly> assemblies, string folder)
        {
            foreach (string assembly in Directory.GetFiles(folder, "*.dll"))
            {
                assemblies.Add(Assembly.LoadFrom(assembly));
            }

            foreach (string subDir in Directory.GetDirectories(folder))
            {
                if (!subDir.EndsWith("x86") && !subDir.EndsWith("x64"))
                {
                    GetAssembliesInFolder(assemblies, subDir);
                }
                else
                {
                    //Ignore the folder since it contains native DLLs
                }
            }
        }
    }
}
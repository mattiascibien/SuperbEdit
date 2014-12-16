using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using SuperbEdit.Base;
using SuperbEdit.Base.Scripting;

namespace SuperbEdit.ViewModels
{
    public class PackageItem
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }
    }


    [Export]
    public sealed class AboutViewModel : Screen
    {
        public AboutViewModel()
        {
            DisplayName = "About SuperbEdit";
            LoadedPackages =
                AssemblyListComposer.loadedAssemblies
                    .Select(ass => new PackageItem
                    {
                        Name = ass.GetName().Name,
                        Version = ass.GetName().Version.ToString(),
                        Author = ass.GetCustomAttribute<AssemblyCompanyAttribute>().Company
                    });

            LoadedPackages = LoadedPackages.Concat(
                ScriptListComposer.loadedAssemblies
                    .Select(ass => new PackageItem
                    {
                        Name = ass.GetName().Name,
                        Version = ass.GetName().Version.ToString(),
                        //TODO: implement a way to diplay authorship information for scripted plugins
                        Author = "Compiled From Script"
                    }));
        }

        public string Version
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                return fvi.FileVersion;
            }
        }

        public bool ErrorsPresent
        {
            get
            {
                return ScriptListComposer.HasErrors;
            }
        }

        public List<string> ScriptErrors
        {
            get
            {
                var ret = new List<string>();
                foreach (var ex in ScriptListComposer.Exceptions)
                {
                    ret.AddRange(ex.Errors);
                }
                return ret;
            }
        }

        public string Copyright
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                return assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
            }
        }

        public IEnumerable<PackageItem> LoadedPackages { get; set; }
    }
}
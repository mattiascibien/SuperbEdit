using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;

namespace SuperbEdit.ViewModels
{
    public class PackageItem
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }


    [Export]
    internal sealed class AboutViewModel : Screen
    {
        public AboutViewModel()
        {
            DisplayName = "About SuperbEdit";
            LoadedPackages =
                AssemblyListComposer.loadedAssemblies
                    .Select(ass => new PackageItem
                    {
                        Name = ass.GetName().Name,
                        Version = ass.GetName().Version.ToString()
                    });
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

        public IEnumerable<PackageItem> LoadedPackages { get; set; }
    }
}
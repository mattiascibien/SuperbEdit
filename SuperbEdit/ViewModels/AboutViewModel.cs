using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Documents;
using Caliburn.Micro;
using SuperbEdit.Base;
using System.Collections.Generic;

namespace SuperbEdit.ViewModels
{
    [Export]
    public sealed class AboutViewModel : Screen
    {
        public AboutViewModel()
        {
            DisplayName = "About SuperbEdit";
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

        public IEnumerable<Assembly> LoadedPackages
        {
            get { return AssemblyListComposer.loadedAssemblies; }
        }

        public string License
        {
            get { return File.ReadAllText(Path.Combine(Folders.DocumentationFolder, "LICENSE.md")); }
        }
    }
}
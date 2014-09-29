using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;

namespace SuperbEdit.Base
{
    [Export(typeof(IFolders))]
    public class Folders : IFolders
    {
        public string ProgramFolder
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
        }


        public string DefaultPackagesFolder
        {
            get { return Path.Combine(ProgramFolder, "Packages"); }
        }

        public string UserFolder
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".superbedit"); }
        }

        public string UserPackagesFolder
        {
            get { return Path.Combine(UserFolder, "Packages"); }
        }

        public string HighlighterFolder
        {
            get { return Path.Combine(ProgramFolder, "Highlighters"); }
        }


        public string DocumentationFolder
        {
            get { return Path.Combine(ProgramFolder, "Docs"); }
        }

    }
}

using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;

namespace SuperbEdit.Base
{
    public static class Folders
    {
        public static string ProgramFolder
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
        }


        public static string DefaultPackagesFolder
        {
            get { return Path.Combine(ProgramFolder, "Packages"); }
        }

        public static string UserFolder
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".superbedit"); }
        }

        public static string UserPackagesFolder
        {
            get { return Path.Combine(UserFolder, "Packages"); }
        }

        public static string HighlighterFolder
        {
            get { return Path.Combine(ProgramFolder, "Highlighters"); }
        }


        public static string DocumentationFolder
        {
            get { return Path.Combine(ProgramFolder, "Docs"); }
        }

    }
}

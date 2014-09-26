using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Constants
{
    public static class Folders
    {
        public static readonly string ProgramFolder = 
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static readonly string DefaultPackagesFolder =
            Path.Combine(ProgramFolder, "Packages");

        public static readonly string UserFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".superbedit");

        public static readonly string UserPackagesFolder =
            Path.Combine(UserFolder, "Packages");

        public static readonly string HighlighterFolder =
            Path.Combine(ProgramFolder, "Highlighters");

        public static readonly string DocumentationFolder =
            Path.Combine(ProgramFolder, "Docs");
    }
}

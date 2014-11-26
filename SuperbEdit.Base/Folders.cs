using System;
using System.IO;
using System.Reflection;

namespace SuperbEdit.Base
{
    /// <summary>
    /// Helpers for getting various directories related to the functioning of the program
    /// </summary>
    public static class Folders
    {

        /// <summary>
        /// Path of the directory of the SuperbEdit executable
        /// </summary>
        public static string ProgramFolder
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
        }

        /// <summary>
        /// Path of the directory containing the program packages folder
        /// </summary>
        public static string DefaultPackagesFolder
        {
            get { return Path.Combine(ProgramFolder, "Packages"); }
        }

        /// <summary>
        /// Path of the directory containing the user data
        /// </summary>
        public static string UserFolder
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".superbedit");
            }
        }

        /// <summary>
        /// Path of the directory containing the packages for the current user
        /// </summary>
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
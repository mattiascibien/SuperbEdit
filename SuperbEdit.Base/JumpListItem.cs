using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    public class ExportJumpListItem  : ExportAttribute
    {
        public ExportJumpListItem() : base(typeof(IJumpListItem))
        {

        }
    }


    public interface IJumpListItem
    {
        string CommandLineArguments { get; }
        string Title { get; }
        string Description { get; }
    }

    public class JumpListItem : IJumpListItem
    {
        public JumpListItem(string title, string description, string commandLine)
        {
            Title = title;
            Description = description;
            CommandLineArguments = commandLine;
        }

        public string CommandLineArguments
        {
            get;
            private set;
        }


        public string Title
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }
    }
}

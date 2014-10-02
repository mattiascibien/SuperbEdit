using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    public abstract class ActionItem : IActionItem
    {

        protected ActionItem(string name, string description)
        {
            Name = name;
            Description = description;
            IsSeparator = false;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public abstract void Execute();


        public bool IsSeparator { get; protected set; }
    }

    public interface IActionItemMetadata
    {
        string Menu { get; }
        string Owner { get; }
        int Order { get; }
        bool RegisterInCommandWindow { get; }
    }

    [Export(typeof (SeparatorItem)) ]
    public class SeparatorItem : ActionItem
    {
        public SeparatorItem() : base("", "")
        {
            IsSeparator = true;
        }
        public override void Execute()
        {
            
        }
    }


    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportActionMetadata : ExportAttribute, IActionItemMetadata
    {
        public ExportActionMetadata()
            : base(typeof(ActionItem))
        {

        } 

        public string Menu { get; set; }

        public string Owner { get; set; }

        public int Order { get; set; }

        public bool RegisterInCommandWindow { get; set; }
    }
}
